'use strict';

angular.module('extraAir').factory("toursResource", function ($resource) {

    var URL = Constants.REST_URL + 'api/tours/:id';
    return {
        tourResource: $resource(URL,
            null,
            {
                getAll: {
                    method: 'GET',
                    isArray: true,
                    timeout: 30000
                },
                get: {
                    method: 'GET',
                    timeout: 30000
                }
            }
        ),

        getTours: function () {
            return this.tourResource.getAll();
        },

        getTour: function (id) {
            return this.tourResource.get({id: id});
        }
    }
});