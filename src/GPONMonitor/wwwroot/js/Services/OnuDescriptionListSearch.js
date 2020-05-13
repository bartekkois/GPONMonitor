var OnuDescriptionListSearch = (function () {
    "use strict";

    var onuListTableTbody = $(".onu-list > tbody");
    var searchForm = $("#search-input");

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
            var searchTerm = $("#search-input").val().toLowerCase();
            if (searchTerm !== "") {
                var searchTermOnuId = "";
                var searchTermOnuName = "";
                var searchTermsArray = searchTerm.split('_');

                if (searchTermsArray.length >= 2)
                {
                    searchTermOnuId = searchTermsArray[0];
                    searchTermOnuName = searchTermsArray[1];
                }
                else
                {
                    searchTermOnuId = "";
                    searchTermOnuName = searchTermsArray[0];
                }

                onuListTableTbody.find("tr.clickable-row").each(function () {
                    if (~$(this).find("td.onu-list-id").text().toLowerCase().indexOf(searchTermOnuId) && (~$(this).find("td.onu-list-item").text().toLowerCase().indexOf(searchTermOnuName) || ~$(this).find("td.onu-list-sn > i").attr("title").toLowerCase().indexOf(searchTermOnuName)))
                        $(this).removeClass("d-none");
                    else
                        $(this).addClass("d-none");
                });
            }
            else {
                onuListTableTbody.find("tr.clickable-row").each(function () {
                    $(this).removeClass("d-none");
                });
            }
        });
    };

    return {
        init: init
    };
}());