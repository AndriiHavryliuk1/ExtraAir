'use strict';

angular.module('extraAir').factory('tourStatusService', ['$q', 'tourStatusResource',
    function ($q, tourStatusResource) {

        function TourStatusService() {

        }

        TourStatusService.prototype.constructor = TourStatusService;


        /**
         * Get tour statuses.
         * @public
         * @param {!int} id - tour id
         * @return {!promise}
         * @method
         **/
        TourStatusService.prototype.getTourStatuses = function ( additionUrlParams) {
            return tourStatusResource.getTourStatuses(additionUrlParams).$promise;
        };


        TourStatusService.prototype.saveTourStatus = function (tourDetails) {
            var tour = tourStatusResource.saveTourTourStatus(tourDetails);
            return tour.$promise;
        };

        TourStatusService.prototype.updateTourStatus = function (tourDetails) {
            var tour = tourStatusResource.updateTourStatus(tourDetails);
            return tour.$promise;
        };


        return new TourStatusService();
    }
]);

