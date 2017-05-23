'use strict';
PRODUCTAPP.set('validator', [], function () {
    var ruleSeparator = ':',
        rules = {},
        errors = [],
        config = {};

    function validate(data) {
        var obj,
            type,
            checker,
            args,
            result,
            typesFor;

        errors = [];
        for (obj in data) {
            if (data.hasOwnProperty(obj)) {
                typesFor = config[obj];
            }
            if (!typesFor) {
                continue;
            }
            typesFor.forEach(function (element) {
                type = element.split(ruleSeparator);
                checker = rules[type[0]];
                args = type[1];
                result = checker.validateFunc(data[obj], args);
                if (!result) {
                    if (args) {
                        errors.push({
                            target: obj,
                            msg: checker.errorMessage + args
                        });
                    } else {
                        errors.push({
                            target: obj,
                            msg: checker.errorMessage
                        });
                    }
                }
            }, this);
        }
        return errors.length !== 0;
    }

    function getErrors() {
        return errors;
    }

    function Rule(validateFunc, message, arg) {
        this.validateFunc = validateFunc;
        this.errorMessage = message;
    }

    function addRule(rule, func, msg) {
        if (rules.hasOwnProperty(rule)) {
            console.error('Error: ' + rule + 'already exist');
            return;
        }
        rules[rule] = new Rule(func, msg);
    }

    function combineRules(obj) {
        var target,
            i;

        for (target in obj) { //TODO: check rules
            if (!config[target]) {
                config[target] = [];
            }
            for (i = 0; i < obj[target].length; i++) {
                config[target].push(obj[target][i]);
            }
        }
    };
    return {
        combineRules: combineRules,
        addRule: addRule,
        getErrors: getErrors,
        validate: validate
    };
});