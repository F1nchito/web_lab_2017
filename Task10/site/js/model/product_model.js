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
            })
            .fail(function (params) {
                throw new Error(params);
            });
        return deferredObject.promise();
    }

    function getProductByID(id) {
        var deferredObject = $.Deferred(),
            product;

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
                deferredObject.reject(res);
            });

        return deferredObject.promise();
    }

    function addProduct(product) {
        var deferredObject = $.Deferred();

        $.ajax({
            url: 'http://localhost:3000/api/',
            method: 'POST',
            data: JSON.stringify(product),
            dataType: 'json',
            contentType: 'application/json'
        })
            .done(function (params) {
                deferredObject.resolve(product);
            })
            .fail(function (res) {
                deferredObject.reject(res);
            });
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
            })
            .fail(function (params) {
                deferredObject.reject(params);
            });
        return deferredObject.promise();
    };

    function editProduct(product) {
        var deferredObject = $.Deferred();

        $.ajax({
            url: 'http://localhost:3000/api/' + product.id,
            method: 'PUT',
            data: JSON.stringify(product),
            dataType: 'json',
            contentType: 'application/json'
        })
            .done(function (params) {
                deferredObject.resolve(params);
            })
            .fail(function (res) {
                deferredObject.reject(res);
            });
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