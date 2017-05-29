var app = angular.module('extraAir');
app.controller('confirmRegistrationController', function( $rootScope, $scope, $http, $routeParams) {
    $scope.confirmed = $routeParams["id"] != undefined;
    var state = true;
    if ($routeParams["id"] != undefined) {
        $http.put(Constants.REST_URL + "api/clients/changeState/" + $routeParams["id"], state, {
            headers: {
                'Content-type': 'application/json'
            }
        }).then(function(){
            $scope.confirmed = true;
        }, function(){
            alert("We have some trouble with our server please try again!");
        });
    }
});