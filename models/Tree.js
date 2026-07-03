const mongoose = require('mongoose');

const treeSchema = new mongoose.Schema(
  {
    treename: { type: String, required: true, maxlength: 100 },
    description: { type: String, required: true, maxlength: 500 },
    image: { type: String, default: '' },
    imageRatio: { type: String, default: '' },
  },
  { collection: 'TreeCollection' }
);

module.exports = mongoose.model('Tree', treeSchema);
