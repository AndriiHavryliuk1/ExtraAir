'use strict';

angular.module('extraAir').factory('airportsService', ['$q', 'airportsResource',
    function ($q, airportsResource) {

        function AirportsService() {

        }

        AirportsService.prototype.constructor = AirportsService;

        /**
         * Get all roles.
         * @public
         * @return {!promise}
         * @method
         **/
        AirportsService.prototype.getAirports = function () {
            var roles = airportsResource.getAirports();
            if (roles === null) {

            }
            return roles.$promise;
        };

        /**
         * Get role.
         * @public
         * @param {!int} id - airport id
         * @return {!promise}
         * @method
         **/
        AirportsService.prototype.getAirport = function (id) {
            var airport = airportsResource.getAirport(id);
            if (airport === null) {

            }
            return airport.$promise;
        };

        return new AirportsService();
    }
]);

