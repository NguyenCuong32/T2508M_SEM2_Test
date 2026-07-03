const express = require("express");
const router = express.Router();

const Tree = require("../models/Tree");

router.get("/", async (req, res) => {

    try {

        const page = parseInt(req.query.page) || 1;
        const limit = 5;

        const totalTrees = await Tree.countDocuments();

        const totalPages = Math.ceil(totalTrees / limit);

        const trees = await Tree.find()
            .skip((page - 1) * limit)
            .limit(limit);

        res.render("index", {
            trees,
            currentPage: page,
            totalPages
        });

    } catch (error) {

        console.log(error);
        res.send("Error");

    }

});

router.get("/add", (req, res) => {
    res.render("add");
});

router.post("/add", async (req, res) => {

    const { treename, description, image } = req.body;

    if (!treename || !description) {
        return res.render("add", {
            error: "Tree Name and Description are required!"
        });
    }

    try {

        const tree = new Tree({
            treename,
            description,
            image
        });

        await tree.save();

        res.redirect("/");

    } catch (error) {
        console.log(error);
        res.send("Error");
    }

});

router.get("/about", (req, res) => {
    res.render("about");
});

router.get("/reset", async (req, res) => {

    try {

        await Tree.deleteMany({});

        res.redirect("/");

    } catch (error) {

        console.log(error);

        res.send("Error");

    }

});

module.exports = router;