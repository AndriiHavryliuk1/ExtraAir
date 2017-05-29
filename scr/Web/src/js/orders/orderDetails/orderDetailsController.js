'use strict';

var app = angular.module('extraAir');
app.controller('orderDetailsController', function ($rootScope, $scope, $location, $routeParams, $window, $filter, getService,
                                                   paginationArrayService, paginationService, airportsService, jwtHelper) {


    var utils = new CommonUtils();
    $scope.loadList = function () {
        $scope.isLoading = true;
        var URL = "api/orders/" + $routeParams.id;
        getService.GetObjects(URL).then(function (data) {
            $scope.order = data.data;
            correctData();

            $scope.isLoading = false;

        }, function (error) {
        }).finally(function () {
            $scope.isLoading = false;
        });

    };

    $scope.loadList();


    function correctData() {
        $scope.order.timeStart = $filter('date')($scope.order.order.DateStart, 'HH:mm');
        $scope.order.timeFinish = $filter('date')($scope.order.order.DateFinish, 'HH:mm');
        $scope.order.dayStart = utils.translateDays(Constants.DAYS[new Date($scope.order.order.DateStart).getDay()]);
        $scope.order.dayFinish = utils.translateDays(Constants.DAYS[new Date($scope.order.order.DateFinish).getDay()]);
        $scope.order.dateStart = $filter('date')($scope.order.order.DateStart, 'dd-MM-yyyy');
        $scope.order.dateFinish = $filter('date')($scope.order.order.DateFinish, 'dd-MM-yyyy');
        $scope.order.paidValue = $scope.order.order.Paid ? "Оплачено" : "Зарезервовано";
        $scope.order.Passengers.forEach(function (pas) {
            pas.GenderValue = pas.Gender === 0 ? "Чоловіча" : "Жіноча";
            pas.BaggageInternalValue = pas.BaggageInternal ? "Включено" : "Немає";
            pas.BaggageeExternalValue = pas.BaggageeExternal ? "Включено" : "Немає";
        });
    }

});