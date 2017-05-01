var app = angular.module('extraAir');
app.directive("choosePlace", function() {
    return {
        restrict: 'E',
        templateUrl: 'js/tours/tourDetail/choosePlaceTemplate.html'
    }
});
