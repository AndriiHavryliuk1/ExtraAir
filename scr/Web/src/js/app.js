var app = angular.module('extraAir');

app.config(function($routeProvider, $locationProvider) {

    var resolveTours = {
        "toursData": ["toursService", function (toursService) {
            return toursService.getTours();
        }]
    };
    $locationProvider.hashPrefix('');
    $routeProvider.when('/', {
        templateUrl: 'js/main/main.html',
        controller: 'mainController'
    });

    $routeProvider.when('/tours', {
        templateUrl: 'js/tours/toursTemplate.html',
        controller: 'toursController',
        reloadOnSearch: false
    });

    $routeProvider.when('/registration', {
        templateUrl: 'js/registration/registrationTemplate.html',
        controller: 'registrationController',
        reloadOnSearch: false
    });

    $routeProvider.when('/login', {
        templateUrl: 'js/login/loginTemplate.html',
        controller: 'loginController',
        reloadOnSearch: false
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