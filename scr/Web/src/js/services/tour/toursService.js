'use strict';

angular.module('extraAir').factory('toursService', ['$q', 'toursResource',
    function ($q, toursResource) {

        function ToursService() {

        }

        ToursService.prototype.constructor = ToursService;

        ToursService.prototype.getTours = function (additionUrlParams) {
            return toursResource.getTours(additionUrlParams).$promise;
        };

        ToursService.prototype.getTour = function (id) {
            var tour = toursResource.getTour(id);
            if (tour === null) {

            }
            return tour.$promise;
        };

        return new ToursService();
    }
]);

