angular.module("extraAir").factory('getService', [ '$http',  '$q','REST_URL',
    function ($http, $q,  REST_URL ){

        var GetObjects = function (control) {
            return  $http.get(REST_URL.url + control)
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