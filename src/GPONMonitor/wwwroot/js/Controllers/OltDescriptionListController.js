var OltDescriptionListController = function (oltDescriptionListService) {
    "use strict";

    var onuListTableTbody = $(".onu-list > tbody");
    var onuListTableRefreshButton = $("#refresh-onu-list");
    var alertIndicator = $("#alert-indicator");
    var alertDescription = $("#alert-description");
    var searchForm = $("#search-input");

    var init = function () {
        $(document).on("click", ".js-get-onu-list:not(.disabled)", refreshOnuList);
        $(document).on("click", "#refresh-onu-list:not(.disabled)", refreshOnuList);
    };

    var initializeOnuList = function (oltId) {
        markActiveNavbarLink(oltId);
        onuListTableRefreshButton.addClass("disabled");
        onuListTableRefreshButton.addClass("fa-spin");
        oltDescriptionListService.getOltDescriptionList(oltId, done, fail);
    };

    var refreshOnuList = function (e) {
        var oltId = $(e.target).attr("data-olt-id");

        markActiveNavbarLink(oltId);
        onuListTableRefreshButton.addClass("disabled");
        onuListTableRefreshButton.addClass("fa-spin");
        onuListTableTbody.empty();
        searchForm.val("");
        oltDescriptionListService.getOltDescriptionList(oltId, done, fail);
    };

    var done = function (oltId, result) {
        alertIndicator.addClass("d-none");
        alertDescription.empty();
        onuListTableTbody.attr("data-olt-id", oltId);
        onuListTableRefreshButton.attr("data-olt-id", oltId);

        for (var i in result) {
            var oltPortId = result[i].oltPortId;
            var onuId = result[i].onuId;
            var onuGponSerialNumber = result[i].onuGponSerialNumber;
            var opticalPowerReceived = parseFloat(result[i].opticalPowerReceived)/10;
            var onuDescription;

            if (result[i].onuDescription !== "")
                onuDescription = result[i].onuDescription;
            else
                onuDescription = "(" + result[i].onuGponSerialNumber + ")";

            var onuOpticalConnectionStateStyle;
            if (result[i].onuOpticalConnectionState == "up")
                onuOpticalConnectionStateStyle = "text-default";
            else
                onuOpticalConnectionStateStyle = "text-danger";

            var opticalPowerReceivedIndicator = "";
            if (result[i].onuOpticalConnectionState == "up") {
                if (opticalPowerReceived < -9.0 && opticalPowerReceived > -26.0)
                    opticalPowerReceivedIndicator = "";
                else if ((opticalPowerReceived < -8.0 && opticalPowerReceived >= -9.0) || (opticalPowerReceived <= -26.0 && opticalPowerReceived > -27.0))
                    opticalPowerReceivedIndicator = "<i class='fa fa-bolt text-warning' title='" + opticalPowerReceived + " dBm'></i>";
                else
                    opticalPowerReceivedIndicator = "<i class='fa fa-bolt text-danger' title='" + opticalPowerReceived + " dBm'></i>";
            }

            onuListTableTbody.append(
            "<tr class='clickable-row' data-olt-port-id='" + oltPortId + "' data-onu-id='" + onuId + "' data-href='#'>" +
            "<td class='onu-list-id'><span>" + oltPortId + "." + onuId + "</span></td>" +
            "<td class='onu-list-sn'><i class='fa fa-hdd " + onuOpticalConnectionStateStyle + "' title='" + onuGponSerialNumber + "'></i></td>" +
                "<td class='onu-list-item'><span>" + onuDescription + " " + opticalPowerReceivedIndicator + "</span></td>" +
            "</tr>");
        }

        var dummyOltPortIdOnuIDWidth = 0;
        var dummyOltPortIdOnuID = "";
        var dummyOnuDescriptionWidth = 0;
        var dummyOnuDescription = "";

        onuListTableTbody.find("tr.clickable-row").each(function () {
            var oltPortIdOnuId = $(this).find("td.onu-list-id>span");
            var onuDescription = $(this).find("td.onu-list-item>span");

            if (oltPortIdOnuId.outerWidth() > dummyOltPortIdOnuIDWidth) dummyOltPortIdOnuID = oltPortIdOnuId.text();
            if (onuDescription.outerWidth() > dummyOnuDescriptionWidth) {
                dummyOnuDescriptionWidth = onuDescription.outerWidth();
                dummyOnuDescription = onuDescription.text();
            }
        });

        onuListTableTbody.append(
            "<tr class='dummy-row invisible'>" +
            "<td class='onu-list-id'><span>" + dummyOltPortIdOnuID + "</span></td>" +
            "<td class='onu-list-sn'><i class='fa fa-hdd'></i></td>" +
            "<td class='onu-list-item'><span>" + dummyOnuDescription + "</span></td>" +
            "</tr>");

        onuListTableRefreshButton.removeClass("fa-spin");
        onuListTableRefreshButton.removeClass("disabled");
        removeDisabledPropertyFromNavbarLink();
    };

    var fail = function (oltId, result) {
        alertIndicator.removeClass("d-none");
        alertDescription.text(result.responseText);
        onuListTableTbody.attr("data-olt-id", oltId);
        onuListTableRefreshButton.attr("data-olt-id", oltId);
        onuListTableRefreshButton.removeClass("fa-spin");
        onuListTableRefreshButton.removeClass("disabled");
        removeDisabledPropertyFromNavbarLink();
    };

    var markActiveNavbarLink = function (oltId) {
        $("a.js-get-onu-list").removeClass("active");
        $("a.js-get-onu-list").removeClass("disabled");
        $(`a.js-get-onu-list[data-olt-id="${oltId}"]`).addClass("active");
        $(`a.js-get-onu-list[data-olt-id!="${oltId}"]`).addClass("disabled");
    };

    var removeDisabledPropertyFromNavbarLink = function () {
        $("a.js-get-onu-list").removeClass("disabled");
    };

    return {
        init: init,
        initializeOnuList: initializeOnuList
    };
}(OltDescriptionListService);