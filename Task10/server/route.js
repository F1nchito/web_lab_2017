const express = require('express');
const app = express();
const router = express.Router();
const repo = require('./repo.js');
const Product = require('./product.js');
const validateSchema = require('./validate.js');

router.get('/api/:id', function (req, res) {
  var filteredRepo = repo.find(function (elem) {
    return elem.id === req.params.id;
  });
  if (filteredRepo === undefined || filteredRepo.length === 0 ) {
    res.status(404).send();
  } else {
    res.send(filteredRepo);
  }
});

router.get('/api', (req, res) => res.send(repo));

router.post('/api', validateSchema('new-product'), (req, res) => {
  let product = new Product(req.body);
  repo.push(product);
  res.status(204).send();
});

router.delete('/api/:id', (req, res) => {
  let index = repo.findIndex(function (element) {
    return element.id === req.params.id;
  });
  if (index === -1) {
    res.status(404).send();
  } else {
    repo.splice(index, 1);
    res.status(204).send();
  }
});

router.put('/api/:id', validateSchema('new-product'), (req, res) => {
  var index = repo.findIndex(function (elem) {
    return elem.id === req.params.id;
  });
  if (index === -1) {
    res.status(404).send();
  } else {
    repo[index] = new Product(req.body);
    repo[index].id = req.params.id;
    res.status(204).send();
  }
})
module.exports = router;