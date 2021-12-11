window.namekApp.service("moduleService", ["globalApiEndPoint", "webClientService", "$http", function (globalApiEndPoint, webClientService, $http) {

    var apiEndPoint = globalApiEndPoint + "/module";
    // get
    this.get = function (params, success, error) {
        webClientService.get(apiEndPoint + "/get", params, success, error);
    }

    this.getAll = function (params, success, error) {
        webClientService.get(apiEndPoint + "/getAll", params, success, error);
    }

    this.getById = function (id, success, error) {
        webClientService.get(apiEndPoint + "/getById/" + id, null, success, error);
    }

    this.post = function(model, success, error) {
        webClientService.post(apiEndPoint + "/post", model, success, error);
    }

    this.put = function (params, success, error) {
        webClientService.put(apiEndPoint + "/put", params, success, error);
    };

    this.delete = function (params, success, error) {
        webClientService.delete(apiEndPoint + "/delete", params, success, error);
    }

}]);