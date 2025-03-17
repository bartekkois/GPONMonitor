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
var OnuDetailsController = function (onuDetailsService) {
    "use strict";

    var onuListTableTbody = $(".onu-list > tbody");
    var onuDetailsTbody = $(".onu-details > tbody");
    var onuDetailsRefreshButton = $("#refresh-onu-details");
    var alertIndicator = $("#alert-indicator");
    var alertDescription = $("#alert-description");
    var resetOnuButton = $("#reset-onu-button");
    var blockOnuButton = $("#block-onu-button");
    var unblockOnuButton = $("unblock-onu-button");
    var currentAction = null;

    var init = function () {
        onuListTableTbody.on("click", "tr.clickable-row", getOnuDetails);
        $(document).on("click", "#refresh-onu-details", refreshOnuDetails);
        $(document).on("click", "#reset-onu-icon", function () { showPasswordModal(resetOnu); });
        $(document).on("click", "#block-onu-icon", function () { showPasswordModal(blockOnu); });
        $(document).on("click", "#unblock-onu-icon", function () { showPasswordModal(unblockOnu); });
        $(document).on("click", "#submitPassword", submitPassword);

        // Add keypress event listener for Enter key on the password input field
        $('#commandProtectionPassword').on('keypress', function (e) {
            if (e.which === 13) { // Enter key pressed
                $('#submitPassword').trigger('click');
            }
        });

        // Set focus on the password input field and clear it when the modal is shown
        $('#passwordModal').on('shown.bs.modal', function () {
            $('#commandProtectionPassword').val(''); // Clear the input field
            $('#commandProtectionPassword').trigger('focus'); // Set focus on the input field
        });
    };

    var getOnuDetails = function (e) {
        var onuLink = $(e.target);

        var oltId = onuListTableTbody.attr("data-olt-id");
        var oltPortId = onuLink.closest("tr").attr("data-olt-port-id");
        var onuId = onuLink.closest("tr").attr("data-onu-id");

        onuDetailsRefreshButton.addClass("fa-spin");
        onuDetailsService.getOnuDetails(oltId, oltPortId, onuId, done, fail);
    };

    var refreshOnuDetails = function (e) {
        var oltId = onuDetailsRefreshButton.attr("data-olt-id");
        var oltPortId = onuDetailsRefreshButton.attr("data-olt-port-id");
        var onuId = onuDetailsRefreshButton.attr("data-onu-id");

        if (oltId == undefined || oltId == "") oltId = 0;
        if (oltPortId == undefined || oltPortId == "") oltPortId = 0;
        if (onuId == undefined || onuId == "") onuId = 0;

        onuDetailsRefreshButton.addClass("fa-spin");
        onuDetailsService.getOnuDetails(oltId, oltPortId, onuId, done, fail);
    };

    var showPasswordModal = function (action) {
        currentAction = action;
        $("#passwordModal").modal("show");
    };

    var submitPassword = function () {
        var password = $("#commandProtectionPassword").val();
        if (password && currentAction) {
            currentAction(password);
            $("#passwordModal").modal("hide");
        }
    };

    var resetOnu = function (password) {
        var oltId = onuDetailsRefreshButton.attr("data-olt-id");
        var oltPortId = onuDetailsRefreshButton.attr("data-olt-port-id");
        var onuId = onuDetailsRefreshButton.attr("data-onu-id");

        $.post("api/ResetOnu", { oltId: oltId, oltPortId: oltPortId, onuId: onuId, commandProtectionPasswordHash: CryptoJS.MD5(password).toString() })
            .done(function (result) {
                setTimeout(function () { refreshOnuDetails(); }, 5000);
            })
            .fail(function (result) {
                alertIndicator.removeClass("d-none");
                alertDescription.text("Failed to reset ONU: " + result.responseText);
            });
    };

    var blockOnu = function (password) {
        var oltId = onuDetailsRefreshButton.attr("data-olt-id");
        var oltPortId = onuDetailsRefreshButton.attr("data-olt-port-id");
        var onuId = onuDetailsRefreshButton.attr("data-onu-id");

        $.post("api/BlockOnu", { oltId: oltId, oltPortId: oltPortId, onuId: onuId, commandProtectionPasswordHash: CryptoJS.MD5(password).toString() })
            .done(function (result) {
                setTimeout(function () { refreshOnuDetails(); }, 3000);
            })
            .fail(function (result) {
                alertIndicator.removeClass("d-none");
                alertDescription.text("Failed to block ONU: " + result.responseText);
            });
    };

    var unblockOnu = function (password) {
        var oltId = onuDetailsRefreshButton.attr("data-olt-id");
        var oltPortId = onuDetailsRefreshButton.attr("data-olt-port-id");
        var onuId = onuDetailsRefreshButton.attr("data-onu-id");

        $.post("api/UnblockOnu", { oltId: oltId, oltPortId: oltPortId, onuId: onuId, commandProtectionPasswordHash: CryptoJS.MD5(password).toString() })
            .done(function (result) {
                setTimeout(function () { refreshOnuDetails(); }, 3000);
            })
            .fail(function (result) {
                alertIndicator.removeClass("d-none");
                alertDescription.text("Failed to unblock ONU: " + result.responseText);
            });
    };

    var translateSeverityLevel = function (level) {
        switch (level) {
            case 0:
                return "indicator-unknown";
            case 1:
                return "indicator-default";
            case 2:
                return "indicator-info";
            case 3:
                return "indicator-success";
            case 4:
                return "indicator-warning";
            case 5:
                return "indicator-danger";
            default:
                return "indicator-unknown";
        }
    };

    var done = function (oltId, oltPortId, onuId, result) {
        alertIndicator.addClass("d-none");
        alertDescription.empty();

        // Onu Olt Port Id and Onu Id
        $("#onu-olt-port-id-onu-id").text(result.oltPortId + "." + result.oltOnuId);

        // Onu Description
        var onuDescription;
        if (result.descriptionName.description !== "")
            onuDescription = result.descriptionName.description;
        else
            onuDescription = "(" + result.gponSerialNumber.description + ")";

        $("#onu-description").text(onuDescription);

        // Onu Model Type
        $("#onu-model-type").text(result.modelType.description);

        // Onu GPON Serial Number
        $("#onu-gpon-serial-number").text(result.gponSerialNumber.description);

        // Onu Optical Connection State
        $("#onu-optical-connection-state").text(result.opticalConnectionState.description);
        $("#onu-optical-connection-state").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.opticalConnectionState.severity));

        // Onu Connection Deactivation Reason
        $("#onu-connection-deactivation-reason").text(result.opticalConnectionDeactivationReason.description);

        // Onu Optical Power Received
        $("#onu-optical-power-received").text(result.opticalPowerReceived.description);
        $("#onu-optical-power-received").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.opticalPowerReceived.severity));

        // Onu Optical Cable Distance
        $("#onu-optical-cable-distance").text(result.opticalCableDistance.description);

        // Onu Optical Connection Uptime
        $("#onu-optical-connection-uptime").text(result.opticalConnectionUptime.description);

        // Onu System Uptime
        $("#onu-system-uptime").html(result.systemUptime.description);
        if (result.opticalConnectionState.value == "2" || result.opticalConnectionState.value == "3") {
            $("#reset-onu-icon").removeClass("d-none");
        }
        else {
            $("#reset-onu-icon").addClass("d-none");
        }

        // Onu Connection Inactive Time
        $("#onu-connection-inactive-time").text(result.opticalConnectionInactiveTime.description);

        // Onu Block Status
        $("#onu-block-status").text(result.blockStatus.description);
        $("#onu-block-status").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.blockStatus.severity));
        if (result.blockStatus.value == "255") {
            $("#block-onu-icon").removeClass("d-none");
            $("#unblock-onu-icon").addClass("d-none");
        }
        else if (result.blockStatus.value == "1" || result.blockStatus.value == "2") {
            $("#block-onu-icon").addClass("d-none");
            $("#unblock-onu-icon").removeClass("d-none");
        }
        else {
            $("#block-onu-icon").addClass("d-none");
            $("#unblock-onu-icon").addClass("d-none");
        }

        // Onu Block Reason
        $("#onu-block-reason").text(result.blockReason.description);
        $("#onu-block-reason").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.blockReason.severity));
        if (result.blockStatus.value !== "255")
            $("#onu-block-reason").parent("tr").removeClass("d-none");
        else
            $("#onu-block-reason").parent("tr").addClass("d-none");

        // Onu firmware version
        $("#onu-firmware-version").text(result.firmwareVersion.description);

        // Onu GPON Profile
        $("#onu-gpon-profile").text(result.gponProfile.description);

        // Onu IP Host 1
        $("#onu-ip-host-1").empty();
        if (result.ipHost1.description.indexOf("0.0.0.0") == -1 && result.ipHost1.description.indexOf("NoSuchInstance") == -1) {
            $("#onu-ip-host-1").parent("tr").removeClass("d-none");
            $("#onu-ip-host-1").append("<a href=http://" + result.ipHost1.description + " target=\"_blank\" rel=\"noopener noreferrer\">" + result.ipHost1.description + "</a>");
        }
        else {
            $("#onu-ip-host-1").parent("tr").addClass("d-none");
        }

        // Onu Ethernet Port 1 State and Speed
        if (result.hasOwnProperty("ethernetPort1State") && result.hasOwnProperty("ethernetPort1Speed")) {
            var ethernetPort1StateAndSpeed;
            if (result.ethernetPort1State.value === 1)
                ethernetPort1StateAndSpeed = result.ethernetPort1State.description + " - " + result.ethernetPort1Speed.description;
            else
                ethernetPort1StateAndSpeed = result.ethernetPort1State.description;

            $("#onu-ethernet-port-1-state-and-speed").text(ethernetPort1StateAndSpeed).parent("tr").removeClass("d-none");
            $("#onu-ethernet-port-1-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort1State.severity));
        }
        else {
            $("#onu-ethernet-port-1-state-and-speed").empty().parent("tr").addClass("d-none");
            $("#onu-ethernet-port-1-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 2 State and Speed
        if (result.hasOwnProperty("ethernetPort2State") && result.hasOwnProperty("ethernetPort2Speed")) {
            var ethernetPort2StateAndSpeed;
            if (result.ethernetPort2State.value === 1)
                ethernetPort2StateAndSpeed = result.ethernetPort2State.description + " - " + result.ethernetPort2Speed.description;
            else
                ethernetPort2StateAndSpeed = result.ethernetPort2State.description;

            $("#onu-ethernet-port-2-state-and-speed").text(ethernetPort2StateAndSpeed).parent("tr").removeClass("d-none");
            $("#onu-ethernet-port-2-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort2State.severity));
        }
        else {
            $("#onu-ethernet-port-2-state-and-speed").empty().parent("tr").addClass("d-none");
            $("#onu-ethernet-port-2-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 3 State and Speed
        if (result.hasOwnProperty("ethernetPort3State") && result.hasOwnProperty("ethernetPort3Speed")) {
            var ethernetPort3StateAndSpeed;
            if (result.ethernetPort3State.value === 1)
                ethernetPort3StateAndSpeed = result.ethernetPort3State.description + " - " + result.ethernetPort3Speed.description;
            else
                ethernetPort3StateAndSpeed = result.ethernetPort3State.description;

            $("#onu-ethernet-port-3-state-and-speed").text(ethernetPort3StateAndSpeed).parent("tr").removeClass("d-none");
            $("#onu-ethernet-port-3-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort3State.severity));
        }
        else {
            $("#onu-ethernet-port-3-state-and-speed").empty().parent("tr").addClass("d-none");
            $("#onu-ethernet-port-3-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 4 State and Speed
        if (result.hasOwnProperty("ethernetPort4State") && result.hasOwnProperty("ethernetPort4Speed")) {
            var ethernetPort4StateAndSpeed;
            if (result.ethernetPort4State.value === 1)
                ethernetPort4StateAndSpeed = result.ethernetPort4State.description + " - " + result.ethernetPort4Speed.description;
            else
                ethernetPort4StateAndSpeed = result.ethernetPort4State.description;

            $("#onu-ethernet-port-4-state-and-speed").text(ethernetPort4StateAndSpeed).parent("tr").removeClass("d-none");
            $("#onu-ethernet-port-4-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort4State.severity));
        }
        else {
            $("#onu-ethernet-port-4-state-and-speed").empty().parent("tr").addClass("d-none");
            $("#onu-ethernet-port-4-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu VoIP Line 1 State
        if (result.hasOwnProperty("voIPLine1State")) {
            $("#onu-voip-port-1-state").text(result.voIPLine1State.description).parent("tr").removeClass("d-none");
            $("#onu-voip-port-1-state").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.voIPLine1State.severity));
        }
        else {
            $("#onu-voip-port-1-state").empty().parent("tr").addClass("d-none");
            $("#onu-voip-port-1-state").attr("class", "onu-detail-item");
        }

        // Onu VoIP Line 2 State
        if (result.hasOwnProperty("voIPLine2State")) {
            $("#onu-voip-port-2-state").text(result.voIPLine2State.description).parent("tr").removeClass("d-none");
            $("#onu-voip-port-2-state").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.voIPLine2State.severity));
        }
        else {
            $("#onu-voip-port-2-state").empty().parent("tr").addClass("d-none");
            $("#onu-voip-port-2-state").attr("class", "onu-detail-item");
        }

        // Onu Image
        if (result.modelType.description !== "") {
            $("#onu-image").attr("src", "images/ONU/" + result.modelType.description + ".png").on("error", function () { $(this).hide(); }).parents().eq(2).removeClass("d-none");
        }
        else {
            $("#onu-image").parents().eq(2).addClass("d-none");
        }

        onuDetailsRefreshButton.attr("data-olt-id", oltId).attr("data-olt-port-id", oltPortId).attr("data-onu-id", onuId);
        onuDetailsRefreshButton.removeClass("fa-spin");
    };

    var fail = function (oltId, oltPortId, onuId, result) {
        alertIndicator.removeClass("d-none");
        alertDescription.text(result.responseText);
        onuDetailsTbody.find("tr > td.onu-detail-item").empty();
        $("#onu-image").parents().eq(2).addClass("d-none");
        onuDetailsRefreshButton.attr("data-olt-id", oltId).attr("data-olt-port-id", oltPortId).attr("data-onu-id", onuId);
        onuDetailsRefreshButton.removeClass("fa-spin");
    };

    return {
        init: init
    };
}(OnuDetailsService);