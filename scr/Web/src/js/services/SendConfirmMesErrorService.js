angular.module('extraAir').service('SendConfirmMesErrorService', function ($http, $window) {
    this.SendMailError = function (UserId) {

        $http.get(Constants.REST_URL + "api/clients/forRegistrationConfirm/" + UserId)
            .then(function (data, status, headers, config) {
                    alert("We have some trouble with our server please try again!");
                    $window.location.href = "#/login";
                    $http.delete(Constants.REST_URL + "api/clients/" + UserId, data).then(function () {
                });
            });
    }
});
