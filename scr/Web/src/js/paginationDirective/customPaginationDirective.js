var app = angular.module('extraAir');
app.directive("custompagination", function() {
    return {
        restrict: 'A',
        templateUrl: 'js/paginationDirective/paginationTemplate.html'

    }
});