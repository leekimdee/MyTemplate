function showNotification(type, message) {
    toastr.options = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "timeOut": "2500"
    }

    switch (type) {
        case "success":
            toastr.success(message);
            break;
        case "error":
            toastr.error(message);
            break;
        case "info":
            toastr.info(message);
            break;
        case "warning":
            toastr.warning(message);
            break;
    }
}