'use strict';

angular.module('extraAir').factory("clientResource", function ($resource) {

    var URL = Constants.REST_URL + 'api/Clients/:id';
    return {
        clientResource: $resource(URL,
            null,
            {
                get: {
                    method: 'GET',
                    timeout: 30000
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

        getClient: function (id) {
            return this.clientResource.get({id: id});
        },

        updateClient: function(client) {
            return this.clientResource.put(client);
        },

        saveClient: function(client){
            return this.clientResource.post(client);
        }
    }
});