const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const path = require('path');

const app = express();

mongoose.connect('mongodb://127.0.0.1:27017/TreeShop')
    .then(() => console.log('MongoDB connected'))
    .catch(err => console.log(err));

app.set('view engine', 'ejs');
app.use(express.static('public'));
app.use(bodyParser.urlencoded({ extended: true }));

const treeRoutes = require('./routes/treeRoutes');
app.use('/', treeRoutes);

app.listen(5500, () => {console.log('Server is running on http://localhost:5500')});