var NavbarMediaResize = (function () {
    "use strict";

    var init = function () {
        function autocollapse() {
            $('body').css({ paddingTop: $('#main-navbar').height() + 15 });
        }

        $(document).on('ready', autocollapse);
        $(window).on('resize', autocollapse);
    };

    return {
        init: init
    };
}());