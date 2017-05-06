'use strict';

var app = angular.module('extraAir');
app.controller('toursListController', function ($rootScope, $scope, $location, $window, $filter, getService, paginationArrayService, paginationService, airportsService, toursService, toursResource) {


    $scope.isLoading = true;

    initPaginationParams();


    airportsService.getAirports().then(function(data){
        $scope.airports = data;
    });



    var utils = new CommonUtils();

    $scope.$on('$locationChangeStart', function(e) {
        $scope.isLoading = true;
        initPaginationParams();
     //   paginationService.ChangeURL($scope.loadList, $scope.tours, $rootScope.preArray, '/toursList', $rootScope.pagingInfo);
    });



    $scope.tours = [];




    $scope.loadList = function(){

        $scope.isLoading = true;
        var URL = "api/tours?page=" + $rootScope.pagingInfo.page + "&itemsPerPage=" + $rootScope.pagingInfo.itemsPerPage +
            "&search=" + $rootScope.pagingInfo.search + "&airportFromId=" + $rootScope.pagingInfo.airportFromId +
            "&airportToId=" + $rootScope.pagingInfo.airportToId + "&day=" + $rootScope.pagingInfo.day;


        getService.GetObjects(URL).then(function (data) {
            $scope.tours = data.data.list;


            $scope.AnyElementOfList = data.data.count == 0;
            $rootScope.pagingInfo.totalItems = data.data.count;
            $scope.pages = Math.ceil(data.data.count / $rootScope.pagingInfo.itemsPerPage);
            $scope.paginArray = paginationArrayService.Array($scope.pages, $rootScope.pagingInfo.page);

            correctData();


            $scope.paginationScope = {
                data: !!$rootScope.pagingInfo ? {} : $rootScope.pagingInfo,
                list: $scope.paginArray
            };

            if (!$scope.$$phase) {
                $scope.$apply();
            }

          //  paginationService.ChangeURL($scope.loadList, $scope.tours, $rootScope.preArray, '/toursList', $rootScope.pagingInfo);
            $scope.isLoading

        }, function (error) { }).finally(function(){
            $scope.isLoading = false;
        });

        $location.search('search', !!$rootScope.pagingInfo.search ? $rootScope.pagingInfo.search : null);
        $location.search('page', $rootScope.pagingInfo.page);
        $location.search('airportFromId', $rootScope.pagingInfo.airportFromId);
        $location.search('airportToId', $rootScope.pagingInfo.airportToId);
        $location.search('day', !!$rootScope.pagingInfo.day ? $rootScope.pagingInfo.day : null);


        $rootScope.preArray = $scope.tours;
    };

    $scope.loadList();


    function initPaginationParams(){
        $rootScope.pagingInfo = {
            page: $location.search().page !== undefined ? $location.search().page :  1,
            itemsPerPage: 5,
            search: $location.search().search !== undefined ? $location.search().search : '',
            airportFromId: $location.search().airportFromId !== undefined ? $location.search().airportFromId : null,
            airportToId: $location.search().airportToId !== undefined ? $location.search().airportToId : null,
            day: $location.search().day !== undefined ? $location.search().day : ''
        };
    }



    function correctData() {
        for (var i = 0; i < $scope.tours.length; i++) {
            $scope.tours[i].timeStart = $filter('date')($scope.tours[i].DateStart, 'HH:mm');
            $scope.tours[i].timeFinish = $filter('date')($scope.tours[i].DateFinish, 'HH:mm');
            $scope.tours[i].economyPrice = ($scope.tours[i].Price * Constants.PRICE_COEF.ECONOMY).toFixed(0);
            $scope.tours[i].businessPrice = ($scope.tours[i].Price * Constants.PRICE_COEF.BUSSINESS).toFixed(0);
            for (var j = 0; j < $scope.tours[i].PossibleDays.length; j++)
            {
                $scope.tours[i].PossibleDays[j] = utils.translateDays($scope.tours[i].PossibleDays[j]);
            }
        }
    }


});