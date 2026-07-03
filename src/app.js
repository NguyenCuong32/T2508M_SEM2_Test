import express from 'express';
import mongoose from 'mongoose';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
import treeRoutes from './features/tree/tree.routes.js';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const port = process.env.PORT || 3000;
const mongoUri = process.env.MONGO_URI || 'mongodb://localhost:27017/TreeShop';

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, '..', 'views'));

app.use(express.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, '..', 'public')));

app.use('/', treeRoutes);

app.use((error, _req, res, _next) => {
  console.error(error);
  res.status(500).send('Server error. Please check the application logs.');
});

async function startServer() {
  await mongoose.connect(mongoUri, {
    dbName: 'TreeShop',
  });

  app.listen(port, () => {
    console.log(`TreeShop is running at http://localhost:${port}`);
  });
}

startServer().catch((error) => {
  console.error('Cannot start server:', error);
  process.exit(1);
});
