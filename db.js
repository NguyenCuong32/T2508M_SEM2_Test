const mongoose = require('mongoose');
const fs = require('fs');
const path = require('path');

const MONGO_URI = 'mongodb://127.0.0.1:27017/TreeShop';
const DB_FILE = path.join(__dirname, 'db.json');

// Mongoose Schema
const treeSchema = new mongoose.Schema({
  treename: { type: String, required: true },
  description: { type: String, required: true },
  image: { type: String }
}, {
  collection: 'TreeCollection'
});

// JSON fallback DB class
class JSONDb {
  static readData() {
    try {
      if (!fs.existsSync(DB_FILE)) {
        // Seed initial data matching standard phonglan
        const initialData = [
          {
            _id: '1',
            treename: 'PhongLan',
            image: '/images/phonglan.png',
            description: 'Nhắc đến ý nghĩa của hoa phong lan, người ta nghĩ ngay đến sự giàu sang, quyến rũ của gia chủ bởi vẻ đẹp cùng sự quyến rũ của loài hoa này.'
          }
        ];
        fs.writeFileSync(DB_FILE, JSON.stringify(initialData, null, 2), 'utf8');
        return initialData;
      }
      const data = fs.readFileSync(DB_FILE, 'utf8');
      return JSON.parse(data);
    } catch (e) {
      console.error('Error reading JSON DB:', e);
      return [];
    }
  }

  static writeData(data) {
    try {
      fs.writeFileSync(DB_FILE, JSON.stringify(data, null, 2), 'utf8');
    } catch (e) {
      console.error('Error writing JSON DB:', e);
    }
  }

  static async find() {
    return this.readData();
  }

  static async findById(id) {
    const data = this.readData();
    return data.find(item => item._id === id.toString());
  }

  static async create(doc) {
    const data = this.readData();
    const newDoc = {
      _id: Date.now().toString(),
      treename: doc.treename,
      description: doc.description,
      image: doc.image || ''
    };
    data.push(newDoc);
    this.writeData(data);
    return newDoc;
  }

  static async findByIdAndUpdate(id, updateData) {
    const data = this.readData();
    const index = data.findIndex(item => item._id === id.toString());
    if (index !== -1) {
      data[index] = {
        ...data[index],
        treename: updateData.treename !== undefined ? updateData.treename : data[index].treename,
        description: updateData.description !== undefined ? updateData.description : data[index].description,
        image: updateData.image !== undefined ? updateData.image : data[index].image
      };
      this.writeData(data);
      return data[index];
    }
    return null;
  }

  static async findByIdAndDelete(id) {
    let data = this.readData();
    const removed = data.find(item => item._id === id.toString());
    data = data.filter(item => item._id !== id.toString());
    this.writeData(data);
    return removed;
  }

  static async deleteMany() {
    this.writeData([]);
    return { deletedCount: 0 };
  }
}

// Connect function
async function initializeDb() {
  let isConnected = false;
  let Model;

  try {
    console.log(`Connecting to MongoDB at ${MONGO_URI}...`);
    await mongoose.connect(MONGO_URI, {
      serverSelectionTimeoutMS: 3000
    });
    console.log('Successfully connected to MongoDB!');
    Model = mongoose.model('Tree', treeSchema);
    isConnected = true;
  } catch (err) {
    console.warn(`\n[WARNING] MongoDB connection failed: ${err.message}`);
    console.warn(`Falling back to JSON file database storage at: ${DB_FILE}\n`);
    Model = JSONDb;
    isConnected = false;
  }

  return { Model, isConnected };
}

module.exports = { initializeDb };
