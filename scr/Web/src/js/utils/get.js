angular.module("extraAir").factory('getService', [ '$http',  '$q',
    function ($http, $q){

        var GetObjects = function (control) {
            return  $http.get(Constants.REST_URL + control)
                .then(function(responce) {
                    return $q.resolve(responce);
                })
                .catch(function(responce) {
                    return $q.reject(responce);
                });
        };
        return {
            GetObjects : GetObjects
        };
    }]);