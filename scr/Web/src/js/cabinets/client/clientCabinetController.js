'use strict';

var app = angular.module('extraAir');
app.controller('clientCabinetController', function ($rootScope, $scope, $location, $filter, getService, airportsService,
                                                    tourDetailsService, toursService, crossingService, clientService, jwtHelper,
                                                    fileUploadService, changeUserInfoService) {

    $scope.picture = null;

    clientService.getClient(jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (data) {
        $scope.client = data;
        $scope.client.ImagePath = Constants.REST_URL +  $scope.client.ImagePath;
        $scope.bufferUser = angular.copy($scope.client);
    });


    $scope.ChangeUser = function () {
        //upload photo
        if ($scope.client != "undefined") {
            fileUploadService.uploadFileToUrl($scope.picture, Constants.REST_URL + "api/image/" +
                jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (response) {
                $scope.client.ImagePath = response.data;
            });
        }

        $scope.notifyError = false;
        $scope.notifySuccess = false;
        changeUserInfoService.ChangeUser($scope.client, $scope.bufferUser, jwtHelper.decodeToken(localStorage.getItem('token')).id, $scope);
    }


});