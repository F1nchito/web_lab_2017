const Product = require('./product.js');
let repo = [];
repo.push(new Product({
    name: 'Apple',
    email: 'apple@gmail.com',
    count: 3,
    price: 100,
    delivery: 'Country',
    country: 'Russia'
}));
repo.push(new Product({
    name: 'Orange',
    email: 'orange@gmail.com',
    count: 10,
    price: 250,
    delivery: 'City',
    country: 'Russia',
    city: 'Saratov'
}));
repo.push(new Product({
    name: 'Watermelon',
    email: 'watermelon@gmail.com',
    count: 1,
    price: 1000,
    delivery: '',
}));
module.exports = repo;