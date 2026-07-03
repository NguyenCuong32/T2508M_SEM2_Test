const express = require('express');
const multer = require('multer');
const treeController = require('../controllers/tree.controller');

const router = express.Router();

const storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, 'public/uploads/');
  },
  filename: function (req, file, cb) {
    cb(null, Date.now() + '-' + file.originalname);
  }
});

const upload = multer({ storage: storage });

router.get('/', treeController.getTrees);
router.post('/add', upload.single('imageFile'), treeController.addTree);
router.get('/edit/:id', treeController.getEditTree);
router.post('/edit/:id', upload.single('imageFile'), treeController.postEditTree);
router.get('/delete/:id', treeController.deleteTree);
router.post('/reset', treeController.resetDatabase);
router.get('/aboutme', treeController.getAboutMe);

module.exports = router;
