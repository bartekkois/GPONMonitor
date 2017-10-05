var OnuDetailsController = function (onuDetailsService) {
    "use strict";

    var onuListTableTbody = $(".onu-list > tbody");
    var onuDetailsTbody = $(".onu-details > tbody");
    var onuDetailsRefreshButton = $("#refresh-onu-details");
    var onuDetailsAlert = $("#onu-details-alert");
    var onuDetailsAlertDescription = $("#onu-details-alert-description");

    var init = function () {
        onuListTableTbody.on("click", "tr.clickable-row", getOnuDetails);
        $(document).on("click", "#refresh-onu-details", refreshOnuDetails);
    };

    var getOnuDetails = function (e) {
        var onuLink = $(e.target);

        var oltId = onuListTableTbody.attr("data-olt-id");
        var oltPortId = onuLink.closest("tr").attr("data-olt-port-id");
        var onuId = onuLink.closest("tr").attr("data-onu-id");

        onuDetailsRefreshButton.addClass("gly-spin");
        onuDetailsService.getOnuDetails(oltId, oltPortId, onuId, done, fail);
    };

    var refreshOnuDetails = function (e) {
        var oltId = onuDetailsRefreshButton.attr("data-olt-id");
        var oltPortId = onuDetailsRefreshButton.attr("data-olt-port-id");
        var onuId = onuDetailsRefreshButton.attr("data-onu-id");

        onuDetailsRefreshButton.addClass("gly-spin");
        onuDetailsService.getOnuDetails(oltId, oltPortId, onuId, done, fail);
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
        onuDetailsAlert.addClass("hidden");
        onuDetailsAlertDescription.empty();

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
        $("#onu-system-uptime").text(result.systemUptime.description);

        // Onu Connection Inactive Time
        $("#onu-connection-inactive-time").text(result.opticalConnectionInactiveTime.description);

        // Onu Block Status
        $("#onu-block-status").text(result.blockStatus.description);
        $("#onu-block-status").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.blockStatus.severity));

        // Onu Block Reason
        $("#onu-block-reason").text(result.blockReason.description);
        $("#onu-block-reason").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.blockReason.severity));
        if (result.blockStatus.value !== "255")
            $("#onu-block-reason").parent("tr").removeClass("hidden");
        else
            $("#onu-block-reason").parent("tr").addClass("hidden");

        // Onu firmware version
        $("#onu-firmware-version").text(result.firmwareVersion.description);

        // Onu GPON Profile
        $("#onu-gpon-profile").text(result.gponProfile.description);

        // Onu Ethernet Port 1 State and Speed
        if (result.hasOwnProperty("ethernetPort1State") && result.hasOwnProperty("ethernetPort1Speed")) {
            var ethernetPort1StateAndSpeed;
            if (result.ethernetPort1State.value === 1)
                ethernetPort1StateAndSpeed = result.ethernetPort1State.description + " - " + result.ethernetPort1Speed.description;
            else
                ethernetPort1StateAndSpeed = result.ethernetPort1State.description;

            $("#onu-ethernet-port-1-state-and-speed").text(ethernetPort1StateAndSpeed).parent("tr").removeClass("hidden");
            $("#onu-ethernet-port-1-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort1State.severity));
        }
        else {
            $("#onu-ethernet-port-1-state-and-speed").empty().parent("tr").addClass("hidden");
            $("#onu-ethernet-port-1-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 2 State and Speed
        if (result.hasOwnProperty("ethernetPort2State") && result.hasOwnProperty("ethernetPort2Speed")) {
            var ethernetPort2StateAndSpeed;
            if (result.ethernetPort2State.value === 1)
                ethernetPort2StateAndSpeed = result.ethernetPort2State.description + " - " + result.ethernetPort2Speed.description;
            else
                ethernetPort2StateAndSpeed = result.ethernetPort2State.description;

            $("#onu-ethernet-port-2-state-and-speed").text(ethernetPort2StateAndSpeed).parent("tr").removeClass("hidden");
            $("#onu-ethernet-port-2-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort2State.severity));
        }
        else {
            $("#onu-ethernet-port-2-state-and-speed").empty().parent("tr").addClass("hidden");
            $("#onu-ethernet-port-2-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 3 State and Speed
        if (result.hasOwnProperty("ethernetPort3State") && result.hasOwnProperty("ethernetPort3Speed")) {
            var ethernetPort3StateAndSpeed;
            if (result.ethernetPort3State.value === 1)
                ethernetPort3StateAndSpeed = result.ethernetPort3State.description + " - " + result.ethernetPort3Speed.description;
            else
                ethernetPort3StateAndSpeed = result.ethernetPort3State.description;

            $("#onu-ethernet-port-3-state-and-speed").text(ethernetPort3StateAndSpeed).parent("tr").removeClass("hidden");
            $("#onu-ethernet-port-3-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort3State.severity));
        }
        else {
            $("#onu-ethernet-port-3-state-and-speed").empty().parent("tr").addClass("hidden");
            $("#onu-ethernet-port-3-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu Ethernet Port 4 State and Speed
        if (result.hasOwnProperty("ethernetPort4State") && result.hasOwnProperty("ethernetPort4Speed")) {
            var ethernetPort4StateAndSpeed;
            if (result.ethernetPort4State.value === 1)
                ethernetPort4StateAndSpeed = result.ethernetPort4State.description + " - " + result.ethernetPort4Speed.description;
            else
                ethernetPort4StateAndSpeed = result.ethernetPort4State.description;

            $("#onu-ethernet-port-4-state-and-speed").text(ethernetPort4StateAndSpeed).parent("tr").removeClass("hidden");
            $("#onu-ethernet-port-4-state-and-speed").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.ethernetPort4State.severity));
        }
        else {
            $("#onu-ethernet-port-4-state-and-speed").empty().parent("tr").addClass("hidden");
            $("#onu-ethernet-port-4-state-and-speed").attr("class", "onu-detail-item");
        }

        // Onu VoIP Line 1 State
        if (result.hasOwnProperty("voIPLine1State")) {
            $("#onu-voip-port-1-state").text(result.voIPLine1State.description).parent("tr").removeClass("hidden");
            $("#onu-voip-port-1-state").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.voIPLine1State.severity));
        }
        else {
            $("#onu-voip-port-1-state").empty().parent("tr").addClass("hidden");
            $("#onu-voip-port-1-state").attr("class", "onu-detail-item");
        }

        // Onu VoIP Line 2 State
        if (result.hasOwnProperty("voIPLine2State")) {
            $("#onu-voip-port-2-state").text(result.voIPLine2State.description).parent("tr").removeClass("hidden");
            $("#onu-voip-port-2-state").attr("class", "onu-detail-item").addClass(translateSeverityLevel(result.voIPLine2State.severity));
        }
        else {
            $("#onu-voip-port-2-state").empty().parent("tr").addClass("hidden");
            $("#onu-voip-port-2-state").attr("class", "onu-detail-item");
        }

        // Onu Image
        if (result.modelType.description !== "") {
            $("#onu-image").attr("src", "images/ONU/" + result.modelType.description + ".png").parents().eq(2).removeClass("hidden");
        }
        else {
            $("#onu-image").parents().eq(2).addClass("hidden");
        }

        onuDetailsRefreshButton.attr("data-olt-id", oltId).attr("data-olt-port-id", oltPortId).attr("data-onu-id", onuId);
        onuDetailsRefreshButton.removeClass("gly-spin");
    };

    var fail = function (oltId, oltPortId, onuId, result) {
        onuDetailsAlert.removeClass("hidden");
        onuDetailsAlertDescription.text(result.responseText);
        onuDetailsTbody.find("tr > td.onu-detail-item").empty();
        $("#onu-image").parents().eq(2).addClass("hidden");
        onuDetailsRefreshButton.attr("data-olt-id", oltId).attr("data-olt-port-id", oltPortId).attr("data-onu-id", onuId);
        onuDetailsRefreshButton.removeClass("gly-spin");
    };

    return {
        init: init
    };
}(OnuDetailsService);