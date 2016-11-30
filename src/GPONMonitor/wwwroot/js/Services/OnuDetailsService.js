var OnuDetailsService = function () {
    "use strict";

    // GET: api/Onu/?oltId=1&oltPortId=2&onuId=3
    var getOnuDetails = function (oltId, oltPortId, onuId, done, fail) {
        $.get("api/Onu/", { oltId: oltId, oltPortId: oltPortId, onuId: onuId }, "json")
        .success(function (result) { done(oltId, oltPortId, onuId, result); })
        .error(function (result) { fail(oltId, oltPortId, onuId, result); });
    };

    return {
        getOnuDetails: getOnuDetails
    };
}();