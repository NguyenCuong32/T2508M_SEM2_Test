const { DataTypes } = require('sequelize');
const { sequelize } = require('../config/db');

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
        type: DataTypes.STRING,
        allowNull: false
    }
}, {
    tableName: 'trees'
});

module.exports = Tree;
