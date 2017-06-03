'use strict';
PRODUCTAPP.set('product_model', [], function () {
    function getAll(sort, filter) {
        var deferredObject = $.Deferred(),
            sortedRepo,
            repo,
            filteredRepo,
            filterRegexp;
        $.ajax('http://localhost:3000/api')
            .done(function (res) {
                repo = res;

                if (filter) {
                    filterRegexp = new RegExp(filter, 'i');
                    filteredRepo = _.filter(repo, function (elem) {
                        return filterRegexp.test(elem.name);
                    });
                } else {
                    filteredRepo = repo;
                }
                if (filteredRepo.length !== 0) {
                    switch (sort) {
                        case 'nameAscending':
                            sortedRepo = _.orderBy(filteredRepo, 'name', 'asc');
                            break;
                        case 'nameDescending':
                            sortedRepo = _.orderBy(filteredRepo, 'name', 'desc');
                            break;
                        case 'priceDescending':
                            sortedRepo = _.orderBy(filteredRepo, 'price', 'desc');
                            break;
                        case 'priceAscending':
                            sortedRepo = _.orderBy(filteredRepo, 'price', 'asc');
                            break;
                        default:
                            break;
                    }
                    deferredObject.resolve(sortedRepo);
                } else {
                    deferredObject.reject('empty');
                }
            }); //TODO: сделать логирование и на неудачу
        return deferredObject.promise();
    }

    function getProductByID(id) {
        var deferredObject = $.Deferred(),
            product;

        // product = _.find(repo, function (p) {
        //     return p.id === id;
        // });
        $.ajax('http://localhost:3000/api/' + id)
            .done(function (responce) {
                product = responce;
                if (product !== undefined) {
                    deferredObject.resolve(product);
                } else {
                    deferredObject.reject('Product with ' + id + 'id not found');
                }
            })
            .fail(function (res) {
                console.log(res);
            });

        return deferredObject.promise();
    }

    function addProduct(product) {
        var deferredObject = $.Deferred();
       
        $.ajax({
            url:'http://localhost:3000/api/',
            method: 'POST',
            data :JSON.stringify(product),
            dataType: 'json',
            contentType :'application/json'
        })
        .done(function (params) {
            deferredObject.resolve(product);
        })
        .fail(function (res) {
            deferredObject.reject(res);
        });
        // if (_.includes(repo, product)) {
        //     deferredObject.reject('Product with ' + product.id + 'already consists');
        // } else {
        //     repo.push(product);
        //     deferredObject.resolve(product);
        // }
        return deferredObject.promise();
    };

    function deleteProduct(id) {
        var deferredObject = $.Deferred();
$.ajax({
    url: 'http://localhost:3000/api/' + id,
    method: 'DELETE'
})
.done(function (params) {
    deferredObject.resolve(params);
});
        // getProductByID(id).done(function (product) {
        //         _.remove(repo, function (elem) {
        //             return elem.id === id;
        //         });
        //     })
        //     .then(function () {
        //         deferredObject.resolve('done');
        //     });
        return deferredObject.promise();
    };

    function editProduct(product) {
        var deferredObject = $.Deferred();
        $.ajax({
            url: 'http://localhost:3000/api/' + product.id,
            method: 'PUT',
            data :JSON.stringify(product),
            dataType: 'json',
            contentType :'application/json'
        })
        .done(function (params) {
            deferredObject.resolve(params);
        });
        // getProductByID(product.id).done(function (oldProduct) {
        //     oldProduct.name = product.name;
        //     oldProduct.email = product.email;
        //     oldProduct.count = product.count;
        //     oldProduct.price = product.price;
        //     oldProduct.delivery = product.delivery;
        //     oldProduct.country = product.country;
        //     oldProduct.city = product.city;
        // });
        return deferredObject.promise();
    }
    return {
        editProduct: editProduct,
        addProduct: addProduct,
        getAll: getAll,
        getProductByID: getProductByID,
        deleteProduct: deleteProduct
    };
});