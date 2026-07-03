const express = require('express');
const bodyParser = require('body-parser');
const multer = require('multer');
const path = require('path');
const fs = require('fs');
// Gọi cấu hình Sequelize và Model
const { sequelize, Tree } = require('./models/Tree');

const app = express();
const PORT = process.env.PORT || 3000;

app.set('view engine', 'ejs');
app.use(express.static('public'));
app.use(bodyParser.urlencoded({ extended: true }));

// Setup multer for image upload
const storage = multer.diskStorage({
    destination: function (req, file, cb) {
        const uploadPath = './public/uploads/';
        if (!fs.existsSync(uploadPath)) {
            fs.mkdirSync(uploadPath, { recursive: true });
        }
        cb(null, uploadPath);
    },
    filename: function (req, file, cb) {
        cb(null, Date.now() + path.extname(file.originalname));
    }
});

const upload = multer({ storage: storage });

// Routes
// 4. Show all trees in the database to the website
app.get('/', async (req, res) => {
    try {
        const trees = await Tree.findAll();
        res.render('index', { trees, error: null });
    } catch (err) {
        res.status(500).send(err.message);
    }
});

app.get('/about', (req, res) => {
    res.render('about');
});

// 1. Write a function Add Tree information into database include: name, description, image.
// 6. Validate input Tree Name, Description is required
app.post('/add', upload.single('image'), async (req, res) => {
    const { treename, description } = req.body;
    const imagePath = req.file ? '/uploads/' + req.file.filename : null;

    if (!treename || !description) {
        const trees = await Tree.findAll();
        return res.render('index', { trees, error: 'Tree Name and Description are required!' });
    }

    try {
        await Tree.create({
            treename,
            description,
            image: imagePath
        });

        res.redirect('/');
    } catch (err) {
        res.status(500).send(err.message);
    }
});

// 5. Write a function Reset to clean all
app.post('/reset', async (req, res) => {
    try {
        await Tree.destroy({ where: {}, truncate: true });

        // Delete uploaded images safely
        const directory = './public/uploads';
        if (fs.existsSync(directory)) {
            fs.readdirSync(directory).forEach(file => {
                if (file !== '.gitkeep') {
                    try { fs.unlinkSync(path.join(directory, file)); } catch (e) { }
                }
            });
        }
        res.redirect('/');
    } catch (err) {
        res.status(500).send(err.message);
    }
});

// Delete individual
app.post('/delete/:id', async (req, res) => {
    try {
        await Tree.destroy({ where: { id: req.params.id } });
        res.redirect('/');
    } catch (err) {
        res.status(500).send(err.message);
    }
});

// Đồng bộ cơ sở dữ liệu và chạy server
sequelize.sync().then(() => {
    console.log("Database synchronized");
    app.listen(PORT, () => {
        console.log(`Server is running on http://localhost:${PORT}`);
    });
}).catch(err => {
    console.error("Unable to connect to the database. Error: ", err);
});
