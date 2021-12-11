
window.namekApp = angular.module("namekApp", [
    'ui.router',
    'angularUtils.directives.dirPagination',
    'ngSanitize',
    'angularFileUpload',
    'toastr',
    'ngMessages',
    'ngMeta',
    'pascalprecht.translate',
    'LocalStorageModule',
    'ui.bootstrap'
])
    .constant('globalApiEndPoint', Configuration.rootApiUrl + '')
    .constant('signalREndPoint', Configuration.rootApiUrl + '/signalr')
    .constant('loggedInUserKey', 'loggedin')
    .constant('userInfoKey', 'userinfo')
    .constant('currentLanguageKey', 'currentLanguage')
    .factory('$global', [
        'globalApiEndPoint', function (globalApiEndPoint) {
            return {
                getApiUrl: function (url) {
                    return globalApiEndPoint + url;
                }
            }
        }
    ])
    ;

window.namekApp.run([
    "$rootScope", "$sce", "$state", "$window",
    "ngMeta" ,
    function ($rootScope, $sce, $state, $window,
        ngMeta 
    ) {
        $rootScope.$state = $state;
        $rootScope.siteName = window.Configuration.siteName;
        $rootScope.copyrightBy = window.Configuration.copyrightBy;
        // meta tag
        ngMeta.init();

        //execute some theme callbacks on view content loaded
        $rootScope.$on('$viewContentLoaded',
            function (event, viewConfig) {
                if (viewConfig !== "@") {
                    if ($window['viewContentLoaded']) {
                        $window['viewContentLoaded']();
                    }
                }

            });

        $rootScope.$on("$includeContentLoaded",
            function (event, templateName) {
                if ($window['viewContentLoaded']) {
                    $window['viewContentLoaded']();
                }
            });
        $rootScope.displayErrors = function (contextName) {
            var errors = $rootScope._errorMessages[contextName] || $rootScope._errorMessages["_global"];
            if (!errors)
                return "";

            var container = '<div class="alert alert-danger alert-dismissible">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                '<h4><i class="icon fa fa-ban"></i>Error</h4>' +
                '{MESSAGES}' +
                '</div>';

            var str = "<ul>";
            for (var i = 0; i < errors.length; i++) {
                str += "<li>" + errors[i] + "</li>";
            }
            str += "</ul>";
            return $sce.trustAsHtml(container.replace("{MESSAGES}", str));
        }

        $rootScope.displayMessages = function (contextName) {
            var msgs = $rootScope._responseMessages[contextName] || $rootScope._responseMessages["_global"];
            if (!msgs)
                return "";

            var container = '<div class="alert alert-success alert-dismissible">' +
                '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
                '<h4><i class="icon fa fa-check"></i>Success</h4>' +
                '{MESSAGES}' +
                '</div>';
            var str = "<ul>";
            for (var i = 0; i < msgs.length; i++) {
                str += "<li>" + msgs[i] + "</li>";
            }
            str += "</ul>";
            return $sce.trustAsHtml(container.replace("{MESSAGES}", str));
        }
        $rootScope._Notifications = function (contextName) {
            return $sce.trustAsHtml($rootScope.displayErrors(contextName) + $rootScope.displayMessages(contextName));
        }
        $rootScope.clearMessages = function () {
            $rootScope._responseMessages = {};
            $rootScope._errorMessages = {};
        };
        $rootScope.clearMessages();
         
         
        $rootScope.Math = window.Math;
    }
]);
