﻿window.namekApp.filter('unescape', function () {
    
    return function (html) {
        return angular.element("<div/>").html(html).text();
    }
});