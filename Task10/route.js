var express = require('express');
var app = express();
var router = express.Router();
var repo = require('./repo.js');
var Product = require('./product.js');
const validateSchema = require('./validate.js')

router.get('/api/:id', function (req, res) {
  var filteredRepo = repo.find(function (elem) {
    return elem.id === req.params.id;
  });
  res.send(filteredRepo);
});

router.get('/api', (req,res)=>res.send(repo));

router.post('/api',validateSchema('new-product'), (req,res) => {
    let product = new Product(req.body);
    repo.push(product);
    res.status(204).send();
});

router.delete('/api/:id',(req,res)=>{
    repo.splice(repo.findIndex(function(element){
    return element.id === req.params.id;
}),1);
res.status(204).send();
});

router.put('/api/:id',validateSchema('new-product'), (req,res)=>{
    var index = repo.findIndex(function (elem) {
    return elem.id === req.params.id;
  });
  repo[index] = new Product(req.body);
  repo[index].id = req.body.id;
  res.status(204).send();
})
module.exports = router;