const uuid = require('uuid/v4');

function Product(obj) {
    this.id = uuid();
    this.name = obj.name;
    this.email = obj.email;
    this.count = obj.count;
    this.price = obj.price;
    this.delivery = obj.delivery;
    this.country = obj.country;
    this.city = obj.city;
};

module.exports = Product;