const express = require('express');
const multer = require('multer');
const path = require('path');
const fs = require('fs');
const Tree = require('../models/Tree');

const router = express.Router();

const uploadDir = path.join(__dirname, '..', 'public', 'uploads');

const storage = multer.diskStorage({
  destination: (req, file, cb) => cb(null, uploadDir),
  filename: (req, file, cb) => cb(null, Date.now() + '-' + file.originalname),
});
const upload = multer({ storage });

// Remove an uploaded image file from disk (ignore any errors).
function removeImage(image) {
  if (!image) return;
  const filePath = path.join(__dirname, '..', 'public', image.replace(/^\//, ''));
  fs.unlink(filePath, () => {});
}

router.get('/', async (req, res) => {
  const trees = await Tree.find().sort({ _id: 1 });
  res.render('index', { trees, error: null, msg: req.query.msg || null });
});

router.get('/about', (req, res) => {
  res.render('about');
});

router.post('/add', upload.single('image'), async (req, res) => {
  const { treename, description } = req.body;

  if (!treename || !treename.trim() || !description || !description.trim()) {
    const trees = await Tree.find().sort({ _id: 1 });
    return res.status(400).render('index', {
      trees,
      error: 'Tree Name and Description are required.',
      msg: null,
    });
  }

  const image = req.file ? '/uploads/' + req.file.filename : '';
  await Tree.create({ treename: treename.trim(), description: description.trim(), image });
  res.redirect('/?msg=added');
});

router.get('/edit/:id', async (req, res) => {
  const tree = await Tree.findById(req.params.id);
  if (!tree) return res.redirect('/');
  res.render('edit', { tree, error: null });
});

router.post('/edit/:id', upload.single('image'), async (req, res) => {
  const { treename, description } = req.body;
  const tree = await Tree.findById(req.params.id);
  if (!tree) return res.redirect('/');

  if (!treename || !treename.trim() || !description || !description.trim()) {
    return res.status(400).render('edit', {
      tree,
      error: 'Tree Name and Description are required.',
    });
  }

  const update = { treename: treename.trim(), description: description.trim() };
  if (req.file) {
    removeImage(tree.image);
    update.image = '/uploads/' + req.file.filename;
  }

  await Tree.findByIdAndUpdate(req.params.id, update);
  res.redirect('/?msg=updated');
});

router.post('/delete/:id', async (req, res) => {
  const tree = await Tree.findByIdAndDelete(req.params.id);
  if (tree) removeImage(tree.image);
  res.redirect('/?msg=deleted');
});

router.post('/reset', async (req, res) => {
  const trees = await Tree.find();
  trees.forEach((t) => removeImage(t.image));
  await Tree.deleteMany({});
  res.redirect('/?msg=reset');
});

module.exports = router;
