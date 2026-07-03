import mongoose from 'mongoose';

const treeSchema = new mongoose.Schema(
  {
    treename: {
      type: String,
      required: true,
      trim: true,
    },
    description: {
      type: String,
      required: true,
      trim: true,
    },
    image: {
      type: String,
      default: '',
      trim: true,
    },
  },
  {
    collection: 'TreeCollection',
    timestamps: true,
  },
);

const Tree = mongoose.model('Tree', treeSchema);

export default Tree;
