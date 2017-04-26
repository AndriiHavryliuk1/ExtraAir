'use strict';

var app = angular.module('extraAir');
app.controller('tourDetailController', function($rootScope, $scope, $location, $filter, getService, airportsService, crossingService){


    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    setupURL();
    

    function setupURL(){
        $location.search('dateStart', $scope.tourSearchInfo.dateStartR);
        $location.search('dateFinish', $scope.tourSearchInfo.dateFinishR);
        $location.search('timeStart', $scope.tourSearchInfo.timeStart);
        $location.search('timeFinish', $scope.tourSearchInfo.timeFinish);
        $location.search('dayStart', $scope.tourSearchInfo.dayStart);
        $location.search('dayFinish', $scope.tourSearchInfo.dayFinish);
    }

    function getSearchInfoURL(){
        return {
            dateStart: $location.search().dateStart,
            dateFinish: $location.search().dateFinish,
            timeStart: $location.search().timeStart,
            timeFinish: $location.search().timeFinish,
            dayStart: $location.search().dayStart,
            dayFinish: $location.search().dayFinish,
        }
    }
});