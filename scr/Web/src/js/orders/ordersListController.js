'use strict';

var app = angular.module('extraAir');
app.controller('ordersListController', function ($rootScope, $scope, $location, $window, $filter, getService,
                                                 paginationArrayService, paginationService, airportsService, jwtHelper) {


    initPaginationParams();

    var utils = new CommonUtils();
    $scope.loadList = function(){
        $scope.isLoading = true;
        var URL = "api/orders?page=" + $rootScope.pagingInfo.page + "&itemsPerPage=" + $rootScope.pagingInfo.itemsPerPage +
            "&search=" + $rootScope.pagingInfo.search + "&type=" + $rootScope.pagingInfo.type + "&userId=" + jwtHelper.decodeToken(localStorage.getItem('token')).id;

        getService.GetObjects(URL).then(function (data) {
            $scope.orders = data.data.list;

            correctData();
            $scope.AnyElementOfList = data.data.count == 0;
            $rootScope.pagingInfo.totalItems = data.data.count;
            $scope.pages = Math.ceil(data.data.count / $rootScope.pagingInfo.itemsPerPage);
            $scope.paginArray = paginationArrayService.Array($scope.pages, $rootScope.pagingInfo.page);


            $scope.paginationScope = {
                data: !!$rootScope.pagingInfo ? {} : $rootScope.pagingInfo,
                list: $scope.paginArray
            };

            if (!$scope.$$phase) {
                $scope.$apply();
            }

            $scope.isLoading = false;

        }, function (error) { }).finally(function(){
            $scope.isLoading = false;
        });

        $location.search('search', !!$rootScope.pagingInfo.search ? $rootScope.pagingInfo.search : null);
        $location.search('page', $rootScope.pagingInfo.page);
        $location.search('type', $rootScope.pagingInfo.type);

        $rootScope.preArray = $scope.orders;
    };

    $scope.loadList();

    $scope.goToDetailPage = function(res) {
        $location.search({});
        $location.path("orders/" + res.OrderId);
    };


    function initPaginationParams() {
        $rootScope.pagingInfo = {
            page: $location.search().page !== undefined ? $location.search().page : 1,
            itemsPerPage: 5,
            search: $location.search().search !== undefined ? $location.search().search : '',
            type: $location.search().type !== undefined ? $location.search().type : 'All'
        };
    }

    function correctData() {
        $scope.orders.forEach(function(order){
            order.timeStart = $filter('date')(order.DateStart, 'HH:mm');
            order.timeFinish = $filter('date')(order.DateFinish, 'HH:mm');
            order.dayStart = utils.translateDays(Constants.DAYS[new Date(order.DateStart).getDay()]);
            order.dayFinish = utils.translateDays(Constants.DAYS[new Date(order.DateFinish).getDay()]);
            order.dateStart = $filter('date')(order.DateStart, 'dd-MM-yyyy');
            order.dateFinish = $filter('date')(order.DateFinish, 'dd-MM-yyyy');
        });
    }

});