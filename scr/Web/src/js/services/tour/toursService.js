'use strict';

angular.module('extraAir').factory('toursService', ['$q', 'toursResource',
    function ($q, toursResource) {

        function ToursService() {

        }

        ToursService.prototype.constructor = ToursService;

        /**
         * Get all tours.
         * @public
         * @return {!promise}
         * @method
         **/
        ToursService.prototype.getTours = function () {
            var tour = toursResource.getTours();
            if (tour === null) {

            }
            return tour.$promise;
        };

        /**
         * Get tour.
         * @public
         * @param {!int} id - tour id
         * @return {!promise}
         * @method
         **/
        ToursService.prototype.getTour = function (id) {
            var tour = toursResource.getTour(id);
            if (tour === null) {

            }
            return tour.$promise;
        };

        return new ToursService();
    }
]);

