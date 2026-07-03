const { Sequelize, DataTypes } = require('sequelize');

// Khởi tạo kết nối Sequelize sử dụng POSTGRES_URI từ file .env
const sequelize = new Sequelize(process.env.POSTGRES_URI, {
    dialect: 'postgres',
    logging: false
});

const Tree = sequelize.define('Tree', {
    treename: {
        type: DataTypes.STRING,
        allowNull: false
    },
    description: {
        type: DataTypes.TEXT,
        allowNull: false
    },
    image: {
        type: DataTypes.STRING
    }
});

// Export sequelize và model Tree
module.exports = { sequelize, Tree };
