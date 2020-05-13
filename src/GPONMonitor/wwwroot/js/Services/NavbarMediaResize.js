var NavbarMediaResize = (function () {
    "use strict";

    var init = function () {
        function autocollapse() {
            $('body').css({ paddingTop: $('#main-navbar').height() + 15 });
        }

        $(autocollapse);
        $(window).on('resize', autocollapse);
    };

    return {
        init: init
    };
}());