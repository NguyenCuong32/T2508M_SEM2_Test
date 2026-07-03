const express = require('express');
const mongoose = require('mongoose');
const path = require('path');

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// View Engine
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// MongoDB Connection - Database name: TreeShop
// Thay đổi connection string tại đây nếu dùng MongoDB Atlas
const MONGO_URI = process.env.MONGO_URI || 'mongodb://127.0.0.1:27017/TreeShop';

mongoose.connect(MONGO_URI)
    .then(() => console.log('Connected to MongoDB - Database: TreeShop'))
    .catch(err => console.error('MongoDB connection error:', err));

// Import Model
const Tree = require('./models/Tree');

// ============ ROUTES ============

// Route: GET / - Trang chủ TreeShop - Hiển thị form + danh sách cây (Question 1, 4)
app.get('/', async (req, res) => {
    try {
        const trees = await Tree.find();
        res.render('index', {
            trees: trees,
            errors: [],
            success: '',
            currentRoute: '/'
        });
    } catch (err) {
        console.error(err);
        res.status(500).send('Server Error');
    }
});

// Route: POST /add - Thêm cây vào MongoDB (Question 1 + Question 6 Validate)
app.post('/add', async (req, res) => {
    const { treename, description, image } = req.body;
    const errors = [];

    // Question 6: Validate input - Tree Name, Description is required
    if (!treename || treename.trim() === '') {
        errors.push('Tree Name is required.');
    }
    if (!description || description.trim() === '') {
        errors.push('Description is required.');
    }

    if (errors.length > 0) {
        try {
            const trees = await Tree.find();
            return res.render('index', {
                trees: trees,
                errors: errors,
                success: '',
                currentRoute: '/'
            });
        } catch (err) {
            console.error(err);
            return res.status(500).send('Server Error');
        }
    }

    try {
        const newTree = new Tree({
            treename: treename.trim(),
            description: description.trim(),
            image: image ? image.trim() : ''
        });
        await newTree.save();

        const trees = await Tree.find();
        res.render('index', {
            trees: trees,
            errors: [],
            success: 'Tree added successfully!',
            currentRoute: '/'
        });
    } catch (err) {
        console.error(err);
        const trees = await Tree.find();
        res.render('index', {
            trees: trees,
            errors: ['Error adding tree to database.'],
            success: '',
            currentRoute: '/'
        });
    }
});

// Route: GET /reset - Xóa tất cả dữ liệu (Question 5)
app.get('/reset', async (req, res) => {
    try {
        await Tree.deleteMany({});
        const trees = await Tree.find();
        res.render('index', {
            trees: trees,
            errors: [],
            success: 'All data has been reset!',
            currentRoute: '/'
        });
    } catch (err) {
        console.error(err);
        res.redirect('/');
    }
});

// Route: GET /about - Trang About me
app.get('/about', (req, res) => {
    res.render('about', {
        currentRoute: '/about'
    });
});

// Start Server
app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
});