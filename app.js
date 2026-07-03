const express = require("express");
const mongoose = require("mongoose");
const path = require("path");
const treeRoutes = require("./routes/treeRoutes");

const app = express();

mongoose.connect("mongodb://127.0.0.1:27017/TreeShop")
    .then(() => console.log("MongoDB Connected"))
    .catch((err) => console.log(err));

app.use(express.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, "public")));

app.set("view engine", "ejs");
app.set("views", path.join(__dirname, "views"));

app.use("/", treeRoutes);

const PORT = 3000;

app.listen(PORT, () => {
    console.log(`Server is running at http://localhost:${PORT}`);
});