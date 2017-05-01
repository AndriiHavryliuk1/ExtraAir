'use strict';

angular.module('extraAir').factory('tourDetailsService', ['$q', 'tourDetailsResource',
    function ($q, tourDetailsResource) {

        function TourDetailsService() {

        }

        TourDetailsService.prototype.constructor = TourDetailsService;


        /**
         * Get tour.
         * @public
         * @param {!int} id - tour id
         * @return {!promise}
         * @method
         **/
        TourDetailsService.prototype.getTourDetails = function (id, additionUrlParams) {
            var tour = tourDetailsResource.getTourDetails(id, additionUrlParams);

            return tour.$promise;
        };



        TourDetailsService.prototype.saveTourDetails = function (tourDetails) {
            var tour = tourDetailsResource.saveTourDetails(tourDetails);
            return tour.$promise;
        };

        TourDetailsService.prototype.updateTourDetails = function (tourDetails) {
            var tour = tourDetailsResource.updateTourDetails(tourDetails);
            return tour.$promise;
        };


        return new TourDetailsService();
    }
]);

