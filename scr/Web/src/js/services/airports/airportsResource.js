'use strict';

angular.module('extraAir').factory("airportsResource", function ($resource) {

    var URL = Constants.REST_URL + 'api/airports/:id';
    return {
        airportResource: $resource(URL,
            null,
            {
                get: {
                    method: 'GET',
                    isArray: true,
                    timeout: 30000
                }
            }
        ),

        getAirports: function () {
            return this.airportResource.get();
        },

        getAirport: function (id) {
            return this.airportResource.get({id: id});
        }
    }
});