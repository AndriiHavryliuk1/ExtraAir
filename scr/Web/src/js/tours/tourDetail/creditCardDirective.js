var app = angular.module('extraAir');
app.directive("creditCard", function() {
    return {
        restrict: 'E',
        templateUrl: 'js/tours/tourDetail/creditCardTemplate.html'
    }
});
