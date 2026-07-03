import Tree from './tree.model.js';

class TreeRepository {
  create(treeData) {
    return Tree.create(treeData);
  }

  findAll() {
    return Tree.find().sort({ createdAt: 1 }).lean();
  }

  deleteAll() {
    return Tree.deleteMany({});
  }

  deleteById(id) {
    return Tree.findByIdAndDelete(id);
  }
}

export default new TreeRepository();
