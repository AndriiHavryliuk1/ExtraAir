var app = angular.module('extraAir');
app.directive("settings", function() {
    return {
        restrict: 'E',
        templateUrl: 'js/templates/userSettingsTemplate.html'
    }
});
