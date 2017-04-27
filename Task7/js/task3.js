;(function() {
    "use strict";
    var button = document.getElementById("insert");
    button.addEventListener('click', changeOutput, false);
    function changeOutput(){
        var input, format, output, result;
        input = document.getElementById("t3input").value;
        format = document.getElementById("t3inputformat").value;
        try {
        output = parseDate(input);
        result = output.format(format);    
        } catch (error) {
            result = error;
        }
        document.getElementById("t3result").innerHTML = result;
    }

    function parseDate(input) {
        var dateParts=[],
        regex = {
            DatePart: /\d+/g,
            ValidInput:/^(?:new Date\()?\d{4}([,\s]{1,2})?(\d{1,2}\1?){0,5}/m
        };
        dateParts = input.match(regex.DatePart);
        var q = regex.ValidInput.test(input);
        if(dateParts == null || !regex.ValidInput.test(input)){
            throw new Error("Wrong date!");
        }else if(dateParts != null && dateParts[0] !== '' && dateParts.length > 0){
            return new Date(dateParts[0] || 0,dateParts[1] || 0 ,dateParts[2] || 0,dateParts[3] || 0,dateParts[4] || 0,dateParts[5] || 0);
        }
    }

    Date.prototype.getNameOfDay =  function(short) {
        return short ? Date.CultureInfo.dayName.short[this.getDay()] : Date.CultureInfo.dayName.full[this.getDay()];
    }

    Date.prototype.getNameOfMonth =  function(short) {
        return short ? Date.CultureInfo.monthName.short[this.getMonth()] : Date.CultureInfo.monthName.full[this.getMonth()];
    }

    Date.prototype.format = function(format) {
        var date = this;
        if(!format){
            throw new Error("Empty format");
        }
        var short = function short(s){
            return s.toString().length === 1 ? '0'+s : s;
        }
        if(date == null){
            throw new Error("Date is empty!");
        }else if(date instanceof Date){
            var result = format.replace(/yy(?:yy)?|d{1,4}|M{1,4}|(h|H|m|s)(\1)?/g, function(format){
                switch (format) {
                    case 'hh':
                        return date.getHours()< 13 ? short(date.getHours())+"AM" : short(date.getHours()-12)+"PM";
                        break;
                    case 'h':
                        return date.getHours()< 13 ? date.getHours()+"AM" : (date.getHours()-12)+"PM";
                        break;
                    case 'HH':
                        return short(date.getHours());
                        break;
                    case 'H':
                        return date.getHours();
                        break;
                    case 'mm':
                        return short(date.getMinutes());
                        break;
                    case 'm':
                        return date.getMinutes();
                        break;
                    case 'ss':
                        return short(date.getSeconds());
                        break;
                    case 's':
                        return date.getSeconds();
                        break;
                    case 'yyyy':
                        return date.getFullYear();
                        break; 
                    case 'yy':
                        return date.getFullYear().toString().substring(2,4);
                        break;
                    case 'dddd':
                        return date.getNameOfDay();
                        break;
                    case 'ddd':
                        return date.getNameOfDay(true);
                        break;
                    case 'dd':
                        return short(date.getDate());
                        break;
                     case 'd':
                        return date.getDate().toString();
                        break;
                    case 'MMMM':
                        return date.getNameOfMonth();
                        break;
                    case 'MMM':
                        return date.getNameOfMonth(true);
                        break;
                    case 'MM':
                        return short(date.getMonth()+1);
                        break;
                    case 'M':
                        return date.getMonth()+1;
                        break;                          
                    default:
                    return format;
                        break;
                }
            });
            return result;
        }
    }
})();