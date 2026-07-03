require('dotenv').config();
const express = require('express');
const path = require('path');
const { sequelize, Tree } = require('./models/Tree');

const app = express();
const PORT = process.env.PORT || 3000;

// Kết nối PostgreSQL qua Sequelize
sequelize.sync()
    .then(() => console.log('Connected to PostgreSQL successfully'))
    .catch(err => console.error('PostgreSQL connection error:', err));

// Cài đặt view engine là EJS
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// Middleware cho public files (CSS, Images) và nhận dữ liệu form
app.use(express.static(path.join(__dirname, 'public')));
app.use(express.urlencoded({ extended: true }));

// ---- Các Routes ----

// 1. Hiển thị danh sách và form trên trang chủ
app.get('/', async (req, res) => {
    try {
        const trees = await Tree.findAll({ order: [['id', 'ASC']] }); // Lấy tất cả trees từ db (Sequelize)
        res.render('index', { trees: trees, currentRoute: '/' });
    } catch (error) {
        console.error(error);
        res.status(500).send('Error loading trees');
    }
});

// 2. Chức năng Add Tree
app.post('/add', async (req, res) => {
    try {
        const { treename, description, image } = req.body;
        
        // Validation (Backend)
        if (!treename || !description) {
            return res.status(400).send('Tree Name và Description là bắt buộc!');
        }

        await Tree.create({
            treename,
            description,
            image
        }); // Lưu vào PostgreSQL

        res.redirect('/'); // Quay lại trang chủ sau khi thêm thành công
    } catch (error) {
        console.error(error);
        res.status(500).send('Error adding tree');
    }
});

// 3. Trang About me
app.get('/about', (req, res) => {
    res.render('about', { currentRoute: '/about' });
});

// 4. (Tuỳ chọn thêm theo giao diện) Xóa Tree
app.post('/delete/:id', async (req, res) => {
    try {
        await Tree.destroy({ where: { id: req.params.id } }); // Xóa tree (Sequelize)
        res.redirect('/');
    } catch (error) {
        console.error(error);
        res.status(500).send('Error deleting tree');
    }
});

app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
