/*
 page controller
 code by caothetoan

 */

window.namekApp.controller("pageController", [
    "$scope", "pageService",
    "moduleService",
   function ($scope, pageService, moduleService) {

        $scope.error = false;
        $scope.loading = false;

        // lấy ds
        $scope.total = 0;
        $scope.page = {};
        $scope.pages = []
            , $scope.currentPage = 1
            , $scope.itemsPerPage = window.Configuration.itemsPerPage
            , $scope.maxSize = 5;
        $scope.request = {
            page: $scope.currentPage,
            count: $scope.itemsPerPage
        };
        $scope.get = function (pageNumber) {
            if (!pageNumber) pageNumber = $scope.currentPage;
            $scope.request.page = pageNumber;

            pageService.get(
                $scope.request,
                function(response) {
                    if (response.success) {
                        $scope.pages = response.responseData;
                        $scope.total = response.total;
                    } else {
                        $scope.error = true;
                        $scope.errorMessage = response.message;
                    }
                },
                function(response) {

                });
        };
        $scope.getActionDesc = function(permission) {
            return pageService.getPageActionsDesc(permission);
        };
        $scope.getById = function (id) {
            //id = id || $stateParams.id;
            if (!id || id === 0) {
                $scope.page.actions = pageService.getPageActions(0);
                return;
            }
            pageService.getById(id,
                function (response) {
                    if (response.success) {
                        if (response.responseData) {
                            $scope.page = response.responseData;
                            $scope.page.actions = pageService.getPageActions($scope.page.permission);
                        } else {
                            window.location.href = '/page';
                        }
                    }
                },
                function () {

                });
        }

        $scope.save = function () {
            var method = $scope.page.id ? "put" : "post";
            var isOld = $scope.page.id > 0;
            alert("1");
            console.log($scope.page);
            $scope.page.permission = pageService.getPagePermission($scope.page.actions);
            alert("21");
            pageService[method]($scope.page,
                function (response) {
                    if (response.success) {
                        $scope.pages = $scope.pages || [];
                        $scope.page = response.responseData;
                        if (!isOld)
                        {
                            $scope.pages.push($scope.page);
                        }
                        if (response.url) {
                            window.location.href = response.url;
                        } else {
                            window.location.href = '/page';
                        }
                    }
                },
                function () {

                });
        }

        $scope.delete = function (id) {
            if (!id || id === 0)
                return;
            swal({
                title: getLanguageText('GlobalShoppingCartStepConfirm'),
                text: 'Bạn có chắc chắn muốn xóa?',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: getLanguageText('ScriptToResetYourPassword'),
                cancelButtonText: getLanguageText('ScriptGet'),
                confirmButtonClass: 'btn blue btn-confirm-swal',
                cancelButtonClass: 'btn default',
                buttonsStyling: false,
                allowOutsideClick: false,
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function(resolve, reject) {
                        pageService.delete(
                            { id: id },
                            function (response) {
                                if (response.success) {
                                    swal({
                                        type: 'success',
                                        title: getLanguageText('GlobalSuccess'),
                                        text: getLanguageText('ScriptArePasswordReset'),
                                        timer: 2000
                                    }).then(function() {
                                        window.location.reload();
                                    }, function(dismiss) {
                                        window.location.reload();
                                    });
                                    //delete in the list
                                    for (var i = 0; i < $scope.pages[i]; i++) {
                                        var e = $scope.pages[i];
                                        if (e.id !== $scope.page.id)
                                            continue;
                                        $scope.pages.splice(i, 1);
                                        break;
                                    }
                                    if (response.url) {
                                        window.location.href = response.url;
                                    } else {
                                        window.location.href = '/page';
                                    }
                                }
                            },
                            function () {

                            });
                    });
                }
            }).then(function() {
            
            }, function(dismiss) {
            
            });
        }

       $scope.getModules = function (pageNumber) {
           if (!pageNumber) pageNumber = $scope.currentPage;

           $scope.moduleRequest = {};
           $scope.moduleRequest.page = pageNumber;
           $scope.moduleRequest.count = 100;
           $scope.moduleRequest.ascending = true;

           moduleService.get(
               $scope.moduleRequest,
               function (response) {
                   if (response.success) {
                       $scope.modules = response.responseData;

                   } else {
                       $scope.error = true;
                       $scope.errorMessage = response.message;
                   }
               },
               function (response) {

               });
       };

        $scope.init = function() {

        }();
    }
]);