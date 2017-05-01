angular.module('extraAir').controller('commonController', function($rootScope, $scope){
    $rootScope.isAuthorized = !!localStorage.getItem('token');

    $scope.isAutorizedLocal = !!localStorage.getItem('token');
    $scope.logout = function(){
        localStorage.removeItem('token');
        $rootScope.isAuthorized = false;
        $scope.isAutorizedLocal = !!localStorage.getItem('token');
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };


    $rootScope.$on('headerDirective', function(data){
        $scope.isAutorizedLocal = !!localStorage.getItem('token');
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    });

});