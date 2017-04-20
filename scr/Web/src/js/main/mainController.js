'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function($rootScope, $scope, getService, airportsService){



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

    airportsService.getAirports();
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
        $scope.tourDetails.date.day = getDay($scope.tourDetails.date);
        console.log($scope.tourDetails);
    };

    $scope.setAdventAirport = function(selectedAirport){
        $scope.tourDetails.destination = selectedAirport !== null ? selectedAirport.AirportId : null;
    };


    function getDay(d){
        var weekday = new Array(7);
        weekday[0] = "Sunday";
        weekday[1] = "Monday";
        weekday[2] = "Tuesday";
        weekday[3] = "Wednesday";
        weekday[4] = "Thursday";
        weekday[5] = "Friday";
        weekday[6] = "Saturday";
        return weekday[d.getDay()];
    }

    function resetAutocomplete() {
        $scope.searchItem  = null;
    }
});