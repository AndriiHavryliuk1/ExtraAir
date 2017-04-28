'use strict';

var app = angular.module('extraAir');
app.controller('tourDetailController', function ($rootScope, $scope, $location, $filter, $routeParams, getService, airportsService, toursService, crossingService) {
    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    setupURL();

    var utils = new CommonUtils();
    toursService.getTour($routeParams.id).then(function (data) {
        $scope.tour = data;
        var coef = $scope.tourSearchInfo.tourClass === 'Economy' ? Constants.PRICE_COEF.ECONOMY : Constants.PRICE_COEF.BUSSINESS;
        correctData(coef);
    });

    function setupURL() {
        $location.search('dateStart', $scope.tourSearchInfo.dateStartR);
        $location.search('dateFinish', $scope.tourSearchInfo.dateFinishR);
        $location.search('timeStart', $scope.tourSearchInfo.timeStart);
        $location.search('timeFinish', $scope.tourSearchInfo.timeFinish);
        $location.search('dayStart', $scope.tourSearchInfo.dayStartO);
        $location.search('dayFinish', $scope.tourSearchInfo.dayFinishO);
        $location.search('tourClass', $scope.tourSearchInfo.tourClass);
    }

    function getSearchInfoURL() {
        return {
            dateStartR: $location.search().dateStart,
            dateFinishR: $location.search().dateFinish,
            timeStart: $location.search().timeStart,
            timeFinish: $location.search().timeFinish,
            dayStartO: $location.search().dayStart,
            dayFinishO: $location.search().dayFinish,
            tourClass: $location.search().tourClass
        }
    }

    function correctData(coef) {
        $scope.tour.timeStart = $filter('date')($scope.tour.DateStart, 'HH:mm');
        $scope.tour.timeFinish = $filter('date')($scope.tour.DateFinish, 'HH:mm');
        $scope.tour.dateStartR = $scope.tourSearchInfo.dateStartR;
        $scope.tour.dateFinishR = $scope.tourSearchInfo.dateFinishR;
        $scope.tour.Price = ($scope.tour.Price * coef).toFixed(0);
        $scope.tour.dayStart = utils.translateDays($scope.tourSearchInfo.dayStartO);
        $scope.tour.dayFinish = utils.translateDays($scope.tourSearchInfo.dayStartO);
    }
});