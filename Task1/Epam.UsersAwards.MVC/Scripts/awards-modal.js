(function () {
    "use strict";
    $(function () {
        $.ajaxSetup({ cache: false });
        $(".modalItem").click(function (e) {

            e.preventDefault();
            $.get(this.href, function (data) {
                $('#dialogContent').html(data);
                $('#modDialog').modal('show');
            });
        });
    })
})()