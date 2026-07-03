import treeRepository from './tree.repository.js';

class TreeService {
  validateTreeData({ treename, description }) {
    const errors = [];

    if (!treename || !treename.trim()) {
      errors.push('Tree Name is required.');
    }

    if (!description || !description.trim()) {
      errors.push('Description is required.');
    }

    return errors;
  }

  async addTree({ treename, description, image }) {
    const errors = this.validateTreeData({ treename, description });

    if (errors.length > 0) {
      const validationError = new Error('Tree validation failed.');
      validationError.name = 'ValidationError';
      validationError.errors = errors;
      throw validationError;
    }

    return treeRepository.create({
      treename: treename.trim(),
      description: description.trim(),
      image: image ? image.trim() : '',
    });
  }

  async getAllTrees() {
    return treeRepository.findAll();
  }

  async resetTrees() {
    return treeRepository.deleteAll();
  }

  async deleteTreeById(id) {
    return treeRepository.deleteById(id);
  }
}

export default new TreeService();
