const express = require('express');
const path = require('path');
const multer = require('multer');
const { connectDB } = require('./config/db');
const Tree = require('./models/tree');

const app = express();
const PORT = 3000;

// 1. Kết nối MongoDB
connectDB();

// 2. Cấu hình Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
// Cho phép truy cập tĩnh vào thư mục public để load ảnh
app.use(express.static('public'));

// 3. Cấu hình Template Engine (EJS)
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// 4. Cấu hình Multer để upload ảnh
const storage = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, './public/uploads'); // Thư mục lưu file
    },
    filename: function (req, file, cb) {
        // Tạo tên file duy nhất để không bị trùng
        cb(null, Date.now() + path.extname(file.originalname)); 
    }
});
const upload = multer({ storage: storage });

// ================= ROUTING ================= //

// [GET] Trang chủ - Hiển thị form và bảng danh sách cây
app.get('/', async (req, res) => {
    try {
        const trees = await Tree.findAll();
        res.render('index', { trees: trees, currentRoute: '/' });
    } catch (error) {
        console.error(error);
        res.status(500).send("Lỗi server khi lấy dữ liệu.");
    }
});

// [POST] Xử lý thêm cây mới
app.post('/add', upload.single('image'), async (req, res) => {
    try {
        const { treename, description } = req.body;
        const image = req.file ? '/uploads/' + req.file.filename : '';

        // Validate cơ bản ở backend
        if (!treename || !description || !image) {
             return res.status(400).send("Vui lòng nhập đầy đủ tên, mô tả và chọn ảnh.");
        }

        await Tree.create({
            treename,
            description,
            image
        });
        res.redirect('/'); // Xong thì load lại trang chủ
    } catch (error) {
        console.error(error);
        res.status(500).send("Lỗi khi thêm cây mới.");
    }
});

// [GET] Hiển thị form sửa cây
app.get('/edit/:id', async (req, res) => {
    try {
        const tree = await Tree.findByPk(req.params.id);
        if (!tree) return res.status(404).send("Không tìm thấy cây.");
        res.render('edit', { tree: tree, currentRoute: '/' });
    } catch (error) {
        console.error(error);
        res.status(500).send("Lỗi khi tải dữ liệu cây.");
    }
});

// [POST] Xử lý sửa cây
app.post('/edit/:id', upload.single('image'), async (req, res) => {
    try {
        const { treename, description } = req.body;
        const tree = await Tree.findByPk(req.params.id);
        if (!tree) return res.status(404).send("Không tìm thấy cây.");

        tree.treename = treename;
        tree.description = description;
        if (req.file) {
            tree.image = '/uploads/' + req.file.filename;
        }

        await tree.save();
        res.redirect('/');
    } catch (error) {
        console.error(error);
        res.status(500).send("Lỗi khi cập nhật cây.");
    }
});

// [GET] Xóa cây
app.get('/delete/:id', async (req, res) => {
    try {
        const tree = await Tree.findByPk(req.params.id);
        if (tree) {
            await tree.destroy();
        }
        res.redirect('/');
    } catch (error) {
        console.error(error);
        res.status(500).send("Lỗi khi xóa cây.");
    }
});

// [GET] Trang About me
app.get('/about', (req, res) => {
    res.render('about', { currentRoute: '/about' });
});

// ================= START SERVER ================= //
app.listen(PORT, () => {
    console.log(`🚀 Server đang chạy tại: http://localhost:${PORT}`);
});
