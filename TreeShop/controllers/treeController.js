const Tree = require("../models/Tree");

// Hiển thị trang chủ
const home = async (req, res) => {
    try {
        const trees = await Tree.find();

        res.render("index", {
            trees,
            error: "",
            success: ""
        });

    } catch (err) {
        console.log(err);
    }
};

// Trang About
const about = (req, res) => {
    res.render("about");
};

// Thêm cây
const addTree = async (req, res) => {

    const { treename, description, image } = req.body;

    // Validate
    if (!treename || !description) {

        const trees = await Tree.find();

        return res.render("index", {
            trees,
            error: "Tree Name và Description không được để trống!",
            success: ""
        });
    }

    try {

        await Tree.create({
            treename,
            description,
            image
        });

        const trees = await Tree.find();

        res.render("index", {
            trees,
            error: "",
            success: "Thêm cây thành công!"
        });

    } catch (err) {

        console.log(err);

        res.redirect("/");
    }

};

// Reset
const resetTree = async (req, res) => {

    try {

        await Tree.deleteMany({});

        res.redirect("/");

    } catch (err) {

        console.log(err);

    }

};

module.exports = {
    home,
    about,
    addTree,
    resetTree
};
