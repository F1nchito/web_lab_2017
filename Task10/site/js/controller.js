'use strict';
PRODUCTAPP.set('controller', ['Product', 'product_model', 'view', 'validator'], function (Product, product_model, view, validator) {
    var $tableContent = $('.table > tbody');

    $(init());

    function init() {
        getAll('nameAscending');
        view.closeModal(null);
        $tableContent.on('click', '[data-action="delete"]', deleteConfirmation)
            .on('click', '[data-action="edit"]', editModal)
            .on('click', '[data-action="name"]', editModal);
        $('#AddProduct').on('click', addModal);
        $('.overlay').on('click', null, view.closeModal);
        $('#table-head').on('click', '[data-sort]', sortHandler);
        $('#filter ~ button').on('click', filterHandler);
    };

    function deliveryHandler(e) {
        var value = e.target.value,
            id = e.target.id,
            $countryBlock = $('#country-choose'),
            $cityBlock = $('#city-choose');

        if (id === 'delivery-choose') {
            if (value === '') {
                $countryBlock.addClass('hidden');
                $cityBlock.addClass('hidden');
                return;
            } else if (value === 'Country') {
                $countryBlock.removeClass('hidden');
                $cityBlock.addClass('hidden');
                return;
            } else if (value === 'City') {
                $countryBlock.removeClass('hidden');
                $cityBlock.removeClass('hidden');
                return;
            }
        } else if (e.target.name === 'country') {
            view.drawCities(value);
            $('#delivery-choose').children('[value="City"]')
                .attr('selected', 'selected');
            $cityBlock.removeClass('hidden');
            return;
        } else if (e.target.name === 'city' || id === 'selectAll') {
            $('#delivery-choose').children('[value="City"]')
                .attr('selected', 'selected');
            if (id === 'selectAll') {
                if (e.target.checked) {
                    $('[name=city]').prop('checked', true);
                } else {
                    $('[name=city]').prop('checked', false);
                }
            }
        }
    };

    function sortHandler(e) {
        var sort = $(this).data('sort'),
            child = $(this).children(),
            filter = $('#filter').val();

        if (child.hasClass('desc')) {
            getAll(sort + 'Ascending', filter);
        } else {
            getAll(sort + 'Descending', filter);
        }
        child.toggleClass('desc');
    };

    function filterHandler(e) {
        e.preventDefault();
        var filter = $('#filter').val();

        getAll('nameAscending', filter);
    };

    function deleteConfirmation(e) {
        $('#productDelete [data-modal="confirm"]').on('click', deleteProduct)
            .data('action-id', $(this).parents('tr')
                .data('product-id'));
        $('#productDelete [data-modal="close"]').on('click', function (e) {
            e.preventDefault();
            view.closeModal(null);
        });
        view.showModal('#productDelete');
    };

    function validatePaste(e) {
        var $input = $(this),
            validateInput = {},
            newVal;

        newVal = e.originalEvent.clipboardData.getData('text');
        validateInput[$input.attr('name')] = newVal;
        if (validator.validate(validateInput)) {
            e.preventDefault();
            return;
        }
    }

    function validateSingle() {
        var $input = $(this),
            validateInput = {},
            error,
            $inputName = $input.attr('name'),
            value = $input.val();

        validateInput[$inputName] = value;
        if (validator.validate(validateInput)) {
            error = validator.getErrors();
            view.showErrors(error);
            return;
        } else if ($inputName === 'price') {
            $input.val(parseFloat(value).toDollarNotation());
        }
        view.deleteErrors($inputName);
    }

    function validateSymbol(e) {
        if (e.keyCode < 48 || e.keyCode > 57) {
            return false;
        }
    }

    function priceFormating(e) {
        var $priceInput = $(this),
            number,
            value = $priceInput.val();

        if ($priceInput.parent().hasClass('has-error')) {
            return;
        }
        if (value) {
            number = value.fromDollarNotation();
            if (number) {
                $priceInput.val(number);
            }
        }
    }

    function bindModel(object) {
        if (object.delivery === undefined) {
            object.coutry = undefined;
            object.city = undefined;
        }
        return new Product(object.name, object.email, +object.count, object.price, object.delivery, object.country, object.city);
    };

    function getAll(sort, filter) {
        product_model.getAll(sort, filter)
            .done(view.drawAll)
            .fail(function (error) {
                throw new Error(error);
            });
    };

    function getProductByID(id) {
        product_model.getProductByID(id)
            .done(view.drawProductInfo)
            .fail(function (error) {
                throw new Error(error);
            });
    };

    function addProduct(e) {
        var product,
            errors,
            productData = e.data.serializeObject();

        e.preventDefault();
        product = bindModel(productData);
        product.price = product.price.fromDollarNotation();
        if (validator.validate(product)) {
            errors = validator.getErrors();
            view.showErrors(errors);
            $('#' + errors[0].target).focus();
            return;
        }
        product_model.addProduct(bindModel(product))
            .done(function (result) {
                $('#filter').val('');
                view.closeModal(getAll('nameAscending'));
            })
            .fail(function (res) {
                errors = JSON.parse(res.responseText);
                view.showErrors(errors);
                return;
            });
    }

    function editProduct(e) {
        var productObj = e.data.form.serializeObject(),
            product,
            errors;

        e.preventDefault();
        product = bindModel(productObj);
        product.id = e.data.id;
        product.price = product.price.fromDollarNotation();
        if (validator.validate(product)) {
            errors = validator.getErrors();
            view.showErrors(errors);
            $('#' + errors[0].target).focus();
            return;
        }
        product_model.editProduct(product)
            .done(function (result) {
                $('#filter').val('');
                view.closeModal(getAll('nameAscending'));
            })
            .fail(function (res) {
                errors = JSON.parse(res.responseText);
                view.showErrors(errors);
                return;
            });
    };

    function deleteProduct(e) {
        var id = $(this).data('action-id');

        product_model.deleteProduct(id)
            .done(function (result) {
                $('#filter').val('');
                view.closeModal(getAll('nameAscending'));
            })
            .fail(function (error) {
                throw new Error(error);
            });
    };

    function editModal(e) {
        var $form,
            id = $(this).parents('tr')
            .data('product-id');

        product_model.getProductByID(id)
            .done(function (param) {
                view.showEditModal(param);
                $('#delivery-block').change(deliveryHandler);
                $form = $('#edit-product');
                $form.on('focusout', 'input', validateSingle);
                $('#count').keypress(validateSymbol);
                $('#count').on('paste', validatePaste);
                $('#price').on('focusin', priceFormating);
                $('.popup-header [data-modal="close"]').on('click', function (e) {
                    e.preventDefault();
                    view.closeModal(null);
                });
                $form.on('click', '[type="submit"]', {
                    form: $form,
                    id: id
                }, editProduct);
            });
    };

    function addModal(e) {
        var $form;

        view.showEditModal();
        $('#delivery-block').change(deliveryHandler);
        $form = $('#edit-product');
        $form.on('focusout', 'input', validateSingle);
        $('#count').keypress(validateSymbol);
        $('#count').on('paste', validatePaste);
        $('#price').on('focusin', priceFormating);
        $('.popup-header [data-modal="close"]').on('click', function (e) {
            e.preventDefault();
            view.closeModal(null);
        });
        $form.on('click', '[type="submit"]', $form, addProduct);
    }

    return {
        addProduct: addProduct,
        getAll: getAll,
        getProductByID: getProductByID,
        deleteProduct: deleteProduct,
        editProduct: editProduct
    };
});