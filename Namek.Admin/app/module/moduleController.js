/*
 module controller
 code by caothetoan

 */

window.namekApp.controller("moduleController", [
    "$scope", "moduleService",
   function ($scope, moduleService) {

        $scope.error = false;
        $scope.loading = false;

        // lấy ds
        $scope.total = 0;
        $scope.module = {};
        $scope.modules = []
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
           
           moduleService.get(
               $scope.request,
               function (response) {
                   console.log(response.responseData);
                   if (response.success) {
                       $scope.modules = response.responseData;
                       $scope.total = response.total;
                   } else {
                       $scope.error = true;
                       $scope.errorMessage = response.message;
                   }
               },
               function(response) {

               });
       };
       $scope.getAll = function () {
           moduleService.getAll(
               function(response) {
                   if (response.success) {
                       console.log(response);
                       $scope.modules = response.responseData;
                       $scope.total = response.total;
                       if (!$scope.module.parentId) {
                           $scope.module.parentId = $scope.modules[0].id;
                       }
                   } else {
                       $scope.error = true;
                       $scope.errorMessage = response.message;
                   }
               },
               function(response) {

               });
       };
        $scope.getById = function (id) {
            //id = id || $stateParams.id;
            if (!id || id === 0)
                return;
            moduleService.getById(id,
                function (response) {
                    if (response.success) {
                        if (response.responseData)
                            $scope.module = response.responseData;
                        else
                            window.location.href = '/module';
                    }
                },
                function () {

                });
        }

        $scope.save = function () {
            var method = $scope.module.id ? "put" : "post";
            var isOld = $scope.module.id > 0;
            moduleService[method]($scope.module,
                function (response) {
                    if (response.success) {
                        $scope.modules = $scope.modules || [];
                        $scope.module = response.responseData;
                        if (!isOld)
                        {
                            $scope.modules.push($scope.module);
                        }
                        if (response.url) {
                            window.location.href = response.url;
                        } else {
                            window.location.href = '/module';
                        }
                    }
                },
                function () {

                });
        }

        $scope.delete = function (id) {
            if (!id || id === 0)
                return;
            moduleService.delete(
                { id: id },
                function (response) {
                    if (response.success) {
                        //delete in the list
                        for (var i = 0; i < $scope.modules[i]; i++) {
                            var e = $scope.modules[i];
                            if (e.id !== $scope.module.id)
                                continue;
                            $scope.modules.splice(i, 1);
                            break;
                        }
                        if (response.url) {
                            window.location.href = response.url;
                        } else {
                            window.location.href = '/module';
                        }
                    }
                },
                function () {

                });
        }

        $scope.init = function() {

        }();
    }
]);

