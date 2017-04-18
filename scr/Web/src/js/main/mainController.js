'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function($rootScope, $scope, getService, airportsService){


    $scope.showPlaceTo = false;

    var a = airportsService.getAirports();
     airportsService.getAirports().then(function(data){
        $scope.allAirports = data;
        $scope.adventAirports = undefined;
    }, function(){ });

    $scope.getAdventAirport = function(selectedAirport){
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

    function resetAutocomplete(){
        $scope.searchItem  = null;
    }




});