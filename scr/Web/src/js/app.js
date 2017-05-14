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

    $routeProvider.when('/tours/:id', {
        templateUrl: 'js/tours/tourDetail/tourDetailTemplate.html',
        controller: 'tourDetailController',
        reloadOnSearch: false
    });

    $routeProvider.when('/userCabinet', {
        templateUrl: 'js/cabinets/client/clientCabinetTemplate.html',
        controller: 'clientCabinetController',
        reloadOnSearch: false
    });

    $routeProvider.when('/toursList', {
        templateUrl: 'js/toursList/toursListTemplate.html',
        controller: 'toursListController',
        reloadOnSearch: false
    });

    $routeProvider.when('/ordersList', {
        templateUrl: 'js/orders/ordersListTemplate.html',
        controller: 'ordersListController',
        reloadOnSearch: false
    });

    $routeProvider.when('/orders/:id', {
        templateUrl: 'js/orders/orderDetails/orderDetailsTemplate.html',
        controller: 'orderDetailsController',
        reloadOnSearch: false
    });

    $routeProvider.when('/tourStatuses', {
        templateUrl: 'js/tours/tourStatus/tourStatusTemplate.html',
        controller: 'tourStatusController',
        reloadOnSearch: false
    });


    $routeProvider.when('/adminCabinet', {
        templateUrl: 'js/cabinets/admin/adminCabinetTemplate.html',
        controller: 'adminCabinetController',
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