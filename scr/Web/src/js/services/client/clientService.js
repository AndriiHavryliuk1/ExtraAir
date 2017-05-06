'use strict';

angular.module('extraAir').factory('clientService', ['$q', 'clientResource',
    function ($q, clientResource) {

        function ClientsService() {

        }

        ClientsService.prototype.constructor = ClientsService;


        /**
         * Get tour.
         * @public
         * @param {!int} id - tour id
         * @return {!promise}
         * @method
         **/
        ClientsService.prototype.getClient = function (id) {
            var tour = clientResource.getClient(id);
            return tour.$promise;
        };

        ClientsService.prototype.saveClient = function (client) {
            var tour = clientResource.saveClient(client);
            return tour.$promise;
        };

        ClientsService.prototype.updateClient = function (client) {
            var tour = clientResource.updateClient(client);
            return tour.$promise;
        };


        return new ClientsService();
    }
]);

