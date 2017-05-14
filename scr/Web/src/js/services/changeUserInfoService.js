angular.module('extraAir').service('changeUserInfoService', function (sha256, $http, $timeout, $filter) {

    var notifyManager = function (scope, successState, errorState, message, timeout) {
        scope.notifySuccess = successState;
        scope.notifyError = errorState;
        scope.message = message;

        if (successState == true) {
            $timeout(function () {
                scope.notifySuccess = false;
            }, timeout);
        }
        if (errorState == true) {
            $timeout(function () {
                scope.notifyError = false;
            }, timeout);
        }
    };

    this.ChangePassword = function (ChangePass, id, scope) {
        if (ChangePass.NewPass != null && ChangePass.OldPass != null) {
            var newChanger = {
                NewPass:sha256.convertToSHA256(ChangePass.NewPass),
                OldPass: sha256.convertToSHA256(ChangePass.OldPass)
            };
            $http.put(Constants.REST_URL + "api/users/changepass/" + id, newChanger).then(function(data, status, headers, config){
                notifyManager(scope, true, false, "Success", 4000);
                ChangePass.NewPass = null;
                ChangePass.OldPass = null;
            }, function(data, status){
                ChangePass.NewPass = null;
                ChangePass.OldPass = null;

                notifyManager(scope, false, true, data.Message, 4000);
            });

        }
    };

    this.ChangeUser = function (prewUser, user, id, scope) {
        user.Birthday = $filter('date')(user.Birthday, 'yyyy-MM-dd');
        $http.put(Constants.REST_URL + "api/users/" + id, user).then(function (data, status, headers, config) {
            prewUser.FirstName = user.FirstName;
            prewUser.LastName = user.LastName;
            prewUser.BirthdayForView = $filter('date')(user.Birthday, 'dd-MM-yyyy');
            prewUser.Birthday = new Date(user.Birthday);
            prewUser.Phone = user.Phone;
            prewUser.Address = user.Address;
            prewUser.IdCard = user.IdCard;

            notifyManager(scope, true, false, "Success", 4000);
        }, function (data) {
            notifyManager(scope, false, true, data.Message, 4000);

            scope.user = prewUser;
            scope.message = data.Message;
        });
    };


});
