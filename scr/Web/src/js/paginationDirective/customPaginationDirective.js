var app = angular.module('extraAir');
app.directive("custompagination", function () {
    return {
        restrict: 'A',
        templateUrl: 'js/paginationDirective/paginationTemplate.html',
        link: function ($scope, $rootScope) {

            $scope.$watch('paginationScope', function (data) {
                if (data !== undefined) {
                    $scope.paging = data.data;
                    $scope.paginArray = data.list;
                }

                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            });
        }

    }
});