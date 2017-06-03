'use strict';
PRODUCTAPP.set('rules_config', ['validator'], function (validator) {
    var rules = {
        name: ['notEmpty',  'maxLength:15'],
        email: ['Email'],
        count: ['Number'],
        price: ['Float', 'maxLength:16']
    };

    validator.addRule('notEmpty',
        function (value) {
            return value && value.toString().trim().length !== 0;
        }, 'Field must be non-empty and not contain white spaces');

    validator.addRule('maxLength',
        function (value, max) {
            return value.toString().trim().length <= max;
        }, 'Length should be less then ');

    validator.addRule('Number',
        function (value) {
            return value && (/^(\d)*$/).test(value);
        }, 'Value must be a integer.');

    validator.addRule('Float',
        function (value) {
            return value && (/^(\d)*(\.\d*)?$/).test(value);
        }, 'Value must be a positive number.');

    validator.addRule('Email',
        function (value) {
            var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return reg.test(value);
        }, 'Incorrect email');

    validator.combineRules(rules);
});