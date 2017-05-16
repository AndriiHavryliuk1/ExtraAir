'use strict';

var app = angular.module('extraAir');
app.controller('tourStatusController', function ($rootScope, $scope, $location, $window, $filter, tourStatusService, getService,
                                                 airportsService) {

    airportsService.getAirports().then(function(data){
        $scope.airports = data;
    });

    $scope.tourStatusParams = {
        tourId: null,
        dateStart: null,
        dateFinish: null,
        airportFromId: null,
        airportToId: null
    };

    $scope.search = function(){
        $scope.tourStatusParams.dateStart = $scope.tourStatusParams.dateStart !== null ?
            $filter('date')($scope.tourStatusParams.dateStart, 'MM/dd/yyyy HH:mm') : null;
        $scope.tourStatusParams.dateFinish = $scope.tourStatusParams.dateFinish !== null ?
            $filter('date')($scope.tourStatusParams.dateFinish, 'MM/dd/yyyy HH:mm') : null;
        var URL = "api/TourStatus?tourId=" + $scope.tourStatusParams.tourId + "&dateStart=" + $scope.tourStatusParams.dateStart +
                "&dateFinish=" + $scope.tourStatusParams.dateFinish + "&airportFromId=" + $scope.tourStatusParams.airportFromId +
                "&airportToId=" + $scope.tourStatusParams.airportToId;

        getService.GetObjects(URL).then(function (data) {
            $scope.tourStatuses = data.data;
            $scope.tourStatuses = correctData($scope.tourStatuses);
            //  paginationService.ChangeURL($scope.loadList, $scope.tours, $rootScope.preArray, '/toursList', $rootScope.pagingInfo);
            $scope.isLoading = false
        }, function (error) { }).finally(function(){
            $scope.isLoading = false;
        });
    };

    function correctData(data) {
        data.forEach(function(status){
            for (var i = 0; i < Constants.TOUR_STATUSES.length; i++) {
                if (Constants.TOUR_STATUSES[i].KEY === status.TourStatusType) {
                    status.statusValue = Constants.TOUR_STATUSES[i].VALUE;
                }
                status.dateStartView = $filter('date')(status.DateStart, 'dd-MM-yyyy HH:mm');
                status.dateFinishView = $filter('date')(status.DateFinish, 'dd-MM-yyyy HH:mm');
            }
        });
        return data;
    }


});