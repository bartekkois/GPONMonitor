var TooltipWhenOverflow = (function () {
    "use strict";

    var init = function () {
        $(document).on("mouseenter", ".onu-list-id, .onu-list-item, .onu-detail-description, .onu-detail-item", function () {
            var $this = $(this);

            if (this.offsetWidth < this.scrollWidth) {
                $this.attr("title", $this.text());
            }
            else {
                $this.attr("title", "");
            }
        });
    };

    return {
        init: init
    };
}());