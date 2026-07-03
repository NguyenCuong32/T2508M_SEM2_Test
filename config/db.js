const mysql = require('mysql2/promise');

const pool = mysql.createPool({
  host: 'localhost',
  user: 'root',
  password: '',
  database: 'baithi'
});

const connectDB = async () => {
  const connection = await mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: ''
  });
  await connection.query('CREATE DATABASE IF NOT EXISTS baithi');
  await connection.query('USE baithi');
  await connection.query(`
    CREATE TABLE IF NOT EXISTS baithi (
      id INT AUTO_INCREMENT PRIMARY KEY,
      treename VARCHAR(255) NOT NULL,
      description TEXT NOT NULL,
      image VARCHAR(255)
    )
  `);
  await connection.end();
};

module.exports = { pool, connectDB };
