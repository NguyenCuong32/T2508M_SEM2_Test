const express = require('express');
const router = express.Router();
const Tree = require('../Model/Tree');

//HomePage
router.get('/', async (req, res) => {
    const trees = await Tree.find();
    res.render('index', { trees });
});

//Add a tree    
router.post('/add', async (req, res) => {
    const { treename, description, image } = req.body;

    if (!treename || !description) {
        return res.send('Treename and description are required!');
    }

    await Tree.create({ treename, description, image });
    res.redirect('/');
});

//Reset
router.post('/reset', async (req, res) => {
    await Tree.deleteMany({});
    res.redirect('/');
});

//About
router.get('/about', (req, res) => {
    res.render('about');
});

module.exports = router;