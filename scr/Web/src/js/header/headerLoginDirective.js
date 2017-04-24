var app = angular.module('extraAir');
app.directive("navbarlogin", function($rootScope) {
    return {
        restrict: 'E',
        templateURL: 'js/header/headerLoginTemplate.html'
       // controller: 'headerController'
    }
});