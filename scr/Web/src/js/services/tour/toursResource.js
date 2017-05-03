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
                    timeout: 30000,
                    params:{
                        page: '@page',
                        itemsPerPage: '@itemsPerPage',
                        search: '@search',
                        airportFromId: '@airportFromId',
                        airportToId: '@airportToId',
                        day: '@day'
                    }
                },
                get: {
                    method: 'GET',
                    timeout: 30000
                }
            }
        ),

        getTours: function (additionUrlParams) {
            return this.tourResource.getAll({
                page: additionUrlParams.page,
                itemsPerPage: additionUrlParams.itemsPerPage,
                search: additionUrlParams.search,
                airportFromId: additionUrlParams.airportFromId,
                airportToId: additionUrlParams.airportToId,
                day: additionUrlParams.day
            });
        },

        getTour: function (id) {
            return this.tourResource.get({id: id});
        }
    }
});