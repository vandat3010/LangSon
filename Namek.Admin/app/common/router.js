

window.namekApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider, ngMetaProvider,
    $translateProvider
) {

    //$urlRouterProvider.otherwise('/home');
    const adminPrefix = window.Configuration.adminUrlPathPrefix;

    $stateProvider
        .state("layoutAdministration.page.list",
            {
                templateUrl: "Views/Page/Index.cshtml",
                url: "/page",
                controller: "pageController"
        })
        .state("layoutAdministration.Agency.List",
        {
            templateUrl: "Views/Agency/Index.cshtml",
            url: "/Agency",
            controller: "AgencyController"
        })
        .state("layoutAdministration.Agency.List",
        {
            templateUrl: "Views/AgencyCustomer/Index.cshtml",
            url: "/AgencyCustomer",
            controller: "AgencyCustomerController"
        })
        .state("layoutZero",
            {
                abstract: true,
                templateUrl: "pages/layouts/layout-none.html"
            })
        .state("layoutZero.login",
            {
                templateUrl: "pages/common/login.html",
                data: {
                    meta: {
                        'title': 'Login page',
                        'titleSuffix': ' | Login',
                        'description': 'Login to the site'
                    }
                },

                url: "/login",
                controller: "loginController"
            })
        .state("layoutZero.register",
            {
                templateUrl: "pages/common/register.html",
                url: "/register",
                //resolve: {
                //    resolver: function(controllerProvider) {
                //        return controllerProvider.resolveBundles(["register"]);
                //    }
                //},
                controller: "registerController"
            })
        .state("layout",
            {
                abstract: true,
                templateUrl: "pages/layouts/layout.html"
            })
        .state('layout.home',
            {
                url: '/',
                templateUrl: 'pages/home/home.html'
            })
        .state('layout.index',
            {
                url: '/index.html',
                templateUrl: 'pages/home/home.html'
            })
        .state('layout.about',
            {
                url: '/about',
                templateUrl: 'pages/common/about.html'
            })
        .state('layout.contact',
            {
                url: '/about',
                templateUrl: 'pages/common/contact.html'
            })
        .state("layout.newsArticle",
            {
                abstract: true,
                url: "/newsArticle",
                template: "<div ui-view></div>"

            })
        .state('layout.newsArticle.list',
            {
                url: '',
                templateUrl: 'pages/newsArticle/newsArticle.list.html'
            })
        .state('layout.newsArticle.edit',
            {
                url: '/edit/:id',
                templateUrl: 'pages/newsArticle/newsArticle.edit.html'
            })
        .state("layout.transaction",
            {
                abstract: true,
                url: "/transaction",
                template: "<div ui-view></div>"

            })
        .state('layout.transaction.list',
            {
                url: '',
                templateUrl: 'pages/transaction/transaction.list.html'
            })
        .state("layout.quantitiesReport",
            {
                abstract: true,
                url: "/quantitiesReport",
                template: "<div ui-view></div>"

            })
        .state('layout.quantitiesReport.list',
            {
                url: '',
                templateUrl: 'Views/Quantiti/transaction.list.html'
            });

    $stateProvider.state("layoutZero.404",
        {
            templateUrl: "pages/common/404.html"
        });
    $urlRouterProvider.otherwise(function ($injector, $location) {
        var state = $injector.get('$state');
        state.go('layoutZero.404');
        return $location.path();
    });
    $locationProvider.html5Mode(true);

    configTranslate($translateProvider);

    configMeta(ngMetaProvider);

});
var configTranslate = function($translateProvider)
{
    // add translation tables
    $translateProvider.translations(window.Configuration.en, translationsEN);
    $translateProvider.translations(window.Configuration.vn, translationsVN);
    $translateProvider.preferredLanguage(window.Configuration.en);
    $translateProvider.fallbackLanguage(window.Configuration.en);

}
var configMeta = function (ngMetaProvider) {
    ngMetaProvider.useTitleSuffix(true);
    ngMetaProvider.setDefaultTitle(window.Configuration.siteName);
    ngMetaProvider.setDefaultTitleSuffix(' | ' + window.Configuration.siteName);
    ngMetaProvider.setDefaultTag('author', window.Configuration.copyrightBy);
}
//window.namekApp.config(["$stateProvider",
//    "$urlRouterProvider",
//    "$locationProvider",
//    "$controllerProvider",
//    "$compileProvider",
//    "$filterProvider",
//    "$provide",

//    "$authProvider",
//    "ngMetaProvider",
//    function ($stateProvider, $urlRouterProvider, $locationProvider,
//        $controllerProvider, $compileProvider, $filterProvider, $provider,

//        $authProvider, ngMetaProvider) {
//        const adminPrefix = window.Configuration.adminUrlPathPrefix;
//        //window.namekApp.lazy = {
//        //    controller: $controllerProvider.register,
//        //    directive: $compileProvider.directive,
//        //    filter: $filterProvider.register,
//        //    factory: $provider.factory,
//        //    service: $provider.service
//        //};

//        $stateProvider
//            .state("layoutZero",
//                {
//                    abstract: true,
//                    templateUrl: "pages/layouts/_layout-none.html"
//                })
//            .state("layoutZero.login",
//                {
//                    templateUrl: "pages/login.html",
//                    data: {
//                        meta: {
//                            'title': 'Login page',
//                            'titleSuffix': ' | Login to YourSiteName',
//                            'description': 'Login to the site'
//                        }
//                    },

//                    url: "/login",
//                    controller: "loginController"
//                })
//            .state("layoutZero.register",
//                {
//                    templateUrl: "pages/common/register.html",
//                    url: "/register",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider.resolveBundles(["register"]);
//                        }
//                    },
//                    controller: "registerController"
//                })
//            .state("layoutZero.activate",
//                {
//                    templateUrl: "pages/common/activate.html",
//                    url: "/activate?code",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider.resolveBundles(["register"]);
//                        }
//                    },
//                    controller: "registerController"
//                })
//            .state("layoutZero.install",
//                {
//                    templateUrl: "pages/common/install.html",
//                    url: "/install",
//                    controller: "installController",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider.resolveBundles(["install"]);
//                        }
//                    }
//                });
//        $stateProvider
//                .state("layoutAdministration",
//                    {
//                        abstract: true,
//                        resolve: {
//                            auth: function (authProvider) {
//                                return authProvider.isLoggedIn();
//                            }
//                        },
//                        templateUrl: "pages/layouts/_layout-administration.html"
//                    })
//                .state("layoutAdministration.dashboard",
//                    {
//                        url: adminPrefix,
//                        templateUrl: "pages/dashboard.html"
//                    })
//                .state("layoutAdministration.users",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/users",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["fileUpload", "users","timeline"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.users.list",
//                    {
//                        url: "",
//                        templateUrl: "pages/users/users.list.html",
//                        controller: "userController"
//                    })
//                .state("layoutAdministration.users.edit",
//                    {
//                        abstract: true,
//                        url: "/edit/:id",
//                        templateUrl: "pages/users/user.edit.html",
//                        controller: "userEditController"
//                    })
//                .state('layoutAdministration.users.edit.basic',
//                    {
//                        url: '',
//                        templateUrl: 'pages/users/user.edit.basic.html'
//                    })
//                .state('layoutAdministration.users.edit.timeline',
//                    {
//                        url: '/timeline',
//                        templateUrl: 'pages/users/user.edit.timeline.html'
//                    })
//                .state("layoutAdministration.settings",
//                    {
//                        url: adminPrefix + "/settings/:settingType",
//                        templateUrl: function (stateParams) {
//                            return "pages/settings/" + stateParams.settingType + "Settings.edit.html";
//                        },
//                        controllerProvider: function ($stateParams) {
//                            if (!$stateParams.settingType)
//                                return "settingEditController";
//                            switch ($stateParams.settingType) {
//                            default:
//                                return "settingEditController";

//                            }
//                        },
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["settings"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.emails",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/emails",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["emailAccounts"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.emails.accountlist",
//                    {
//                        url: "/emailaccounts",
//                        controller: "emailAccountController",
//                        templateUrl: "/pages/emails/emailAccount.list.html"
//                    })
//                .state("layoutAdministration.emails.accountedit",
//                    {
//                        url: "/emailaccount/:id",
//                        controller: "emailAccountController",
//                        templateUrl: "/pages/emails/emailAccount.editor.html"
//                    })
//                .state("layoutAdministration.emails.templatelist",
//                    {
//                        url: "/emailtemplates",
//                        controller: "emailTemplateController",
//                        templateUrl: "/pages/emails/emailTemplate.list.html"
//                    })
//                .state("layoutAdministration.emails.templateedit",
//                    {
//                        url: "/emailtemplate/:id",
//                        controller: "emailTemplateController",
//                        templateUrl: "/pages/emails/emailTemplate.editor.html"
//                    })
//                .state("layoutAdministration.skills",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/skills",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["skillAdmin"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.skills.list",
//                    {
//                        url: "",
//                        templateUrl: "pages/skills/skill.list.html",
//                        controller: "skillController"
//                    })
//                .state("layoutAdministration.media",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/media",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["mediaAdmin"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.media.list",
//                    {
//                        url: "",
//                        templateUrl: "pages/media/media.list.html",
//                        controller: "mediaAdminController"
//                    })
//                .state("layoutAdministration.articles",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/articles",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["fileUpload", "articlesAdmin"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.articles.list",
//                    {
//                        url: "",
//                        controller: "articleAdminController",
//                        templateUrl: "/pages/articles/article.admin.html"
//                    })
//                .state("layoutAdministration.articles.edit",
//                    {
//                        url: "/:id",
//                        controller: "articleAdminController",
//                        templateUrl: "/pages/articles/article.edit.html"
//                    })
//                .state("layoutAdministration.cars",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/cars",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["fileUpload", "carsAdmin"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.cars.list",
//                    {
//                        url: "",
//                        controller: "carAdminController",
//                        templateUrl: "/pages/cars/car.admin.html"
//                    })
//                .state("layoutAdministration.cars.edit",
//                    {
//                        url: "/:id",
//                        controller: "carAdminController",
//                        templateUrl: "/pages/cars/car.edit.html"
//                    })
//                .state("layoutAdministration.products",
//                    {
//                        abstract: true,
//                        url: adminPrefix + "/products",
//                        template: "<div ui-view></div>",
//                        resolve: {
//                            resolver: function (controllerProvider) {
//                                return controllerProvider.resolveBundles(["fileUpload", "productAdmin"]);
//                            }
//                        }
//                    })
//                .state("layoutAdministration.products.list",
//                    {
//                        url: "",
//                        controller: "productAdminController",
//                        templateUrl: "/pages/products/product.admin.html"
//                    })
//                .state("layoutAdministration.product.edit",
//                    {
//                        url: "/:id",
//                        controller: "productAdminController",
//                        templateUrl: "/pages/products/product.edit.html"
//                    })
//            ;

//        $stateProvider
//            .state("layoutMobSocial",
//                {
//                    abstract: true,
//                    resolve: {
//                        auth: function (authProvider) {
//                            return authProvider.isLoggedIn();
//                        }
//                    },
//                    templateUrl: "pages/layouts/_layout-main.html"
//                })
//            .state("layoutMobSocial.home", // trang chủ
//                {
//                    url: "/",
//                    data: {
//                        meta: {
//                            'title': 'Home page',
//                            'description': 'home page'
//                        }
//                    },

//                    templateUrl: "pages/home/home.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "fileUpload", "users", "article"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.activity",
//                {
//                    url: "/activity",
//                    data: {
//                        meta: {
//                            'title': 'Activity Newsfeed',
//                            'description': 'Activity Newsfeed'
//                        }
//                    },

//                    templateUrl: "pages/users/activity.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "fileUpload", "users", "timeline", "education", "skillPublic", "address"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.car",
//                {
//                    url: "/cars",
//                    templateUrl: "pages/cars/car.list.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "fileUpload", "users", "car"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.car.detail",
//                {
//                    //abstract: true,
//                    url: "/car/:idOrUserName",
//                    templateUrl: "pages/cars/car.detail.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "media", "fileUpload", "users", "car"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.article",
//                {
//                    url: "/articles",
//                    templateUrl: "pages/articles/article.list.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "fileUpload", "users", "article"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.article.detail",
//                {
//                    //abstract: true,
//                    url: "/article/:idOrUserName",
//                    templateUrl: "pages/articles/article.detail.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "media", "fileUpload", "users", "article"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.product",
//                {
//                    url: "/products",
//                    templateUrl: "pages/products/product.list.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "fileUpload", "users", "product"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.product.detail",
//                {
//                    //abstract: true,
//                    url: "/product/:idOrUserName",
//                    templateUrl: "pages/products/product.detail.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "media", "fileUpload", "users", "product"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.userprofile",
//                {
//                    abstract: true,
//                    url: "/u/:idOrUserName?tab",
//                    templateUrl: "pages/users/user.profile.html",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "social", "media", "fileUpload", "users", "timeline", "skillPublic"]);
//                        }
//                    }
//                })
//            .state("layoutMobSocial.userprofile.tabs",
//                {
//                    url: "",
//                    templateUrl: function ($stateParams, state) {
//                        if ([undefined, "main", "pictures", "videos", "friends", "followers", "following", "skills"].indexOf($stateParams.tab) === -1) {
//                            return "pages/common/404.html";
//                        }
//                        return "pages/users/user.profile." + ($stateParams.tab || "main") + ".html";
//                    }
//                });

//        $stateProvider
//            .state("layoutMobSocial.twoColumns",
//                {
//                    abstract: true,
//                    templateUrl: "pages/layouts/_layout-mobsocial-two-columns.html"
//                })
//            .state("layoutMobSocial.twoColumns.editProfile",
//                {
//                    url: "/edit-profile/?tab",
//                    resolve: {
//                        resolver: function (controllerProvider) {
//                            return controllerProvider
//                                .resolveBundles(["videogular", "fileUpload", "media", "users", "education", "skillPublic"]);
//                        }
//                    },
//                    views: {
//                        "left": {
//                            templateUrl: "pages/users/user.profile.edit.navigation.html"
//                        },
//                        "right": {
//                            templateUrl: function ($stateParams, state) {
//                                if ([undefined, "basic", "education", "skills"].indexOf($stateParams.tab) === -1) {
//                                    return "pages/common/404.html";
//                                }
//                                return "pages/users/user.profile.edit." + ($stateParams.tab || "basic") + ".html";
//                            },
//                            resolve: {
//                                resolver: function (controllerProvider) {
//                                    return controllerProvider
//                                        .resolveBundles(["education", "skillPublic"]);
//                                }
//                            },
//                            controllerProvider: function ($stateParams) {
//                                if (!$stateParams.tab)
//                                    return "userEditController";
//                                switch ($stateParams.tab) {
//                                case "basic":
//                                    return "userEditController";
//                                case "education":
//                                    return "educationController";
//                                case "skills":
//                                    return "skillController";

//                                }
//                            }
//                        }
//                    }
//                });
//        $stateProvider.state("layoutZero.404",
//            {
//                templateUrl: "pages/common/404.html"
//            });
//        $urlRouterProvider.otherwise(function ($injector, $location) {
//            var state = $injector.get('$state');
//            state.go('layoutZero.404');
//            return $location.path();
//        });

//        // use the HTML5 History API
//        $locationProvider.html5Mode(true);


//        ngMetaProvider.useTitleSuffix(true);
//        ngMetaProvider.setDefaultTitle('Top of IT');
//        ngMetaProvider.setDefaultTitleSuffix(' | VNIT.TOP');
//        ngMetaProvider.setDefaultTag('author', 'Cao Thế Toàn');
//    }]);