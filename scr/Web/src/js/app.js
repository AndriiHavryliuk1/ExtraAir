var app = angular.module('extraAir');

app.config(function($routeProvider) {

    var resolveTours = {
        "toursData": ["toursService", function (toursService) {
            return toursService.getTours();
        }]
    };

    $routeProvider.when('/', {
        templateUrl: 'js/main/main.html',
        controller: 'mainController'
    });

    $routeProvider.when('/main', {
        templateUrl: 'js/tours/toursTemplate.html',
        controller: 'toursController'
    });




    //  "medicalhistory/" + $routeParams["id"] + "/page/" + $scope.searchInfo.page


    $routeProvider.otherwise({
        redirectTo: '/'
    });
});

app.config(function Config($httpProvider, jwtInterceptorProvider) {
    jwtInterceptorProvider.tokenGetter = function(jwtHelper, $http) {
        return localStorage.getItem('token');
    };
    $httpProvider.interceptors.push('jwtInterceptor');
});