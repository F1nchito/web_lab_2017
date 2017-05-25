'use strict';
PRODUCTAPP.set('repo', ['Product'], function (Product) {
    var repo = [];

    repo.push(new Product('Apple', 'apple@ya.ru', 3, 100, 'Country', 'Russia', ''));
    repo.push(new Product('Orange', 'ora@gmail.com', 10, 299, 'City', 'Russia', 'Saratov'));
    repo.push(new Product('Watermelon', 'ora@gmail.com', 1, 500));
    return repo;
});