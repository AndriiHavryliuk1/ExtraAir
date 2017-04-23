var app = angular.module('extraAir');

app.controller('loginController', function($rootScope, $scope) {

    $scope.loading = false;

    $scope.logout = function(){
        localStorage.removeItem('token');
        $rootScope.isAuthorized = false;
    };


});
