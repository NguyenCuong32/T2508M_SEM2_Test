require("dotenv").config();

const fs = require("fs");
const path = require("path");
const express = require("express");
const mongoose = require("mongoose");
const multer = require("multer");

const Tree = require("./models/tree");

const app = express();
const PORT = process.env.PORT || 3000;
const MONGO_URI = process.env.MONGO_URI || "mongodb://127.0.0.1:27017/TreeShop";
const uploadDir = path.join(__dirname, "public", "uploads");

fs.mkdirSync(uploadDir, { recursive: true });

const databaseState = {
  connected: false,
  error: null
};
let mongoConnectionPromise = null;
let lastMongoAttemptAt = 0;

mongoose.set("strictQuery", true);

mongoose.connection.on("connected", () => {
  databaseState.connected = true;
  databaseState.error = null;
});

mongoose.connection.on("disconnected", () => {
  databaseState.connected = false;
});

mongoose.connection.on("error", (error) => {
  databaseState.connected = false;
  databaseState.error = error.message;
});

function connectToMongo() {
  if (mongoose.connection.readyState === 1) {
    databaseState.connected = true;
    databaseState.error = null;
    return Promise.resolve(true);
  }

  if (mongoConnectionPromise) {
    return mongoConnectionPromise;
  }

  const now = Date.now();
  if (databaseState.error && now - lastMongoAttemptAt < 4000) {
    return Promise.resolve(false);
  }

  lastMongoAttemptAt = now;
  mongoConnectionPromise = mongoose
    .connect(MONGO_URI, { serverSelectionTimeoutMS: 2500 })
    .then(() => {
      databaseState.connected = true;
      databaseState.error = null;
      return true;
    })
    .catch((error) => {
      databaseState.connected = false;
      databaseState.error = error.message;
      console.error("MongoDB connection failed:", error.message);
      return false;
    })
    .finally(() => {
      mongoConnectionPromise = null;
    });

  return mongoConnectionPromise;
}

connectToMongo();

const storage = multer.diskStorage({
  destination: (req, file, cb) => {
    cb(null, uploadDir);
  },
  filename: (req, file, cb) => {
    const ext = path.extname(file.originalname).toLowerCase() || ".jpg";
    const safeBase = path
      .basename(file.originalname, ext)
      .replace(/[^a-z0-9]/gi, "-")
      .replace(/-+/g, "-")
      .toLowerCase()
      .slice(0, 40);
    cb(null, `${Date.now()}-${safeBase || "tree"}${ext}`);
  }
});

const upload = multer({
  storage,
  limits: {
    fileSize: 2 * 1024 * 1024
  },
  fileFilter: (req, file, cb) => {
    if (!file.mimetype.startsWith("image/")) {
      return cb(new Error("Image must be a valid image file."));
    }

    cb(null, true);
  }
});

app.set("view engine", "ejs");
app.set("views", path.join(__dirname, "views"));

app.use(express.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, "public")));

app.use((req, res, next) => {
  res.locals.currentPath = req.path;
  res.locals.year = new Date().getFullYear();
  next();
});

function validateTreeInput(body) {
  const values = {
    treename: (body.treename || "").trim(),
    description: (body.description || "").trim(),
    image: (body.image || "").trim()
  };
  const errors = [];

  if (!values.treename) {
    errors.push("Tree Name is required.");
  }

  if (!values.description) {
    errors.push("Description is required.");
  }

  return { values, errors };
}

function removeUploadedFile(file) {
  if (!file) {
    return;
  }

  fs.unlink(file.path, () => {});
}

function removeLocalImage(imagePath) {
  if (!imagePath || !imagePath.startsWith("/uploads/")) {
    return;
  }

  const resolvedPath = path.resolve(
    __dirname,
    "public",
    imagePath.replace(/^\/+/, "")
  );
  const safeUploadRoot = path.resolve(uploadDir);

  if (!resolvedPath.startsWith(safeUploadRoot)) {
    return;
  }

  fs.unlink(resolvedPath, () => {});
}

function getSuccessMessage(key) {
  const messages = {
    added: "Tree added successfully.",
    updated: "Tree updated successfully.",
    deleted: "Tree deleted successfully.",
    reset: "All trees have been removed."
  };

  return messages[key] || null;
}

async function renderShop(req, res, options = {}) {
  let trees = [];
  let dbError = options.dbError || null;

  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    trees = await Tree.find().sort({ createdAt: -1 }).lean();
  } catch (error) {
    dbError =
      "Cannot read TreeCollection. Please start MongoDB and check MONGO_URI.";
  }

  res.render("index", {
    title: "TreeShop",
    trees,
    errors: options.errors || [],
    formData: options.formData || {},
    editTree: options.editTree || null,
    dbError,
    successMessage: options.successMessage || getSuccessMessage(req.query.success)
  });
}

app.get("/", (req, res) => {
  res.redirect("/treeshop");
});

app.get("/treeshop", (req, res) => renderShop(req, res));

app.post("/trees", upload.single("imageFile"), async (req, res) => {
  const { values, errors } = validateTreeInput(req.body);

  if (errors.length) {
    removeUploadedFile(req.file);
    return renderShop(req, res, { errors, formData: values });
  }

  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    await Tree.create({
      treename: values.treename,
      description: values.description,
      image: req.file ? `/uploads/${req.file.filename}` : values.image
    });
    res.redirect("/treeshop?success=added");
  } catch (error) {
    removeUploadedFile(req.file);
    renderShop(req, res, {
      errors: ["Tree could not be saved. Please check MongoDB connection."],
      formData: values
    });
  }
});

app.get("/trees/:id/edit", async (req, res) => {
  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    const editTree = await Tree.findById(req.params.id).lean();

    if (!editTree) {
      return res.redirect("/treeshop");
    }

    renderShop(req, res, {
      editTree,
      formData: editTree
    });
  } catch (error) {
    renderShop(req, res, {
      errors: ["Tree could not be loaded for editing."]
    });
  }
});

app.post("/trees/:id/update", upload.single("imageFile"), async (req, res) => {
  const { values, errors } = validateTreeInput(req.body);

  if (errors.length) {
    removeUploadedFile(req.file);
    return renderShop(req, res, {
      errors,
      formData: values,
      editTree: { _id: req.params.id, ...values }
    });
  }

  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    const currentTree = await Tree.findById(req.params.id);

    if (!currentTree) {
      removeUploadedFile(req.file);
      return res.redirect("/treeshop");
    }

    const previousImage = currentTree.image;
    const nextImage = req.file ? `/uploads/${req.file.filename}` : values.image;

    currentTree.treename = values.treename;
    currentTree.description = values.description;
    currentTree.image = nextImage;
    await currentTree.save();

    if (previousImage !== nextImage) {
      removeLocalImage(previousImage);
    }

    res.redirect("/treeshop?success=updated");
  } catch (error) {
    removeUploadedFile(req.file);
    renderShop(req, res, {
      errors: ["Tree could not be updated. Please check MongoDB connection."],
      formData: values,
      editTree: { _id: req.params.id, ...values }
    });
  }
});

app.post("/trees/:id/delete", async (req, res) => {
  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    const deletedTree = await Tree.findByIdAndDelete(req.params.id).lean();

    if (deletedTree) {
      removeLocalImage(deletedTree.image);
    }

    res.redirect("/treeshop?success=deleted");
  } catch (error) {
    renderShop(req, res, {
      errors: ["Tree could not be deleted. Please check MongoDB connection."]
    });
  }
});

app.post("/trees/reset", async (req, res) => {
  try {
    const isConnected = await connectToMongo();
    if (!isConnected) {
      throw new Error(databaseState.error);
    }

    const trees = await Tree.find({}, { image: 1 }).lean();
    await Tree.deleteMany({});
    trees.forEach((tree) => removeLocalImage(tree.image));
    res.redirect("/treeshop?success=reset");
  } catch (error) {
    renderShop(req, res, {
      errors: ["TreeCollection could not be reset. Please check MongoDB connection."]
    });
  }
});

app.get("/about", (req, res) => {
  res.render("about", {
    title: "About me"
  });
});

app.use((err, req, res, next) => {
  if (err instanceof multer.MulterError) {
    return renderShop(req, res, {
      errors: [`Upload error: ${err.message}`],
      formData: req.body || {}
    });
  }

  renderShop(req, res, {
    errors: [err.message || "Something went wrong."],
    formData: req.body || {}
  });
});

app.listen(PORT, () => {
  console.log(`TreeShop is running at http://localhost:${PORT}`);
  console.log(`MongoDB URI: ${MONGO_URI}`);
});
