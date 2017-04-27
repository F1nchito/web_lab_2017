;(function () {
    "use strict";
    var button,
        patterns = {
                    numbers : /\-?[1-9](?:\.\d+)?[e][+-]?\d+|\-?(?:[1-9]+\d*)?[0-9](?:\.\d+)?/i,
                    operator : /[\+\-\*\/\=]/i,
                    equal: /\=/ig
                };
    button = document.getElementById("calculate");
    button.addEventListener('click', writeResult, false);
    function writeResult() {
        var input, result = 0;
        input = document.getElementById("t1input").value;
        try {
        validateString(input);
        result = calculateInput(input);
        } catch (error) {
            result = error;
        }
        document.getElementById("t1result").innerHTML = result;
    };
    
    function validateString(input) {
        var equalMatches;
        equalMatches = input.match(patterns.equal);
        if (equalMatches == null) {
            throw new Error("Required '='");
        } else if (equalMatches.length !== 1) {
            throw new Error('More than one "="');
        }
    };

    function parseString(input) {
        var matches = [],i;
        for (i = 0; ; i++) {
            if (i % 2 === 0) {
                matches[i] = input.match(patterns.numbers);
                if(matches[i] == null){
                    throw new Error("Empty number on posititon "+ i);
                }
                if (i != 0 && input.substring(0, matches[i].index).match(patterns.operator) !== null) {
                    if (input.substring(0, matches[i].index).match(/\./) !== null) {
                        throw new Error("Error! '.' not allowed between number and operator");
                    }
                    throw new Error("Error! 2nd operator between numbers");
                }
                input = input.substring(matches[i].index + matches[i][0].length);
                matches[i][0] = parseFloat(matches[i][0]);
                if (matches[i][0].isNaN) {
                    throw new Error(i + " element not a number");
                }
            } else {
                matches[i] = input.match(patterns.operator);
                if (matches[i][0] == "=") {
                    break;
                }
                input = input.substring(matches[i].index + matches[i][0].length);
            }
        }
        return matches;
    };
    
    function calculateInput(input) {
        var result = 0,
            matches = [],
            j;
        matches = parseString(input);
        result += matches[0][0];
        for (j = 1; j < matches.length; j++) {
            switch (matches[j][0]) {
                case "+": result += matches[j + 1][0]; break;
                case "-": result -= matches[j + 1][0]; break;
                case "*": result *= matches[j + 1][0]; break;
                case "/": result /= matches[j + 1][0]; break;
                case "=": break;
                default: continue; break;
            }
        }
        return result.toFixed(2);
    };
})();