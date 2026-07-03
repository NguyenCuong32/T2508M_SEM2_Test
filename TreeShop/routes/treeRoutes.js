const express = require("express");

const router = express.Router();

const treeController = require("../controllers/treeController");

router.get("/", treeController.home);

router.get("/about", treeController.about);

router.post("/add", treeController.addTree);

router.post("/reset", treeController.resetTree);

module.exports = router;