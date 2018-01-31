
//状态栏消息显示扩展函数

bootbox.message = function (alertClassName, iconClassName, message) {
    var template = '<div class="alert alert-dismissible status-bar"><i class="icon fa"></i><span></span></div>';
    var html = $(template).addClass(alertClassName).hide();
    html.find("i").addClass(iconClassName);
    html.find("span").text(message);
    $(".content").after(html);
    html.delay(200).fadeIn("slow").delay(3000).fadeOut("slow", function () {
        $(this).remove();
    });
}

bootbox.info = function (message) {
    bootbox.message("alert-info", "fa-info", message);
}

bootbox.success = function (message) {
    bootbox.message("alert-success", "fa-check", message);
}

bootbox.warning = function (message) {
    bootbox.message("alert-warning", "fa-warning", message);
}

bootbox.error = function (message) {
    bootbox.message("alert-error", "fa-times-circle", message);
}

//侧边栏展开折叠记忆

$(document).on("expanded.pushMenu", "body", function (e) {
    localStorage.setItem("pushMenu", "expanded");
});

$(document).on("collapsed.pushMenu", "body", function (e) {
    localStorage.setItem("pushMenu", "collapsed");
});

$(document).ready(function () {
    if (localStorage.getItem("pushMenu") == "collapsed") {
        $($.AdminLTE.options.sidebarToggleSelector, document).trigger("click");
    }
});