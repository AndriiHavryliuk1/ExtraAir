angular.module('extraAir').factory("airportsResource", function (REST_URL, $resource) {

    var URL = REST_URL.url + 'api/airports/:id';
    return $resource(URL, null, {
        getAll: {
            method: 'GET',
            isArray: true,
            timeout: 30000
        },
        get: {
            method: 'GET',
            isArray: true,
            timeout: 30000
        },

        getAirports: function () {
            return this.getAll();
        },

        getAirport: function (id) {
            return this.get({id: id}).$promise;
        }
    });
});
