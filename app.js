const express = require('express');
const path = require('path');
const fs = require('fs');
const multer = require('multer');
const { initializeDb } = require('./db');

const app = express();
const PORT = process.env.PORT || 3000;

// Ensure uploads directories exist
const uploadDir = path.join(__dirname, 'public', 'images');
if (!fs.existsSync(uploadDir)) {
  fs.mkdirSync(uploadDir, { recursive: true });
}

// Multer Storage Configuration
const storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, uploadDir);
  },
  filename: function (req, file, cb) {
    const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1E9);
    cb(null, uniqueSuffix + '-' + file.originalname.replace(/\s+/g, '_'));
  }
});

const upload = multer({ 
  storage: storage,
  limits: { fileSize: 5 * 1024 * 1024 } // 5MB limit
});

// Configure EJS view engine
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// Middlewares
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// Global variable to reference the active Database Model
let Tree;
let dbConnected = false;

// Initialize Database connection
initializeDb().then(db => {
  Tree = db.Model;
  dbConnected = db.isConnected;
  
  // Start server after DB initialization
  app.listen(PORT, () => {
    console.log(`==================================================`);
    console.log(`Server is running at http://localhost:${PORT}`);
    console.log(`Database Connection Status: ${dbConnected ? 'CONNECTED TO MONGODB' : 'FALLBACK TO JSON DATABASE'}`);
    console.log(`==================================================`);
  });
}).catch(err => {
  console.error('Fatal error initializing database:', err);
  process.exit(1);
});

// Routes

// 1. GET '/': Show all trees (and handle Edit mode via query param)
app.get('/', async (req, res) => {
  try {
    const trees = await Tree.find();
    let editTree = undefined;

    if (req.query.edit) {
      editTree = await Tree.findById(req.query.edit);
    }

    res.render('index', { 
      trees, 
      editTree,
      error: null,
      success: req.query.success || null
    });
  } catch (err) {
    console.error('Error fetching trees:', err);
    res.status(500).send('Internal Server Error');
  }
});

// 2. POST '/add': Add Tree information into MongoDB/JSON
app.post('/add', upload.single('imageFile'), async (req, res) => {
  const { treename, description, image } = req.body;
  
  // Input Validation (Tree Name and Description are required)
  if (!treename || !treename.trim() || !description || !description.trim()) {
    try {
      const trees = await Tree.find();
      return res.render('index', {
        trees,
        editTree: undefined,
        error: 'Tree Name and Description are required fields.',
        success: null
      });
    } catch (dbErr) {
      return res.status(500).send('Database Error');
    }
  }

  try {
    let finalImagePath = image || '/images/phonglan.png';

    // If file was uploaded via Multer, use the uploaded path
    if (req.file) {
      finalImagePath = '/images/' + req.file.filename;
    }

    await Tree.create({
      treename: treename.trim(),
      description: description.trim(),
      image: finalImagePath
    });

    res.redirect('/?success=Tree added successfully!');
  } catch (err) {
    console.error('Error adding tree:', err);
    try {
      const trees = await Tree.find();
      res.render('index', {
        trees,
        editTree: undefined,
        error: 'Error occurred while saving to database: ' + err.message,
        success: null
      });
    } catch (dbErr) {
      res.status(500).send('Error occurred');
    }
  }
});

// 3. POST '/edit/:id': Update Tree details
app.post('/edit/:id', upload.single('imageFile'), async (req, res) => {
  const { treename, description, image } = req.body;
  const { id } = req.params;

  // Validation
  if (!treename || !treename.trim() || !description || !description.trim()) {
    try {
      const trees = await Tree.find();
      const editTree = await Tree.findById(id);
      return res.render('index', {
        trees,
        editTree,
        error: 'Tree Name and Description cannot be empty.',
        success: null
      });
    } catch (dbErr) {
      return res.status(500).send('Database Error');
    }
  }

  try {
    let finalImagePath = image;

    if (req.file) {
      finalImagePath = '/images/' + req.file.filename;
    }

    await Tree.findByIdAndUpdate(id, {
      treename: treename.trim(),
      description: description.trim(),
      image: finalImagePath
    });

    res.redirect('/?success=Tree updated successfully!');
  } catch (err) {
    console.error('Error updating tree:', err);
    res.redirect('/?error=Error updating tree details.');
  }
});

// 4. POST '/delete/:id': Delete a tree record
app.post('/delete/:id', async (req, res) => {
  try {
    await Tree.findByIdAndDelete(req.params.id);
    res.redirect('/?success=Tree deleted successfully!');
  } catch (err) {
    console.error('Error deleting tree:', err);
    res.redirect('/?error=Error deleting tree record.');
  }
});

// 5. POST '/reset': Clean all data in TreeCollection (Database Reset)
app.post('/reset', async (req, res) => {
  try {
    await Tree.deleteMany({});
    res.redirect('/?success=Database reset successful. All records cleared!');
  } catch (err) {
    console.error('Error resetting database:', err);
    res.redirect('/?error=Error resetting database.');
  }
});

// 6. GET '/about': About Me page
app.get('/about', (req, res) => {
  res.render('about');
});
