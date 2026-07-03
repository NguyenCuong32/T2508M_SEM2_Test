const { Client } = require('pg');
const client = new Client({ user: 'postgres', password: 'hung404*', host: 'localhost', port: 5432, database: 'postgres' });
client.connect()
  .then(() => client.query('CREATE DATABASE "TreeShop"'))
  .then(() => console.log('Database created'))
  .catch(e => console.log(e.message))
  .finally(() => client.end());
