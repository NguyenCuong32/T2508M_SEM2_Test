const mongoose = require("mongoose");

const treeSchema = new mongoose.Schema(
  {
    treename: {
      type: String,
      required: [true, "Tree Name is required"],
      trim: true
    },
    description: {
      type: String,
      required: [true, "Description is required"],
      trim: true
    },
    image: {
      type: String,
      trim: true,
      default: ""
    }
  },
  {
    timestamps: true,
    collection: "TreeCollection",
    bufferCommands: false
  }
);

module.exports = mongoose.model("Tree", treeSchema, "TreeCollection");
