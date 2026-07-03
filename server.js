const express = require('express');
const path = require('path');
const { connectDB } = require('./config/db');
const treeRoutes = require('./routes/tree.route');

const app = express();

connectDB();

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

app.use(express.static(path.join(__dirname, 'public')));
app.use(express.urlencoded({ extended: true }));

app.use('/', treeRoutes);

app.listen(3000, () => {
  console.log('Server is running on port 3000');
});
