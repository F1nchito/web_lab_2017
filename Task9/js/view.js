'use strict';
PRODUCTAPP.set('view', ['Product'], function (Product) {
    var tableTemplate = _.template($('#list-template').html()),
        editModalTemlate = _.template($('#edit-modal').html()),
        tableItemTemplate = _.template($('#list-item-template').html()),
        cityTemlate = _.template($('#city-template').html()),
        locationInfo = {
            Russia: ['Moscow', 'Saratov', 'Sochi'],
            USA: ['New York', 'Los-Angeles', 'Las-Vegas'],
            Japan: ['Tokio', 'Osaka', 'Yokohama']
        };

    function drawCities(country) {
        var cityHtml = cityTemlate({
            location: country,
            locationInfo: locationInfo
        });

        $('#city-choose').html(cityHtml);
    }

    function showModal(id, body) {
        var $modal = $(id),
            documentH = $(document).height(),
            windowH = $(window).height(),
            windowW = $(window).width();

        $('.overlay').css({
            'width': windowW,
            'height': documentH
        })
            .fadeTo(400, 0.4);
        if (body) {
            $modal.html(body);
        }
        $modal.css('top', windowH / 2 - $modal.height() / 2)
            .css('left', windowW / 2 - $modal.width() / 2)
            .fadeIn(200);
    };

    function closeModal(callback) {
        $('.overlay, .popup').hide();
        if (callback && callback.type !== 'click') {
            callback();
        }
    }

    function showEditModal(product) {
        if (product === undefined) {
            showModal('#popup', editModalTemlate({
                layout: {
                    header: 'New product',
                    button: 'Add'
                },
                product: new Product(),
                locationInfo: locationInfo
            }));
        } else {
            showModal('#popup', editModalTemlate({
                layout: {
                    header: 'Edit product',
                    button: 'Edit'
                },
                product: product,
                locationInfo: locationInfo
            }));
        }
    }

    function showErrors(errors) {
        errors.forEach(function (element) {
            var input = '#' + element.target,
                helpSpan = input + ' + span';

            $(helpSpan).html(element.msg);
            $(input).parent()
            .addClass('has-error');
        }, this);
    }

    function deleteErrors(element) {
        var input = '#' + element,
            helpSpan = input + ' + span';

        $(input).parent()
        .removeClass('has-error');
        $(helpSpan).html('');
    }

    function drawAll(array) {
        var tableHtml = tableTemplate({
            list: array,
            tableItemTemplate: tableItemTemplate
        });

        $('#table-body').html(tableHtml);
    }

    function drawProduct(product) {
        return tableItemTemplate(product);
    };
    return {
        deleteErrors: deleteErrors,
        drawCities: drawCities,
        showErrors: showErrors,
        showEditModal: showEditModal,
        closeModal: closeModal,
        showModal: showModal,
        drawAll: drawAll,
        drawProduct: drawProduct
    };
});