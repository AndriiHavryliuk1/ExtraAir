angular.module('extraAir').service('fileUploadService', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);
        var imgurl = $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: {'Content-Type': undefined}
        }).then(function (data) {
            return data;
        }, function () {
        });
        return imgurl;
    }
}]);
