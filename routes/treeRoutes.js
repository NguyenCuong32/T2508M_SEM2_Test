const express = require('express');
const mongoose = require('mongoose');
const router = express.Router();
const Tree = require('../models/Tree');

router.param('id', (req, res, next, id) => {
  if (!mongoose.Types.ObjectId.isValid(id)) return res.redirect('/');
  next();
});

const asyncHandler = (fn) => (req, res, next) => fn(req, res, next).catch(next);

const TREENAME_MAX_LENGTH = 100;
const DESCRIPTION_MAX_LENGTH = 500;
const MAX_IMAGE_DATA_URL_LENGTH = 8 * 1024 * 1024; // ~6MB ảnh nhị phân sau khi giải base64

function validateTree(treename, description, image) {
  if (!treename || !treename.trim() || !description || !description.trim()) {
    return 'Tree Name và Description là bắt buộc.';
  }
  if (treename.trim().length > TREENAME_MAX_LENGTH) {
    return `Tree Name không được vượt quá ${TREENAME_MAX_LENGTH} ký tự.`;
  }
  if (description.trim().length > DESCRIPTION_MAX_LENGTH) {
    return `Description không được vượt quá ${DESCRIPTION_MAX_LENGTH} ký tự.`;
  }
  if (image && image.startsWith('data:') && image.length > MAX_IMAGE_DATA_URL_LENGTH) {
    return 'Ảnh vượt quá dung lượng cho phép. Vui lòng chọn ảnh nhỏ hơn.';
  }
  return null;
}

function escapeRegExp(str) {
  return str.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}

async function findDuplicateName(treename, excludeId) {
  const query = { treename: new RegExp(`^${escapeRegExp(treename.trim())}$`, 'i') };
  if (excludeId) query._id = { $ne: excludeId };
  return Tree.findOne(query);
}

// GET /api/check-name - Live duplicate name check
router.get('/api/check-name', asyncHandler(async (req, res) => {
  const { name, excludeId } = req.query;
  if (!name || !name.trim()) return res.json({ exists: false });
  const validExcludeId = excludeId && mongoose.Types.ObjectId.isValid(excludeId) ? excludeId : null;
  const duplicate = await findDuplicateName(name, validExcludeId);
  res.json({ exists: !!duplicate });
}));

// GET / - Show all trees
router.get('/', asyncHandler(async (req, res) => {
  const trees = await Tree.find().sort({ _id: -1 });
  res.render('index', { trees, error: null, formData: {} });
}));

// POST /add - Add a new tree (with validation)
router.post('/add', asyncHandler(async (req, res) => {
  const { treename, description, image, imageRatio } = req.body;

  let validationError = validateTree(treename, description, image);
  if (!validationError && (await findDuplicateName(treename))) {
    validationError = `Tên cây "${treename.trim()}" đã tồn tại. Vui lòng chọn tên khác để dễ phân biệt.`;
  }
  if (validationError) {
    const trees = await Tree.find().sort({ _id: -1 });
    return res.render('index', {
      trees,
      error: validationError,
      formData: { treename, description, image, imageRatio },
    });
  }

  await Tree.create({ treename: treename.trim(), description: description.trim(), image, imageRatio });
  res.redirect('/');
}));

// GET /edit/:id - Show edit form for a tree
router.get('/edit/:id', asyncHandler(async (req, res) => {
  const tree = await Tree.findById(req.params.id);
  if (!tree) return res.redirect('/');
  res.render('edit', { tree, error: null });
}));

// POST /edit/:id - Update a tree (with validation)
router.post('/edit/:id', asyncHandler(async (req, res) => {
  const { treename, description, image, imageRatio } = req.body;

  let validationError = validateTree(treename, description, image);
  if (!validationError && (await findDuplicateName(treename, req.params.id))) {
    validationError = `Tên cây "${treename.trim()}" đã tồn tại. Vui lòng chọn tên khác để dễ phân biệt.`;
  }
  if (validationError) {
    return res.render('edit', {
      tree: { _id: req.params.id, treename, description, image, imageRatio },
      error: validationError,
    });
  }

  await Tree.findByIdAndUpdate(req.params.id, {
    treename: treename.trim(),
    description: description.trim(),
    image,
    imageRatio,
  });
  res.redirect('/');
}));

// POST /delete/:id - Delete a single tree
router.post('/delete/:id', asyncHandler(async (req, res) => {
  await Tree.findByIdAndDelete(req.params.id);
  res.redirect('/');
}));

// POST /reset - Delete all trees
router.post('/reset', asyncHandler(async (req, res) => {
  await Tree.deleteMany({});
  res.redirect('/');
}));

// GET /about - About me page
router.get('/about', (req, res) => {
  res.render('about');
});

module.exports = router;
