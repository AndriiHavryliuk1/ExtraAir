'use strict';

angular.module('extraAir').factory("tourDetailsResource", function ($resource) {

    var URL = Constants.REST_URL + 'api/TourDetails/:id';
    return {
        tourDetailsResource: $resource(URL,
            null,
            {
                get: {
                    method: 'GET',
                    timeout: 30000,
                    params:{
                        comfort: '@comfort',
                        dateStart: '@dateStart',
                        dateFinish: '@dateFinish'
                    }
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

        getTourDetails: function (id, additionUrlParams) {
            return this.tourDetailsResource.get({id: id}, {
                comfort: additionUrlParams.comfort,
                dateStart: additionUrlParams.dateStart,
                dateFinish: additionUrlParams.dateFinish
            });
        },

        updateTourDetails: function(tourDetail) {
            return this.tourDetailsResource.put(tourDetail);
        },

        saveTourDetails: function(tourDetail){
            return this.tourDetailsResource.post(tourDetail);
        }
    }
});