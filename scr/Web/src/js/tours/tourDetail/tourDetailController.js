'use strict';

var app = angular.module('extraAir');
app.controller('tourDetailController', function ($rootScope, $scope, $location, $filter, $routeParams, getService, airportsService,
                                                 tourDetailsService, toursService, crossingService) {
    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    setupURL();
    $scope.activePlacesList = [];
    $scope.ordering = false;
    $rootScope.isAutorized = !!localStorage.getItem('token');

    var utils = new CommonUtils();
    toursService.getTour($routeParams.id).then(function (data) {
        $scope.tour = data;
        var coef = $scope.tourSearchInfo.tourClass === 'Economy' ? Constants.PRICE_COEF.ECONOMY : Constants.PRICE_COEF.BUSSINESS;
        correctData(coef);
    });


    $scope.setActivePlaces = function (coordinate) {
        $scope.allPlaces.forEach(function (place) {
            place.forEach(function (point) {
                if (coordinate.x === point.coordinate.x && coordinate.y === point.coordinate.y) {
                    if (point.active) {
                        point.active = false;
                        for (var i = 0; i < $scope.activePlacesList.length; i++) {
                            if ($scope.activePlacesList[i].coordinate.x === coordinate.x
                                && $scope.activePlacesList[i].coordinate.y === coordinate.y) {

                                for (var j = 0; j < $scope.allPassengers.length; j++) {
                                    if ($scope.activePlacesList[i].passengerIndex === j){
                                        $scope.allPassengers[j].coordinate = null;
                                        $scope.allPassengers[j].coordinateValue = null;
                                        break;
                                    }
                                }
                                $scope.activePlacesList.splice(i, 1);
                                break;
                            }
                        }
                    }
                    else {
                        if ($scope.activePlacesList.length >= $scope.tourSearchInfo.passengerCount) {
                            return;
                        }
                        point.active = true;
                        for (var i = 0; i < $scope.allPassengers.length; i++) {
                            if ($scope.allPassengers[i].coordinate === null) {
                                $scope.activePlacesList.push({
                                    coordinate: point.coordinate,
                                    value: point.value,
                                    passenger: $scope.allPassengers[i].name + ' ' + $scope.allPassengers[i].surname,
                                    passengerIndex: i
                                });
                                $scope.allPassengers[i].coordinate = point.coordinate;
                                $scope.allPassengers[i].coordinateValue = point.value;
                                break;
                            }
                        }
                    }
                }
            });
        });
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.openOrderingForm = function () {
        $scope.ordering = true;
        $scope.allPassengers = getAllPassenger();
        setTourDetails();
    };

    function setTourDetails() {
        $scope.allPlaces = $scope.allPlaces !== undefined ? $scope.allPlaces : getAllPlaces();
        var additionUrl = {
            comfort: $scope.tourSearchInfo.tourClass === 'Economy' ? 0 : 1,
            dateStart: getGeneralDateFormat($scope.tourSearchInfo.dateStartR, $scope.tourSearchInfo.timeStart),
            dateFinish: getGeneralDateFormat($scope.tourSearchInfo.dateFinishR, $scope.tourSearchInfo.timeFinish)
        };
        tourDetailsService.getTourDetails($routeParams.id, additionUrl).then(function (data) {
            setCheckedPlaces(data.BookedPoints);
            console.log(data);
        }, function (data) {
            console.log(data);
        })
    }

    function getAllPassenger() {
        var list = [];
        for (var i = 0; i < $scope.tourSearchInfo.passengerCount; i++) {
            list.push({
                name: '',
                surname: '',
                gender: null,
                idCard: '',
                baggage: {
                    inner: 0,
                    external: 0
                },
                methodRegister: null,
                coordinate: null,
                coordinateValue: null
            });
        }
        return list;
    }

    function setCheckedPlaces(bookedPoints) {
        if (bookedPoints === undefined) {
            return;
        }

        $scope.allPlaces.forEach(function (place) {
            place.forEach(function (point) {
                bookedPoints.forEach(function (booked) {
                    if (point.coordinate.x === booked.X && point.coordinate.y === booked.Y) {
                        booked.booked = true;
                    }
                });
            });
        });
    }


    function getAllPlaces() {
        var tmp = $scope.tourSearchInfo.tourClass === 'Economy' ? 'CountOfEconomyPassenger' : 'CountOfBusinessPassenger';
        var list = [];
        var res = [];
        var literals = ['A', 'B', 'C', 'D', 'E', 'F'];
        for (var i = 1, x = 0, y = 0; i <= $scope.tour.Plane.MaxCountPassenger[tmp]; i++, x++) {
            list.push({
                value: (y + 1) + literals[x],
                coordinate: {
                    x: x,
                    y: y
                },
                booked: false,
                active: false
            });
            if (i % 6 === 0) {
                res.push(list);
                list = [];
                x = -1;
                y++;
            }
        }
        return res;
    }

    function getGeneralDateFormat(date, time) {
        var from = date.split("-");
        return from[2] + '-' + from[1] + '-' + from[0] + 'T' + time + ':00';
    }

    function setupURL() {
        $location.search('dateStart', $scope.tourSearchInfo.dateStartR);
        $location.search('dateFinish', $scope.tourSearchInfo.dateFinishR);
        $location.search('timeStart', $scope.tourSearchInfo.timeStart);
        $location.search('timeFinish', $scope.tourSearchInfo.timeFinish);
        $location.search('dayStart', $scope.tourSearchInfo.dayStartO);
        $location.search('dayFinish', $scope.tourSearchInfo.dayFinishO);
        $location.search('tourClass', $scope.tourSearchInfo.tourClass);
        $location.search('passengerCount', $scope.tourSearchInfo.passengerCount);
    }

    function getSearchInfoURL() {
        return {
            dateStartR: $location.search().dateStart,
            dateFinishR: $location.search().dateFinish,
            timeStart: $location.search().timeStart,
            timeFinish: $location.search().timeFinish,
            dayStartO: $location.search().dayStart,
            dayFinishO: $location.search().dayFinish,
            tourClass: $location.search().tourClass,
            passengerCount: $location.search().passengerCount
        }
    }

    function correctData(coef) {
        $scope.tour.timeStart = $filter('date')($scope.tour.DateStart, 'HH:mm');
        $scope.tour.timeFinish = $filter('date')($scope.tour.DateFinish, 'HH:mm');
        $scope.tour.dateStartR = $scope.tourSearchInfo.dateStartR;
        $scope.tour.dateFinishR = $scope.tourSearchInfo.dateFinishR;
        $scope.tour.Price = ($scope.tour.Price * coef).toFixed(0);
        $scope.tour.dayStart = utils.translateDays($scope.tourSearchInfo.dayStartO);
        $scope.tour.dayFinish = utils.translateDays($scope.tourSearchInfo.dayFinishO);
    }
});