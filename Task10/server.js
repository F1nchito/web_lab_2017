var express = require('express');
var repo = require('./repo.js').repo;
var app = express();
var router = require('./route.js');
var bodyParser = require('body-parser');
var cors = require('cors');

app.listen(3000);
app.use(cors());
app.use(bodyParser.json());
app.use('/', router);
