const express = require('express');
const mysql = require('mysql2');
const path = require('path');

const app = express();
const PORT = process.env.PORT || 3000;

// Cấu hình Middleware xử lý dữ liệu từ Form gửi lên
app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// Cấu hình thư mục tĩnh public để chứa hình ảnh
app.use(express.static(path.join(__dirname, 'public')));

// Thiết lập View Engine là EJS
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

// --- CẤU HÌNH KẾT NỐI MYSQL XAMPP ---
// Nếu XAMPP của bạn hiển thị cổng là 3306 thì giữ nguyên. Nếu hiển thị 3307 thì sửa số 3306 thành 3307.
const dbConfig = {
    host: '127.0.0.1',
    user: 'root',
    password: '',
    port: 3306 
};

let db;

function handleDisconnect() {
    db = mysql.createConnection(dbConfig);

    db.connect(err => {
        if (err) {
            console.error('❌ LỖI: Không thể kết nối tới MySQL XAMPP! Hãy chắc chắn bạn đã nhấn START MySQL trong XAMPP Control Panel.');
            console.error('Chi tiết lỗi:', err.message);
            // Thử kết nối lại sau 2 giây nếu thất bại
            setTimeout(handleDisconnect, 2000);
            return;
        }
        console.log(' Đã kết nối thành công tới MySQL của XAMPP!');
        
        // Tự động khởi tạo Database và Bảng dữ liệu theo yêu cầu đề bài
        db.query('CREATE DATABASE IF NOT EXISTS TreeShop', (err) => {
            if (err) console.error(err);
            
            db.query('USE TreeShop', (err) => {
                if (err) console.error(err);
                
                const createTableSql = `
                    CREATE TABLE IF NOT EXISTS TreeCollection (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        treename VARCHAR(255) NOT NULL,
                        description TEXT NOT NULL,
                        image VARCHAR(255)
                    )
                `;
                db.query(createTableSql, (err) => {
                    if (err) console.error(err);
                    else console.log(' Cơ sở dữ liệu và bảng TreeCollection đã sẵn sàng hoạt động!');
                });
            });
        });
    });

    db.on('error', err => {
        console.error('Mất kết nối cơ sở dữ liệu:', err);
        if (err.code === 'PROTOCOL_CONNECTION_LOST') {
            handleDisconnect();
        } else {
            throw err;
        }
    });
}

// Chạy hàm kết nối
handleDisconnect();

// Biến cục bộ tạm thời để đẩy thông báo ra giao diện EJS
let errorsNotification = [];
let successNotification = '';

// --- CÁC ROUTE XỬ LÝ DỮ LIỆU ---

// Route 1: Hiển thị trang chủ và danh sách cây (Yêu cầu 4)
app.get('/', (req, res) => {
    const sql = 'SELECT * FROM TreeCollection';
    db.query(sql, (err, results) => {
        if (err) {
            console.error(err);
            return res.status(500).send('Lỗi hệ thống khi tải danh sách cây. Vui lòng kiểm tra lại XAMPP MySQL.');
        }
        
        res.render('index', { 
            trees: results, 
            errors: errorsNotification,
            success_msg: successNotification
        });
        
        // Xóa thông báo sau khi tải trang xong
        errorsNotification = [];
        successNotification = '';
    });
});

// Route 2: Xử lý chức năng thêm mới cây (Yêu cầu 1 và Yêu cầu 6 Validate)
app.post('/add', (req, res) => {
    const { treename, description, image } = req.body;
    const localErrors = [];

    // Khâu kiểm tra Validation dữ liệu đầu vào theo Yêu cầu 6
    if (!treename || treename.trim() === '') {
        localErrors.push('Tree Name is required (Tên cây không được để trống).');
    }
    if (!description || description.trim() === '') {
        localErrors.push('Description is required (Mô tả không được để trống).');
    }

    // Nếu có lỗi, quay ngược lại trang chủ và hiển thị lỗi
    if (localErrors.length > 0) {
        errorsNotification = localErrors;
        return res.redirect('/');
    }

    // Thực hiện thêm thông tin cây vào bảng TreeCollection
    const sql = 'INSERT INTO TreeCollection (treename, description, image) VALUES (?, ?, ?)';
    const imgValue = image ? image.trim() : 'D:\\image\\phonglan.png';

    db.query(sql, [treename.trim(), description.trim(), imgValue], (err) => {
        if (err) {
            console.error(err);
            errorsNotification = ['Đã xảy ra lỗi khi thêm dữ liệu vào MySQL.'];
            return res.redirect('/');
        }
        successNotification = 'Thêm dữ liệu cây thành công vào Database!';
        res.redirect('/');
    });
});

// Route 3: Xử lý chức năng Reset để xóa sạch toàn bộ dữ liệu trong bảng (Yêu cầu 5)
app.get('/reset', (req, res) => {
    const sql = 'TRUNCATE TABLE TreeCollection';
    db.query(sql, (err) => {
        if (err) {
            console.error(err);
            errorsNotification = ['Không thể làm sạch dữ liệu do lỗi kết nối Database.'];
            return res.redirect('/');
        }
        successNotification = 'Đã làm sạch (Reset) toàn bộ dữ liệu trong TreeCollection!';
        res.redirect('/');
    });
});

// Route 4: Trang hiển thị thông tin About me phụ
app.get('/about', (req, res) => {
    res.render('about');
});

// Khởi chạy Server
app.listen(PORT, () => {
    console.log(`Server đang chạy ổn định tại: http://localhost:${PORT}`);
});