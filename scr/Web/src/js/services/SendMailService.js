angular.module('extraAir').service('sendMailService', ['$http', function ($http) {
    this.sendMail = function (str) {
        var html = {
            HTML: str
        };
        $http.post(Constants.REST_URL + "api/notification", html, {
            headers: {
                'Content-type': 'application/json'
            }
        }).then(function () {

        }, function () {

        });
    }
}]);
