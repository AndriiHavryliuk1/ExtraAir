'use strict';

var app = angular.module('extraAir');
app.controller('clientCabinetController', function ($rootScope, $scope, $location, $filter, getService, airportsService,
                                                    tourDetailsService, toursService, crossingService, clientService, jwtHelper,
                                                    fileUploadService, changeUserInfoService) {
    $scope.picture = null;

    $scope.ChangePass = {
        OldPass: null,
        NewPass: null
    };

    clientService.getClient(jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (data) {
        $scope.client = data;
        $scope.client.ImagePath = Constants.REST_URL + $scope.client.ImagePath;
        $scope.client.BirthdayForView = $filter('date')($scope.client.Birthday, 'dd-MM-yyyy');
        $scope.client.Birthday = new Date($scope.client.Birthday);
        $scope.bufferUser = angular.copy($scope.client);
    });

    $scope.ChangePassword = function() {
        changeUserInfoService.ChangePassword($scope.ChangePass, jwtHelper.decodeToken(localStorage.getItem('token')).id,$scope);
    };

    $scope.ChangeUser = function () {
        //upload photo
        if ($scope.client != "undefined" && $scope.picture !== null) {
            fileUploadService.uploadFileToUrl($scope.picture, Constants.REST_URL + "api/image/" +
                jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (response) {
                if (response !== undefined) {
                    $scope.client.ImagePath = response.data;
                }
            });
        }

        $scope.notifyError = false;
        $scope.notifySuccess = false;
        changeUserInfoService.ChangeUser($scope.client, $scope.bufferUser, jwtHelper.decodeToken(localStorage.getItem('token')).id, $scope);
    }


});