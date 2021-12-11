window.namekApp.service("pageService", ["globalApiEndPoint", "webClientService", "$http", function (globalApiEndPoint, webClientService, $http) {

    var apiEndPoint = globalApiEndPoint + "/page";
    var actions = [{ id: 1, name: getLanguageText('PermissionRead') }, { id: 2, name: getLanguageText('PermissionWrite') }, { id: 4, name: getLanguageText('PageEdit') }, { id: 8, name: getLanguageText('PageDelete')}];
    var listActions = [{ id: 1, name: getLanguageText('PermissionRead'), has: false}, 
        { id: 2, name: getLanguageText('PermissionWrite'), has: false}, 
        { id: 4, name: getLanguageText('PageEdit'), has: false}, 
        { id: 8, name: getLanguageText('PageDelete'), has: false}];
    // get
    this.get = function (params, success, error) {
        webClientService.get(apiEndPoint + "/get", params, success, error);
    }

    this.getById = function (id, success, error) {
        webClientService.get(apiEndPoint + "/getById/" + id, null, success, error);
    }

    this.post = function(roleEntityModel, success, error) {
        webClientService.post(apiEndPoint + "/post", roleEntityModel, success, error);
    }

    this.put = function (params, success, error) {
        webClientService.post(apiEndPoint + "/UpdatePage", params, success, error);
    };

    this.delete = function (params, success, error) {
        webClientService.delete(apiEndPoint + "/delete", params, success, error);
    }

    this.getPageActions = function (actionKey) {
        for (var i = 0; i < actions.length; i++) {
            if ((actionKey & actions[i].id) === actions[i].id) {
                listActions[i].has = true;
            }
        }
        return listActions;
    }

    this.getPageActionsDesc = function (actionKey) {
        var ret = '';
        for (var i = 0; i < actions.length; i++) {
            if ((actionKey & actions[i].id) === actions[i].id) {
                if (ret !== '') ret += '; ';
                ret += actions[i].name;
            }
        }
        return ret;
    }

    this.getPagePermission = function(actions) {
        return actions.reduce(function(a, c) {
            return a + (c.has ? c.id : 0);
        }, 0);
    }

}]);