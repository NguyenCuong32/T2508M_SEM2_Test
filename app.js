require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const multer = require('multer');
const path = require('path');
const fs = require('fs');
const Tree = require('./models/Tree');

const app = express();
const port = process.env.PORT || 3000;

// Connect to MongoDB
mongoose.connect(process.env.MONGODB_URI, { serverSelectionTimeoutMS: 5000 }).then(() => {
    console.log("Connected to MongoDB successfully!");
}).catch(err => {
    console.error("MongoDB Atlas connection failed:", err.message);
});

// Setup EJS
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));
app.use('/uploads', express.static(path.join(__dirname, 'uploads')));

// Configure Multer for File Upload
const storage = multer.diskStorage({
    destination: function (req, file, cb) {
        const uploadPath = path.join(__dirname, 'uploads');
        if (!fs.existsSync(uploadPath)) {
            fs.mkdirSync(uploadPath);
        }
        cb(null, uploadPath);
    },
    filename: function (req, file, cb) {
        cb(null, Date.now() + '-' + file.originalname);
    }
});
const upload = multer({ storage: storage });

// Routes
app.get('/', async (req, res) => {
    try {
        let trees = [];
        if (mongoose.connection.readyState === 1) {
            trees = await Tree.find();
        }
        res.render('index', { trees: trees, pageTitle: 'Tree Shop', dbError: mongoose.connection.readyState !== 1 });
    } catch (err) {
        console.error(err);
        res.render('index', { trees: [], pageTitle: 'Tree Shop', dbError: true });
    }
});

app.post('/add', upload.single('image'), async (req, res) => {
    try {
        if (mongoose.connection.readyState !== 1) {
            return res.status(500).send("Database not connected. Please fix your MongoDB connection first.");
        }

        const { treename, description } = req.body;
        let image = '';
        if (req.file) {
            image = '/uploads/' + req.file.filename;
        }

        const newTree = new Tree({
            treename,
            description,
            image
        });

        await newTree.save();
        res.redirect('/');
    } catch (err) {
        console.error(err);
        res.status(500).send("Error saving tree.");
    }
});

app.get('/delete/:id', async (req, res) => {
    try {
        if (mongoose.connection.readyState !== 1) {
            return res.status(500).send("Database not connected. Please fix your MongoDB connection first.");
        }

        await Tree.findByIdAndDelete(req.params.id);
        res.redirect('/');
    } catch (err) {
        console.error(err);
        res.status(500).send("Error deleting tree.");
    }
});

app.get('/about', (req, res) => {
    res.render('about', { pageTitle: 'About Me' });
});

app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});
