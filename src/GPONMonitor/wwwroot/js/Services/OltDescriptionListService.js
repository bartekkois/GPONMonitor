var OltDescriptionListService = function () {
    "use strict";

    // GET: api/OltOnuDescriptionList?oltId=1
    var getOltDescriptionList = function (oltId, done, fail) {
        $.get("api/OltOnuDescriptionList", { oltId: oltId }, "json")
        .done(function (result) { done(oltId, result); })
        .fail(function (result) { fail(oltId, result); });
    };

    return {
        getOltDescriptionList: getOltDescriptionList
    };
}();