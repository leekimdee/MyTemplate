function printNotification(type, message) {
    var placement = { from: "top", align: "right" };
    var icon = "";
    switch (type) {
        case "success":
            icon = "glyphicon glyphicon-ok";
            break;
        case "warning":
            icon = "glyphicon glyphicon-exclamation-sign";
            break;
        case "danger":
            icon = "glyphicon glyphicon-remove-sign";
            break;
        case "info":
            icon = "glyphicon glyphicon-info-sign";
            break;
    }

    $.notify({
        // options
        icon: icon,
        message: message
    }, {
        // settings
        type: type,
        placement: placement
    });
}