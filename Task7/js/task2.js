; (function () {
    "use strict";
    var button = document.getElementById("erase");
    button.addEventListener('click', changeOutput, false);
    function eraseRepeat(input) {
        var pattern = /[^\s.?,;:!]+/ig,
            escapePattern= /[\/\[\]\\\(\)]/,
            firstWord, count, i, j, char,
            mathes = [];
        mathes = input.match(pattern);
        if (mathes == null || mathes.length <= 1) {
            return input;
        }
        firstWord = mathes[0];
        for (i = 0; i < firstWord.length; i++) {
            if (escapePattern.test(firstWord[i])) {
                char = new RegExp('\\' + firstWord[i], "i");
            } else {
                char = new RegExp(firstWord[i], "i");
            }
            count = 0;
            for (j = 1; j < mathes.length; j++) {
                if (char.test(mathes[j])) {
                    count++;
                }
            }
            if (count === mathes.length - 1) {
                if (escapePattern.test(firstWord[i])) {
                    input = input.replace(new RegExp('\\' + firstWord[i], "ig"), '');
                }else{
                input = input.replace(new RegExp(firstWord[i], "ig"), '');
                }
            }
        }
        return input;
    }
    function changeOutput() {
        var input = document.getElementById("t2input").value;
        var output = eraseRepeat(input);
        document.getElementById("t2result").innerHTML = output;
    }
})();