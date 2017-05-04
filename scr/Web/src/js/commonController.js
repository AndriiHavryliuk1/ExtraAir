angular.module('extraAir').controller('commonController', function ($rootScope, $scope, jwtHelper) {
    $rootScope.isAuthorized = !!localStorage.getItem('token');

    $scope.isAutorizedLocal = !!localStorage.getItem('token');
    setCabinetURL();
    $scope.logout = function () {
        localStorage.removeItem('token');
        $rootScope.isAuthorized = false;
        $scope.isAutorizedLocal = !!localStorage.getItem('token');
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };


    $rootScope.$on('headerDirective', function (data) {
        $scope.isAutorizedLocal = !!localStorage.getItem('token');

        setCabinetURL

        if (!$scope.$$phase) {
            $scope.$apply();
        }
    });

    function setCabinetURL(){
        switch (jwtHelper.decodeToken(localStorage.getItem('token')).role) {
            case Constants.ROLES.ADMIN: {
                $rootScope.isAdmin = true;
                $scope.cabinetUrl = '#/adminCabinet';
                break;
            }
            case Constants.ROLES.CLIENT:
                $rootScope.isClient = true;
                $scope.cabinetUrl = '#/userCabinet';
                break;
        }
    }

});