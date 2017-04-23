var app = angular.module('extraAir');

app.controller('registrationController', function(sha256, $scope, $http, $window, $rootScope) {

    $scope.loading = false;

    $scope.submit = function() {
        $scope.loading = true;
        $scope.client.Deleted = false;
        $scope.client.Password = sha256.convertToSHA256($scope.client.Password);
        $scope.client.Phone = "";
        $scope.client.UserClaimId = 2;
        $scope.client.Birthday = new Date();
        $http.post(Constants.REST_URL + "api/clients", $scope.client, {
            headers: {
                'Content-type': 'application/json'
            }
        })
            .success(function(data, status, headers, config) {
                var user = data;

                $http.post(URL_FOR_REST.url + "api/Patients/" + data.UserId + "/confirmRegistration", user, {
                    headers: {
                        'Content-type': 'application/json'
                    }
                })
                    .success(function(data, status, headers, config) {
                        $scope.loading = false;
                    })
                    .error(function(data, status, headers, config) {
                        $scope.loading = false;
                    });
                $window.location.href = "#/confirmRegistration";
            })
            .error(function(data, status, headers, config) {
                localStorage.removeItem('token');
                $rootScope.isAuthorized = false;
                $scope.failed = true;
                $scope.loading = false;
            });
    };


});
