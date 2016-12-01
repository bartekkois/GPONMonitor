var OnuDescriptionListSearch = (function () {
    "use strict";

    var onuListTableTbody = $(".onu-list > tbody");
    var searchForm = $("#search-form");

    var init = function () {
        $.event.special.inputchange = {
            setup: function () {
                var self = this, val;
                $.data(this, "timer", window.setInterval(function () {
                    val = self.value;
                    if ($.data(self, "cache") !== val) {
                        $.data(self, "cache", val);
                        $(self).trigger("inputchange");
                    }
                }, 1200));
            },
            teardown: function () {
                window.clearInterval($.data(this, "timer"));
            },
            add: function () {
                $.data(this, "cache", this.value);
            }
        };

        searchForm.on("inputchange", function () {
            var searchTerm = $("#search-form").val().toLowerCase();
            if (searchTerm !== "") {
                onuListTableTbody.find("tr").each(function () {
                    if (~$(this).find("td.onu-list-item").text().toLowerCase().indexOf(searchTerm) || ~$(this).find("td.onu-list-sn").text().toLowerCase().indexOf(searchTerm))
                        $(this).removeClass("hidden");
                    else
                        $(this).addClass("hidden");
                });
            }
            else {
                onuListTableTbody.find("tr").each(function () {
                    $(this).removeClass("hidden");
                });
            }
        });
    };

    return {
        init: init
    };
}());