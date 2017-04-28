 (function() {
     var select = document.getElementById("lang");
     selectLang();
     select.addEventListener("change", selectLang , false);
     function selectLang() {
     var lang = select.value;
     changeLang(lang);
     };
 
function changeLang(lang) {
        var en_US = {
        name: 'en-US',
        dayName: {
            full:['Sunday','Monday','Tuesday','Wednesday', 'Thursday','Friday','Saturday'],
            short:['Su','Mo','Tu','We','Th','Fr','Sa']
        },
        monthName: {
            full: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            short: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'Julе', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec']
        }
    };
        var ru_RU = {
        name: 'ru-RU',
        dayName: {
            full:['Воскресенье','Понедельник','Вторник','Среда', 'Четверг','Пятница','Суббота'],
            short:['Вс','Пн','Вт','Ср','Чт','Пт','Сб']
        },
        monthName: {
            full: ['Январь', 'Февраль', 'Март', 'Апрель', 'Мая', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            short: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек']
        }
    };
    var es_ES = {
        name: 'es-ES',
        dayName: {
            full:['Domingo','Lunes','Martes','Miércoles', 'Jueves','Viernes','Sábado'],
            short:['Do','Lu','Ma','Mi','Ju','Vi','Sá']
        },
        monthName: {
            full: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            short: ['enero', 'feb', 'marzo', 'abr', 'mayo', 'jun', 'jul', 'agosto', 'sept', 'oct', 'nov', 'dic']
        }
    };
    switch (lang) {
         case "en-US":
             return Date.CultureInfo = en_US;
             break;
        case "ru-RU":
             return Date.CultureInfo = ru_RU;
             break;
        case "es-ES":
             return Date.CultureInfo = es_ES;
             break;
         default:  return Date.CultureInfo = en_US;
             break;
     };
    };
})();
 
    
