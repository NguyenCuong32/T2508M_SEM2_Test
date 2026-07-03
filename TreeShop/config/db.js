const { Sequelize } = require('sequelize');

const sequelize = new Sequelize('treeshop', 'root', '', {
    host: 'localhost',
    dialect: 'mysql',
    logging: false
});

const connectDB = async () => {
    try {
        const mysql = require('mysql2/promise');
        const connection = await mysql.createConnection({ host: 'localhost', user: 'root', password: '' });
        await connection.query(`CREATE DATABASE IF NOT EXISTS \`treeshop\`;`);
        await connection.end();

        await sequelize.authenticate();
        console.log('✅ Đã kết nối thành công tới MySQL (Laragon - treeshop)');
        await sequelize.sync();
    } catch (error) {
        console.error('❌ Lỗi kết nối MySQL:', error);
        process.exit(1);
    }
};

module.exports = { sequelize, connectDB };
