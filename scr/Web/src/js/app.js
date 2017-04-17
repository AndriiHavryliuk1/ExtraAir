var app = angular.module('extraAir');

app.config(function($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'js/main/main.html',
        controller: 'mainController'
    });

    $routeProvider.when('/main', {
        templateUrl: 'js/main/main.html',
        controller: ''
    });

    $routeProvider.when('/error', {
        templateUrl: 'src/views/ErrorPage.html',
        controller: ''
    });

    $routeProvider.when('/error/:id', {
        templateUrl: 'src/views/ErrorPage.html',
        controller: ''
    });

    $routeProvider.when('/departments', {
        templateUrl: 'src/views/ListOfDepartments.html',
        controller: 'ListOfDepartmentsController'
    });

    $routeProvider.when('/registrationForAppointment', {
        templateUrl: 'src/views/RegistrationForAppointment.html',
        controller: ''
    });

    $routeProvider.when('/doctors', {
        templateUrl: 'src/views/ListOfDoctors.html',
        controller: 'ListOfDoctorsController',
        reloadOnSearch: false
    });

    $routeProvider.when('/confirmRegistration', {
        templateUrl: 'src/views/ConfirmRegistration.html',
        controller: 'ConfirmRegistrationController'
    });

    $routeProvider.when('/confirmRegistration/:id', {
        templateUrl: 'src/views/ConfirmRegistration.html',
        controller: 'ConfirmRegistrationController'
    });

    $routeProvider.when('/signUp', {
        templateUrl: 'src/views/Registration.html',
        controller: ''
    });

    $routeProvider.when('/signIn', {
        templateUrl: 'src/views/SignIn.html',
        controller: ''
    });

    $routeProvider.when('/patientCabinet', {
        templateUrl: 'src/views/PatientPersonalCabinet.html',
        controller: 'patientCabinet'
    });

    $routeProvider.when('/appointmentInfo/:id', {
        template: '<div appointment-info></div>',
        controller: ''
    });

    $routeProvider.when('/patients/:id', {
        templateUrl: 'src/views/PatientInfo.html',
        controller: 'PatientInfoController',
        reloadOnSearch: false
    });

    $routeProvider.when('/medicalHistory/:id', {
        templateUrl: 'src/views/MedicalHistory.html',
        controller: 'MedicalHistoryController',
        reloadOnSearch: false
    });

    $routeProvider.when('/recoveryPassword', {
        templateUrl: 'src/views/RecoveryPassword.html',
        controller: ''
    });


    //  "medicalhistory/" + $routeParams["id"] + "/page/" + $scope.searchInfo.page


    $routeProvider.otherwise({
        redirectTo: '/error/404'
    });
});

app.config(function Config($httpProvider, jwtInterceptorProvider) {
    jwtInterceptorProvider.tokenGetter = function(jwtHelper, $http) {
        return localStorage.getItem('token');
    }
    $httpProvider.interceptors.push('jwtInterceptor');
});