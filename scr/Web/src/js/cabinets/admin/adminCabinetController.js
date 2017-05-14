var app = angular.module('extraAir');
app.controller('adminCabinetController', function($scope, $rootScope, $location, $filter, airportsService,  $http,changeUserInfoService, fileUploadService, getService, jwtHelper) {


    var currentUser = jwtHelper.decodeToken(localStorage.getItem('token'));

    var URL_forAdmin = "api/Users/" + currentUser.id;
    getService.GetObjects(URL_forAdmin).then(function(data) {
        $scope.admin = data.data;
        $scope.admin.ImagePath = Constants.REST_URL + $scope.admin.ImagePath;
        $scope.admin.BirthdayForView = $filter('date')($scope.admin.Birthday, 'dd-MM-yyyy');
        $scope.admin.Birthday = new Date($scope.admin.Birthday);
        $scope.bufferUser = angular.copy($scope.admin);
    }, function(){

    });

    airportsService.getAirports().then(function(data){
        $scope.airports = data;
        $scope.airports = forEditing($scope.airports);
    });



    $scope.selectView = function(view){
        $rootScope.selectedView = view;
    };

    $scope.ChangePassword = function() {
        changeUserInfoService.ChangePassword($scope.ChangePass, jwtHelper.decodeToken(localStorage.getItem('token')).id,$scope);
    };

    $scope.ChangeUser = function () {
        //upload photo
        if ($scope.admin != "undefined" && !!$scope.picture) {
            fileUploadService.uploadFileToUrl($scope.picture, Constants.REST_URL + "api/image/" +
                jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (response) {
                if (response !== undefined) {
                    $scope.admin.ImagePath = response.data;
                }
            });
        }

        $scope.notifyError = false;
        $scope.notifySuccess = false;
        changeUserInfoService.ChangeUser($scope.admin, $scope.bufferUser, jwtHelper.decodeToken(localStorage.getItem('token')).id, $scope);
    };

    $scope.editAirport = function(airport) {
        airport.isEditing = !airport.isEditing;
        var editableAirport = angular.copy(airport);
         delete editableAirport.isEditing;

        if (!airport.isEditing){
            airportsService.putAirport(editableAirport).then(function(data){
                console.log(data);
            }, function(){
                console.log("error");
            })
        }
    };

    $scope.cancel = function(airport){
        airport.isEditing = !airport.isEditing;
    };


    function forEditing(data){
        data.forEach(function(d){
            d.isEditing = false;
        });
        return data;
    }




});
