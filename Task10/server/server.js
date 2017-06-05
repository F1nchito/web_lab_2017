const express = require('express');
const repo = require('./repo.js').repo;
const app = express();
const fs = require('fs');
const router = require('./route.js');
const bodyParser = require('body-parser');
const cors = require('cors');
let writeStream = fs.createWriteStream('errors.txt', {
    'flags': 'a'
});
process.on('uncaughtException',(err)=> writeStream.write(`Caught exception: ${err}\n`));
app.listen(3000);
app.use(cors());
app.use(bodyParser.json());
app.use('/', router);
