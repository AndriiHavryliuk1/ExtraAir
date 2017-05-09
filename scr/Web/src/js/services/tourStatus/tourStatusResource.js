'use strict';

angular.module('extraAir').factory("tourStatusResource", function ($resource) {

    var URL = Constants.REST_URL + 'api/TourStatus/:id';
    return {
        tourStatusResource: $resource(URL,
            null,
            {
                getaAll: {
                    method: 'GET',
                    timeout: 30000,
                    params:{
                        tourId: '@tourId',
                        dateStart: '@dateStart',
                        dateFinish: '@dateFinish',
                        airportFromId: '@airportFromId',
                        airportToId: '@airportToId'
                    },
                    isArray: true
                },
                post: {
                    method: 'POST',
                    timeout: 30000
                },
                put: {
                    method: 'PUT',
                    timeout: 30000
                }
            }
        ),

        getTourStatuses: function (additionUrlParams) {
            return this.tourStatusResource.get({
                tourId: additionUrlParams.tourId,
                dateStart: additionUrlParams.dateStart,
                dateFinish: additionUrlParams.dateFinish,
                airportFromId: additionUrlParams.airportFromId,
                airportToId: additionUrlParams.airportToId
            });
        },

        updateTourStatus: function(tourDetail) {
            return this.tourStatusResource.put(tourDetail);
        },

        saveTourTourStatus: function(tourDetail){
            return this.tourStatusResource.post(tourDetail);
        }
    }
});