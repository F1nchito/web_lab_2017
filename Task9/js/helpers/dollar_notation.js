'use strict';
PRODUCTAPP.set('dollar_notation', [], function () {
    Number.prototype.toDollarNotation = function () {
        return '$' + this.toLocaleString('en-US');
    };

    String.prototype.fromDollarNotation = function () {
        var number = _.replace(this, /,/g, '');

        number = _.trimStart(number, '$');
        number = +number;
        if (typeof number === 'number') {
            return number;
        }
        return false;
    };
});