angular.module('extraAir').service('changeUserInfoService', function (sha256, $http, $timeout) {

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
            ChangePass.NewPass = sha256.convertToSHA256(ChangePass.NewPass);
            ChangePass.OldPass = sha256.convertToSHA256(ChangePass.OldPass);
            $http.put(Constants.REST_URL + "api/users/changepass/" + id, ChangePass)
                .success(function (data, status, headers, config) {

                    notifyManager(scope, true, false, "Success", 4000);
                    ChangePass.NewPass = null;
                    ChangePass.OldPass = null;
                    //  alert("You have jast change your setting!");
                }).error(function (data, status) {

                ChangePass.NewPass = null;
                ChangePass.OldPass = null;

                notifyManager(scope, false, true, data.Message, 4000);
            });
        }
    };


    function isObject(val) {
        if (val === null) { return false;}
        return ( (typeof val === 'function') || (typeof val === 'object') );
    }


    this.ChangeUser = function (prewUser, user, id, scope) {
        for (var prop in user) {
            if (isObject(user[prop])){
                for (var propInner in user[prop]) {
                    if (propInner[user[prop]] === prewUser[user[prop]]){
                        delete propInner[user[prop]];
                    }
                }
            }
            else if (user[prop] === prewUser[prop]){
                delete user[prop];
            }
        }


        $http.put(Constants.REST_URL + "api/users/" + id, user).then(function (data, status, headers, config) {


            prewUser.FirstName = user.FirstName;
            prewUser.LastName = user.LastName;
            prewUser.Birthday = user.Birthday;
            prewUser.Phone = user.Phone;
            prewUser.Address = user.Address;

            notifyManager(scope, true, false, "Success", 4000);
        }, function (data) {
            notifyManager(scope, false, true, data.Message, 4000);

            scope.user = prewUser;
            scope.message = data.Message;
        });
    };


});
