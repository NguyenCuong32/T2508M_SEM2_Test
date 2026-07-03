
const { Sequelize, DataTypes } = require('sequelize');

// Update to match Laragon's default MySQL configuration
const sequelize = new Sequelize('TreeShop', 'root', '', {
    host: 'localhost',
    dialect: 'mysql',
    logging: false // Tắt log query để console sạch hơn
});

const Tree = sequelize.define('TreeCollection', {
    id: {
        type: DataTypes.INTEGER,
        autoIncrement: true,
        primaryKey: true
    },
    treename: {
        type: DataTypes.STRING,
        allowNull: false
    },
    description: {
        type: DataTypes.TEXT,
        allowNull: false
    },
    image: {
        type: DataTypes.STRING,
        allowNull: true
    }
}, {
    tableName: 'treecollection', // Đặt tên bảng thành chữ thường
    timestamps: false
});

module.exports = { sequelize, Tree };
