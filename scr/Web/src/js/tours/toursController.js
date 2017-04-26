'use strict';

var app = angular.module('extraAir');
app.controller('toursController', function($rootScope, $scope, $location, $window,  $filter, getService, airportsService, crossingService){


    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    setupURL();

    var restURL = 'api/tours/bysearch?airportFromId=' + $scope.tourSearchInfo.origin + '&airportToId='
        + $scope.tourSearchInfo.destination + '&dayStart=' + $scope.tourSearchInfo.date.day;

    getService.GetObjects(restURL).then(function(data){
            $scope.tours = data.data;
            correctData(1);
    }, function(){
            console.log("error");
    });


    $scope.goToDetailPage = function (tour) {
        crossingService.setTour(tour);
        console.log(tour);
        $window.location.href = "#/tours/" + tour.TourId;
    };

    function getSearchInfoURL() {
        return {
            origin: $location.search().airportFromId,
            destination: $location.search().airportToId,
            date:{
                day: $location.search().dayStart,
                allDate: $location.search().dateStart
            }
        }
    }

    function correctData(coef){
        for (var i = 0; i < $scope.tours.length; i++){
            var d = new Date($scope.tourSearchInfo.date.allDate);
            $scope.tours[i].timeStart = $filter('date')($scope.tours[i].DateStart, 'HH:mm');
            $scope.tours[i].timeFinish = $filter('date')($scope.tours[i].DateFinish, 'HH:mm');
            $scope.tours[i].dateStartR = $filter('date')($scope.tourSearchInfo.date.allDate, 'dd-MM-yyyy');
            $scope.tours[i].dateFinishR = new Date($filter('date')($scope.tours[i].DateFinish, 'dd-MM-yyyy')) >
                new Date($filter('date')($scope.tours[i].DateStart, 'dd-MM-yyyy')) ?
                $filter('date')(new Date(d.setDate(d.getDate() + 1)), 'dd-MM-yyyy')
                : $scope.tours[i].dateStartR;
            $scope.tours[i].Price = ($scope.tours[i].Price * coef).toFixed(0);
            $scope.tours[i].dayStart = translateDays($scope.tourSearchInfo.date.day);
            $scope.tours[i].dayFinish = translateDays(Constants.DAYS[
                new Date(d).getDay()]);
        }
    }

    function translateDays(day){
        switch(day){
            case Constants.DAYS[1]:
                return 'Понеділок';
                break;
            case Constants.DAYS[2]:
                return 'Вівторок';
                break;
            case Constants.DAYS[3]:
                return 'Середа';
                break;
            case Constants.DAYS[4]:
                return 'Четвер';
                break;
            case Constants.DAYS[5]:
                return "П'ятниця";
                break;
            case Constants.DAYS[6]:
                return 'Субота';
                break;
            case Constants.DAYS[0]:
                return 'Неділя';
                break;
        }
    }

    function setupURL(){
        //set url
        $location.search('airportFromId', $scope.tourSearchInfo.origin);
        $location.search('airportToId', $scope.tourSearchInfo.destination);
        $location.search('dateStart', $filter('date')($scope.tourSearchInfo.date.allDate, 'yyyy-MM-dd'));
        $location.search('dayStart', $scope.tourSearchInfo.date.day);
    }


});