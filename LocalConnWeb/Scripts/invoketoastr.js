// notification popup
var type = $("#toastr-type").attr("data-msg");
var message = $("#toastr-message").attr("data-msg");
if (message != '' && message != null) {

    toastr.options.closeButton = true;
    toastr.options.positionClass = 'toast-bottom-right';
    toastr.options.showDuration = 300;
    toastr.options.hideDuration = 1000;
    toastr.options.timeOut = 5000;
    toastr.options.showEasing = 'swing';
    toastr.options.hideEasing = 'linear';
    toastr.options.showMethod = 'fadeIn';
    toastr.options.hideMethod = 'fadeOut';


    if (type == "success") { toastr.success(message, "Success"); }
    else if (type == "error") {
        toastr.error(message, "Error");
    }
    else if (type == "info") {
        toastr.info(message, "Info");
    }

}