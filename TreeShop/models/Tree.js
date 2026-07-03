const mongoose = require("mongoose");

const treeSchema = new mongoose.Schema(
  {
    treename: {
      type: String,
      required: true,
    },

    description: {
      type: String,
      required: true,
    },

    image: {
      type: String,
      default:
        "https://images.unsplash.com/photo-1441974231531-c6227db76b6e",
    },
  },
  {
    collection: "TreeCollection",
  }
);

module.exports = mongoose.model("Tree", treeSchema);