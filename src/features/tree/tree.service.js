import treeRepository from './tree.repository.js';
import fs from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);
const uploadDir = path.join(__dirname, '..', '..', '..', 'public', 'uploads');

class TreeService {
  async deleteUploadedImage(image) {
    if (!image || !image.startsWith('/uploads/')) return;

    const fileName = path.basename(image);
    const filePath = path.join(uploadDir, fileName);

    try {
      await fs.unlink(filePath);
    } catch (error) {
      if (error.code !== 'ENOENT') throw error;
    }
  }

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
    const trees = await treeRepository.findAll();

    await Promise.all(trees.map((tree) => this.deleteUploadedImage(tree.image)));

    return treeRepository.deleteAll();
  }

  async deleteTreeById(id) {
    const deletedTree = await treeRepository.deleteById(id);

    if (deletedTree) {
      await this.deleteUploadedImage(deletedTree.image);
    }

    return deletedTree;
  }
}

export default new TreeService();
