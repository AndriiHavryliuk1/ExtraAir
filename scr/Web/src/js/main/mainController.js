'use strict';

var app = angular.module('extraAir');
app.controller('mainController', function ($rootScope, $scope, $window, getService, airportsService, crossingService) {
    $scope.tourDetails = {
        origin: null,
        destination: null,
        date: null,
        passengerCount: 1,
        tourClass: null
    };

    $scope.showPlaceTo = false;

    airportsService.getAirports().then(function (data) {
        $scope.allAirports = data;
        $scope.adventAirports = undefined;
    }, function () {
    });

    getService.GetObjects('api/tours/alltours').then(function (data) {
        $scope.allTours = data.data;
    });

    $scope.getAdventAirport = function (selectedAirportId) {
        $scope.tourDetails.origin = !!selectedAirportId ? selectedAirportId : null;
        $scope.adventAirports = undefined;
        if (!!selectedAirportId) {
            airportsService.getAirport(selectedAirportId).then(function (data) {
                $scope.adventAirports = data;
                $scope.showPlaceTo = !!data.length;
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }, function () {
            });
        }
        else {
            $scope.showPlaceTo = false;
        }
    };

    $scope.prepareTo = function () {
        for (var t in $scope.tourDetails) {
            if ($scope.tourDetails.hasOwnProperty(t) && $scope.tourDetails[t] === null) {
                alert("Заповніть усі поля");
                return;
            }
        }
        $scope.tourDetails.date = {
            day: Constants.DAYS[$scope.tourDetails.date.getDay()],
            allDate: $scope.tourDetails.date
        };
        if ($scope.tourDetails.date.allDate < new Date()) {
            alert("Невірно вибрана дата");
            return;
        }
        crossingService.setTour($scope.tourDetails);
        console.log($scope.tourDetails);
        $window.location.href = "#/tours";
    };


});