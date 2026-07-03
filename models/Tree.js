const mongoose = require('mongoose');

// Collection Name: TreeCollection
// Fields: treename (String), description (String), image (String)
const treeSchema = new mongoose.Schema({
    treename: { type: String, required: true },
    description: { type: String, required: true },
    image: { type: String }
});

module.exports = mongoose.model('Tree', treeSchema, 'TreeCollection');