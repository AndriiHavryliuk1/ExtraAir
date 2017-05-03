'use strict';

var app = angular.module('extraAir');
app.controller('toursListController', function ($rootScope, $scope, $location, $window, $filter, getService, airportsService, toursService, toursResource) {


    $rootScope.pagingInfo = {
        page: 1,
        itemsPerPage: 7,
        search: null,
        airportFromId: null,
        airportToId: null,
        day: ""
    };

    $scope.tours = [];


    var URL = "api/tours?page=" + $rootScope.pagingInfo.page + "&itemsPerPage=" + $rootScope.pagingInfo.itemsPerPage +
        "&search=" + $rootScope.pagingInfo.search + "&airportFromId=" + $rootScope.pagingInfo.airportFromId +
        "&airportToId=" + $rootScope.pagingInfo.airportToId + "&day=" + $rootScope.pagingInfo.day;

    getService.GetObjects(URL).then(function (data) {
        $scope.tours = data.data.list;
        $scope.toursListFullCount = data.data.count;


        $rootScope.preArray = $scope.tours;
        correctData();

        $location.search('search', !!$rootScope.pagingInfo.search ? $rootScope.pagingInfo.search : null);
        $location.search('page', $rootScope.pagingInfo.page);
        $location.search('airportFromId', $rootScope.pagingInfo.airportFromId);
        $location.search('airportToId', $rootScope.pagingInfo.airportToId);
        $location.search('day', $rootScope.pagingInfo.day);
        if (!$scope.$$phase) {
            $scope.$apply();
        }

    }, function (error) {
        console.log("dfsbdfb");

    });


    function correctData() {
        for (var i = 0; i < $scope.tours.length; i++) {
            $scope.tours[i].timeStart = $filter('date')($scope.tours[i].DateStart, 'HH:mm');
            $scope.tours[i].timeFinish = $filter('date')($scope.tours[i].DateFinish, 'HH:mm');
            $scope.tours[i].economyPrice = ($scope.tours[i].Price * Constants.PRICE_COEF.ECONOMY).toFixed(0);
            $scope.tours[i].businessPrice = ($scope.tours[i].Price * Constants.PRICE_COEF.BUSSINESS).toFixed(0);
        }
    }


});