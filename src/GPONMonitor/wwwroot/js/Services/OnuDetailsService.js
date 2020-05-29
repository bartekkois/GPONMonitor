var OnuDetailsService = function () {
    "use strict";

    // GET: api/OnuStateByOltPortIdAndOnuId?oltId=1&oltPortId=2&onuId=3
    var getOnuDetails = function (oltId, oltPortId, onuId, done, fail) {
        $.get("api/OnuStateByOltPortIdAndOnuId", { oltId: oltId, oltPortId: oltPortId, onuId: onuId }, "json")
        .done(function (result) { done(oltId, oltPortId, onuId, result); })
        .fail(function (result) { fail(oltId, oltPortId, onuId, result); });
    };

    return {
        getOnuDetails: getOnuDetails
    };
}();