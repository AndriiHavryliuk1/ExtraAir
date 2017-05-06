'use strict';

var app = angular.module('extraAir');
app.controller('toursController', function ($rootScope, $scope, $location, $window, $filter, getService, airportsService, crossingService) {


    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    $scope.isLoading = true;
    setupURL();

    var utils = new CommonUtils();
    var restURL = 'api/tours/bysearch?airportFromId=' + $scope.tourSearchInfo.origin + '&airportToId='
        + $scope.tourSearchInfo.destination + '&dayStart=' + $scope.tourSearchInfo.date.day;

    getService.GetObjects(restURL).then(function (data) {
        $scope.tours = data.data;
        $scope.tours.tourClass = $scope.tourSearchInfo.tourClass;
        var coef = $scope.tours.tourClass === 'Economy' ? Constants.PRICE_COEF.ECONOMY : Constants.PRICE_COEF.BUSSINESS;
        correctData(coef);
    }, function () {
        console.log("error");
    }).finally(function () {
        $scope.isLoading = false;
    });


    $scope.goToDetailPage = function (tour) {
        tour.tourClass = $scope.tours.tourClass;
        tour.passengerCount = $scope.tourSearchInfo.passengerCount;
        crossingService.setTour(tour);
        console.log(tour);
        $window.location.href = "#/tours/" + tour.TourId;
    };

    function getSearchInfoURL() {
        return {
            origin: $location.search().airportFromId,
            destination: $location.search().airportToId,
            date: {
                day: $location.search().dayStart,
                allDate: $location.search().dateStart
            },
            tourClass: $location.search().tourClass,
            passengerCount: $location.search().passengerCount
        }
    }

    function correctData(coef) {
        for (var i = 0; i < $scope.tours.length; i++) {
            var d = new Date($scope.tourSearchInfo.date.allDate);
            $scope.tours[i].timeStart = $filter('date')($scope.tours[i].DateStart, 'HH:mm');
            $scope.tours[i].timeFinish = $filter('date')($scope.tours[i].DateFinish, 'HH:mm');
            $scope.tours[i].dateStartR = $filter('date')($scope.tourSearchInfo.date.allDate, 'dd-MM-yyyy');
            $scope.tours[i].dateFinishR = new Date($filter('date')($scope.tours[i].DateFinish, 'dd-MM-yyyy')) >
            new Date($filter('date')($scope.tours[i].DateStart, 'dd-MM-yyyy')) ?
                $filter('date')(new Date(d.setDate(d.getDate() + 1)), 'dd-MM-yyyy')
                : $scope.tours[i].dateStartR;
            $scope.tours[i].Price = ($scope.tours[i].Price * coef).toFixed(0);
            $scope.tours[i].dayStart = utils.translateDays($scope.tourSearchInfo.date.day);
            $scope.tours[i].dayFinish = utils.translateDays(Constants.DAYS[
                new Date(d).getDay()]);
            $scope.tours[i].dayStartO = $scope.tourSearchInfo.date.day;
            $scope.tours[i].dayFinishO = Constants.DAYS[new Date(d).getDay()];
        }
    }

    function setupURL() {
        //set url
        $location.search('airportFromId', $scope.tourSearchInfo.origin);
        $location.search('airportToId', $scope.tourSearchInfo.destination);
        $location.search('dateStart', $filter('date')($scope.tourSearchInfo.date.allDate, 'yyyy-MM-dd'));
        $location.search('dayStart', $scope.tourSearchInfo.date.day);
        $location.search('tourClass', $scope.tourSearchInfo.tourClass);
        $location.search('passengerCount', $scope.tourSearchInfo.passengerCount);
    }


});