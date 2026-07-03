require('dotenv').config();
const express = require('express');
const mongoose = require('mongoose');
const path = require('path');

const treeRoutes = require('./routes/treeRoutes');
const Tree = require('./models/Tree');
const plantSeed = require('./seed/plantSeed');

const app = express();
const PORT = process.env.PORT || 3000;

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

app.use(express.urlencoded({ extended: true, limit: '10mb' }));
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', treeRoutes);

app.use((err, req, res, next) => {
  console.error(err);
  res.status(500).send('Đã xảy ra lỗi.');
});

const MONGO_URI = process.env.MONGO_URI || 'mongodb://127.0.0.1:27017/TreeShop';

mongoose
  .connect(MONGO_URI)
  .then(async () => {
    console.log('Connected to MongoDB (TreeShop)');

    const count = await Tree.countDocuments();
    if (count === 0) {
      await Tree.insertMany(plantSeed);
      console.log(`Database rỗng, đã tự động thêm ${plantSeed.length} cây mẫu.`);
    }

    app.listen(PORT, () => console.log(`Server running at http://localhost:${PORT}`));
  })
  .catch((err) => console.error('MongoDB connection error:', err));
