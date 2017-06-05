'use strict';
PRODUCTAPP.set('Product', [], function () {
    function Product(name, email, count, price, delivery, country, city) {
        this.name = name;
        this.email = email;
        this.count = count;
        this.price = price;
        this.delivery = delivery;
        this.country = country;
        this.city = city;
    };

    return Product;
});