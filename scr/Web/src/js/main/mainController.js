'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function($rootScope, $scope, $window, getService, airportsService, crossingService){
    $scope.tourDetails = {
        origin: null,
        destination: null,
        date: null,
        passenger: {}
    };

    $scope.passenger = {
        adult: 1,
        child: null,
        baby: null
    };
    $scope.showPlaceTo = false;

     airportsService.getAirports().then(function(data){
        $scope.allAirports = data;
        $scope.adventAirports = undefined;
    }, function(){ });

    $scope.getAdventAirport = function(selectedAirport){
        $scope.tourDetails.origin = selectedAirport !== null ? selectedAirport.AirportId : null;
        $scope.adventAirports = undefined;
        resetAutocomplete();
        if (!!selectedAirport && selectedAirport.AirportId !== undefined) {
            airportsService.getAirport(selectedAirport.AirportId).then(function (data) {
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
        $scope.tourDetails.passenger = $scope.passenger;
        $scope.tourDetails.date = {
            day: Constants.DAYS[$scope.tourDetails.date.getDay()],
            allDate: $scope.tourDetails.date
        };
        crossingService.setTour($scope.tourDetails);
        console.log($scope.tourDetails);
        $window.location.href = "#/tours";
    };

    $scope.setAdventAirport = function(selectedAirport){
        $scope.tourDetails.destination = selectedAirport !== null ? selectedAirport.AirportId : null;
    };


    function resetAutocomplete() {
        $scope.searchItem  = null;
    }
});