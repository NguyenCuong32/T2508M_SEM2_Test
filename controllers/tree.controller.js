const { pool } = require('../config/db');

exports.getTrees = async (req, res) => {
  try {
    const [rows] = await pool.query('SELECT * FROM baithi');
    res.render('index', { trees: rows, errors: [] });
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.addTree = async (req, res) => {
  try {
    const { treename, description } = req.body;
    const errors = [];

    if (!treename || treename.trim() === '') {
      errors.push('Tree Name is required.');
    }
    if (!description || description.trim() === '') {
      errors.push('Description is required.');
    }

    if (errors.length > 0) {
      const [rows] = await pool.query('SELECT * FROM baithi');
      return res.render('index', { trees: rows, errors });
    }

    const imagePath = req.file ? '/uploads/' + req.file.filename : '';

    await pool.query(
      'INSERT INTO baithi (treename, description, image) VALUES (?, ?, ?)',
      [treename, description, imagePath]
    );

    res.redirect('/');
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.getEditTree = async (req, res) => {
  try {
    const [rows] = await pool.query('SELECT * FROM baithi WHERE id = ?', [req.params.id]);
    res.render('edit', { tree: rows[0], errors: [] });
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.postEditTree = async (req, res) => {
  try {
    const { treename, description } = req.body;
    const errors = [];

    if (!treename || treename.trim() === '') {
      errors.push('Tree Name is required.');
    }
    if (!description || description.trim() === '') {
      errors.push('Description is required.');
    }

    if (errors.length > 0) {
      const [rows] = await pool.query('SELECT * FROM baithi WHERE id = ?', [req.params.id]);
      return res.render('edit', { tree: rows[0], errors });
    }

    if (req.file) {
      const imagePath = '/uploads/' + req.file.filename;
      await pool.query(
        'UPDATE baithi SET treename = ?, description = ?, image = ? WHERE id = ?',
        [treename, description, imagePath, req.params.id]
      );
    } else {
      await pool.query(
        'UPDATE baithi SET treename = ?, description = ? WHERE id = ?',
        [treename, description, req.params.id]
      );
    }

    res.redirect('/');
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.deleteTree = async (req, res) => {
  try {
    await pool.query('DELETE FROM baithi WHERE id = ?', [req.params.id]);
    res.redirect('/');
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.resetDatabase = async (req, res) => {
  try {
    await pool.query('TRUNCATE TABLE baithi');
    res.redirect('/');
  } catch (err) {
    res.status(500).send(err.message);
  }
};

exports.getAboutMe = (req, res) => {
  res.render('aboutme');
};
