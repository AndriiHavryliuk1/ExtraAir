'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function($rootScope, $scope, getService, airportsResource){


    $scope.showPlaceTo = false;

    getService.GetObjects('api/airports').then(function(data){
        $scope.allAirports = data.data;
        $scope.adventAirports = undefined;
    }, function(){ });

    $scope.getAdventAirport = function(selectedAirport){
        $scope.adventAirports = undefined;
        if (!!selectedAirport && selectedAirport.AirportId !== undefined) {
            getService.GetObjects('api/airports/' + selectedAirport.AirportId).then(function (data) {
                $scope.adventAirports = data.data;
                $scope.showPlaceTo = !!data.data.length;
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }, function () {  });
        }
        else {
            $scope.showPlaceTo = false;
        }
    }

    function resetAutocomplete(){
        $scope.searchItem  = null;
        $scope.item = null;
    }




});