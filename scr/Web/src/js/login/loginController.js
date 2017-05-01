var app = angular.module('extraAir');

app.controller('loginController', function($rootScope, $interval, $scope, sha256, $route, $http, $window, jwtHelper) {

    $scope.loading = false;

    $scope.submit = function() {
        $scope.failed = false;
        $scope.user.Password = sha256.convertToSHA256($scope.Password);
        var body = "grant_type=password&username=" + $scope.user.Email + "&password=" + $scope.user.Password + "&client=desktop";

        $http.post(Constants.REST_URL + "Token", body, {
                headers: {
                    'Content-Type': 'x-www-form-urlencoded'
                }
            })
            .then(function(data, status, headers, config) {
                if (data.data.access_token != undefined) {
                    localStorage.setItem('token', data.data.access_token);
                    $rootScope.isAuthorized = true;
                    var x = jwtHelper.decodeToken(data.data.access_token);

                    switch (x.role) {
                        case Constants.ROLES.ADMIN: {
                            $rootScope.isAdmin = true;
                            $window.location.href = "#/administratorCabinet";
                            break;
                        }
                        case Constants.ROLES.CLIENT:
                            $rootScope.isClient = true;
                            $window.location.href = "#/userCabinet";
                            break;
                        }
                    }
                    $rootScope.$emit('headerDirective', $rootScope.isAuthorized);
            } ,function(data, status, headers, config) {
                localStorage.removeItem('token');
                $rootScope.isAuthorized = false;
                $scope.failed = true;
            });
    };


    $scope.setCheck = function() {
        $scope.loading = true;
    };

    $scope.recoveryPass = function() {
        $scope.failed = false;
        $scope.loading = true;
        $http.post(URL_FOR_REST.url + "api/users/recovery?email=" + $scope.user.Email, {
            headers: {
                'Content-type': 'application/json'
            }
        })
            .success(function(data, status, headers, config) {
                $scope.loading = false;
                alert("Your password has beed changed!");

                $window.location.href = "#/signIn";

            })
            .error(function(data, status, headers, config) {
                $scope.loading = false;
                $scope.failed = true;
                //  alert("Incorrect email or you don`t register!");
                $window.location.href = "#/recoveryPassword";

            })


    }
});
