'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function($rootScope, $scope, $window, getService, airportsService, crossingService){
    $scope.tourDetails = {
        origin: null,
        destination: null,
        date: null,
        passengerCount: 1
    };

    $scope.showPlaceTo = false;

     airportsService.getAirports().then(function(data){
        $scope.allAirports = data;
        $scope.adventAirports = undefined;
    }, function(){ });

    $scope.getAdventAirport = function(selectedAirportId){
        $scope.tourDetails.origin = !!selectedAirportId ? selectedAirportId : null;
        $scope.adventAirports = undefined;
        resetAutocomplete();
        if (!!selectedAirportId) {
            airportsService.getAirport(selectedAirportId).then(function (data) {
                $scope.adventAirports = data;
                $scope.showPlaceTo = !!data.length;
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }, function () {  });
        }
        else {
            $scope.showPlaceTo = false;
        }
    };

    $scope.prepareTo = function() {
        $scope.tourDetails.date = {
            day: Constants.DAYS[$scope.tourDetails.date.getDay()],
            allDate: $scope.tourDetails.date
        };
        crossingService.setTour($scope.tourDetails);
        console.log($scope.tourDetails);
        $window.location.href = "#/tours";
    };


    function resetAutocomplete() {
        $scope.searchItem  = null;
    }
});