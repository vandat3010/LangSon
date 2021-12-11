window.namekApp.service("CommonService", ["globalApiEndPoint", "webClientService", "$http", function (globalApiEndPoint, webClientService, $http) {

    var apiEndPoint = globalApiEndPoint + "/Common";
    var actions = [{id: 1, name: getLanguageText('ScriptRead')}, {id: 2, name: getLanguageText('ScriptWrite')}, {id: 4, name: getLanguageText('ScriptEdit')}, {id: 8, name: getLanguageText('ScriptDelete')}];
    var listActions = [{id: 1, name: getLanguageText('ScriptRead'), has: false}, 
                       {id: 2, name: getLanguageText('ScriptWrite'), has: false}, 
                       {id: 4, name: getLanguageText('ScriptEdit'), has: false}, 
                       {id: 8, name: getLanguageText('ScriptDelete'), has: false}];
   
    //DucAnh: get danh sach Agency
    this.getListAgency = function (params, success, error) {
        webClientService.get(apiEndPoint + "/GetListAgency", params, success, error);
    }
    this.GetListUser = function (params, success, error) {
        webClientService.get(apiEndPoint + "/GetListUser", params, success, error);
    }
    this.GetListUserByRoleId = function (params, success, error) {
        webClientService.get(apiEndPoint + "/GetListUserByRoleId", params, success, error);
    }
    this.GetListDaiLy = function (params, success, error) {
        webClientService.get(apiEndPoint + "/GetListDaiLy", params, success, error);
    }
    this.GetProvince = function (params, success, error) {
        webClientService.get(apiEndPoint + "/GetProvince", params, success, error);
    }
}]);