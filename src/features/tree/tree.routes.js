import express from 'express';
import multer from 'multer';
import path from 'node:path';
import treeController from './tree.controller.js';

const router = express.Router();

const storage = multer.diskStorage({
  destination: (_req, _file, cb) => cb(null, 'public/uploads'),
  filename: (_req, file, cb) => {
    const uniqueName = `${Date.now()}-${Math.round(Math.random() * 1e9)}${path.extname(file.originalname)}`;
    cb(null, uniqueName);
  },
});

const upload = multer({
  storage,
  fileFilter: (_req, file, cb) => {
    if (file.mimetype.startsWith('image/')) return cb(null, true);
    cb(new Error('Only image files are allowed.'));
  },
});

router.get('/', treeController.showTreeShop.bind(treeController));
router.get('/about', treeController.showAbout.bind(treeController));
router.post('/trees', upload.single('imageFile'), treeController.addTree.bind(treeController));
router.post('/trees/reset', treeController.resetTrees.bind(treeController));
router.post('/trees/:id/delete', treeController.deleteTree.bind(treeController));

export default router;
