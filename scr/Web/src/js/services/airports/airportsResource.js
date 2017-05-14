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
                },
                post: {
                    method: 'POST',
                    timeout: 30000
                },
                put: {
                    method: 'PUT',
                    timeout: 30000
                },
                delete: {
                    method: 'DELETE',
                    timeout: 30000
                }
            }
        ),

        getAirports: function () {
            return this.airportResource.get();
        },

        getAirport: function (id) {
            return this.airportResource.get({id: id});
        },

        putAirport: function (airport) {
            return this.airportResource.put(airport);
        },

        postAirport: function (airport) {
            return this.airportResource.post(airport);
        },

        deleteAirport: function (id) {
            return this.airportResource.delete({id: id});
        }
    }
});