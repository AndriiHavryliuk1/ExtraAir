var app = angular.module('extraAir');

app.controller('registrationController', function(sha256, $scope, $http, $window, $rootScope, SendConfirmMesErrorService) {
    $scope.loading = false;

    $scope.submit = function() {
        $scope.loading = true;
        $scope.client.Deleted = false;
        $scope.client.IsActive = false;
        $scope.client.Password = sha256.convertToSHA256($scope.client.Password);
        $scope.client.Phone = "";
        $scope.client.UserClaimId = 2;
        $scope.client.ImagePath = 'content/images/profileAvatar.jpg';
        $http.post(Constants.REST_URL + "api/clients", $scope.client).then(function(data) {

            $http.post(Constants.REST_URL + "api/clients/" + data.data.UserId + "/confirmRegistration").then(function(){
                $scope.loading = false;
                $window.location.href = "#/confirmRegistration";
            }, function(){
                SendConfirmMesErrorService.SendMailError(data.data.UserId);
                $scope.loading = false;
            });
        }, function() {
            $scope.failed = true;
            $scope.loading = false;
        });
    };
});