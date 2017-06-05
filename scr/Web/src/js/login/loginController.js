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
                            $window.location.href = "#/adminCabinet";
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
        $http.post(Constants.REST_URL + "api/users/recovery?email=" + $scope.user.Email, {
            headers: {
                'Content-type': 'application/json'
            }
        }).then(function() {
            $scope.loading = false;
            alert("Ваш пароль було змінено, перевірте свій email!");
            $window.location.href = "#/signIn";

        }, function() {
            $scope.loading = false;
            $scope.failed = true;
            //  alert("Incorrect email or you don`t register!");
            $window.location.href = "#/recoveryPassword";

        })
    }
});
