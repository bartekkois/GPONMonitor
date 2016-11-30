var OltDescriptionListService = function () {
    "use strict";

    // GET: api/Olt/?oltId=1
    var getOltDescriptionList = function (oltId, done, fail) {
        $.get("api/Olt/", { oltId: oltId }, "json")
        .success(function (result) { done(oltId, result); })
        .error(function (result) { fail(oltId, result); });
    };

    return {
        getOltDescriptionList: getOltDescriptionList
    };
}();