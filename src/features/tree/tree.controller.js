import treeService from './tree.service.js';

class TreeController {
  async showTreeShop(req, res, next) {
    try {
      const trees = await treeService.getAllTrees();

      res.render('index', {
        pageTitle: 'Tree Shop',
        trees,
        errors: [],
        formData: {},
      });
    } catch (error) {
      next(error);
    }
  }

  showAbout(req, res) {
    res.render('about', { pageTitle: 'About me' });
  }

  async addTree(req, res, next) {
    const formData = {
      treename: req.body.treename ?? '',
      description: req.body.description ?? '',
      image: req.body.image ?? '',
    };

    try {
      const uploadedPath = req.file ? `/uploads/${req.file.filename}` : '';

      await treeService.addTree({
        ...formData,
        image: uploadedPath || formData.image,
      });

      res.redirect('/');
    } catch (error) {
      if (error.name === 'ValidationError') {
        const trees = await treeService.getAllTrees();

        return res.status(400).render('index', {
          pageTitle: 'Tree Shop',
          trees,
          errors: error.errors,
          formData,
        });
      }

      next(error);
    }
  }

  async resetTrees(req, res, next) {
    try {
      await treeService.resetTrees();
      res.redirect('/');
    } catch (error) {
      next(error);
    }
  }

  async deleteTree(req, res, next) {
    try {
      await treeService.deleteTreeById(req.params.id);
      res.redirect('/');
    } catch (error) {
      next(error);
    }
  }
}

export default new TreeController();
