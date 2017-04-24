var app = angular.module('extraAir');
app.directive("customheader", function($rootScope) {
    return {
        restrict: 'E',
        templateUrl: 'js/header/headerTemplate.html'
    }
});
