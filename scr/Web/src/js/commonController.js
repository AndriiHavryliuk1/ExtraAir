angular.module('extraAir').controller('commonController', function($rootScope, $scope){
    $rootScope.isAutorized = !!localStorage.getItem('token');

    $scope.logout = function(){
        localStorage.removeItem('token');
        $rootScope.isAuthorized = false;
        $rootScope.$emit('headerDirective', true);
    };

});