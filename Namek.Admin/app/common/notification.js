var NotificationNameK = {
    title: getLanguageText('GlobalNotification'),
    init: function () {
        // request permission on page load
        document.addEventListener('DOMContentLoaded', function () {
            NotificationNameK.option();
            $.each($('.note-success'), function (i, value) {
                NotificationNameK.success($(this).html());
            });
            $.each($('.note-error'), function (i, value) {
                NotificationNameK.error($(this).html());
            });

        });
    },
    option: function () {
        if (window.toastr)
            window.toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "5000",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
    },
    success: function (message, title) {
        if (typeof title === "undefined")
            title = getLanguageText('GlobalSuccess');
        if (window.toastr)
            window.toastr.success(message, title);
    },
    error: function (message, title) {
        if (typeof title === "undefined")
            title = getLanguageText('GlobalError');
        if (window.toastr)
            window.toastr.error(message, title);
    },

    info: function (message, title) {
        if (typeof title === "undefined")
            title = getLanguageText('ScriptInformation');
        if (window.toastr)
            window.toastr.info(message, title);
    },
    warning: function (message, title) {
        if (typeof title === "undefined")
            title = getLanguageText('ScriptWarning');
        if (window.toastr)
            window.toastr.warning(message, title);
    }

}
NotificationNameK.init();
