var app = angular.module('extraAir');
app.controller('adminCabinetController', function($scope, $rootScope, $location, $filter, airportsService,  $http,changeUserInfoService, fileUploadService, getService, jwtHelper) {


    var currentUser = jwtHelper.decodeToken(localStorage.getItem('token'));

    $scope.tourStatusParams = {
        tourId: null,
        dateStart: null,
        dateFinish: null,
        airportFromId: null,
        airportToId: null
    };

    $scope.constantTourStatuses = Constants.TOUR_STATUSES;

    var URL_forAdmin = "api/Users/" + currentUser.id;
    getService.GetObjects(URL_forAdmin).then(function(data) {
        $scope.admin = data.data;
        $scope.admin.ImagePath = Constants.REST_URL + $scope.admin.ImagePath;
        $scope.admin.BirthdayForView = $filter('date')($scope.admin.Birthday, 'dd-MM-yyyy');
        $scope.admin.Birthday = new Date($scope.admin.Birthday);
        $scope.bufferUser = angular.copy($scope.admin);
    }, function(){

    });

    airportsService.getAirports().then(function(data){
        $scope.airports = data;
        $scope.airports = forEditing($scope.airports);
    });



    $scope.selectView = function(view){
        $rootScope.selectedView = view;
    };

    $scope.ChangePassword = function() {
        changeUserInfoService.ChangePassword($scope.ChangePass, jwtHelper.decodeToken(localStorage.getItem('token')).id,$scope);
    };

    $scope.ChangeUser = function () {
        //upload photo
        if ($scope.admin != "undefined" && !!$scope.picture) {
            fileUploadService.uploadFileToUrl($scope.picture, Constants.REST_URL + "api/image/" +
                jwtHelper.decodeToken(localStorage.getItem('token')).id).then(function (response) {
                if (response !== undefined) {
                    $scope.admin.ImagePath = response.data;
                }
            });
        }

        $scope.notifyError = false;
        $scope.notifySuccess = false;
        changeUserInfoService.ChangeUser($scope.admin, $scope.bufferUser, jwtHelper.decodeToken(localStorage.getItem('token')).id, $scope);
    };

    $scope.editAirport = function(airport) {
        airport.isEditing = !airport.isEditing;
        var editableAirport = angular.copy(airport);
         delete editableAirport.isEditing;

        if (!airport.isEditing){
            airportsService.putAirport(editableAirport).then(function(data){
                console.log(data);
            }, function(){
                console.log("error");
            })
        }
    };

    $scope.cancel = function(airport){
        airport.isEditing = !airport.isEditing;
    };


    $scope.search = function(){

        $scope.tourStatusParams.dateStart = $scope.tourStatusParams.dateStart !== null ?
            $filter('date')($scope.tourStatusParams.dateStart, 'MM/dd/yyyy HH:mm') : null;
        $scope.tourStatusParams.dateFinish = $scope.tourStatusParams.dateFinish !== null ?
            $filter('date')($scope.tourStatusParams.dateFinish, 'MM/dd/yyyy HH:mm') : null;
        var URL = "api/TourStatus?tourId=" + $scope.tourStatusParams.tourId + "&dateStart=" + $scope.tourStatusParams.dateStart +
            "&dateFinish=" + $scope.tourStatusParams.dateFinish + "&airportFromId=" + $scope.tourStatusParams.airportFromId +
            "&airportToId=" + $scope.tourStatusParams.airportToId;

        getService.GetObjects(URL).then(function (data) {
            $scope.tourStatuses = data.data;
            $scope.tourStatuses = correctTourStatusData($scope.tourStatuses);

            $scope.tourStatuses = forEditing($scope.tourStatuses);

            //  paginationService.ChangeURL($scope.loadList, $scope.tours, $rootScope.preArray, '/toursList', $rootScope.pagingInfo);
            $scope.isLoading = false

        }, function (error) { }).finally(function(){
            $scope.isLoading = false;
        });
    };


    $scope.editTourStatus = function(tourStatus) {
        tourStatus.isEditing = !tourStatus.isEditing;
        if (!tourStatus.isEditing) {
            var postedTS = angular.copy(tourStatus);
            delete postedTS.isEditing;
            delete postedTS.dateStartView;
            delete postedTS.dateFinishView;
            delete postedTS.statusValue;

            postedTS.DateStart = $filter('date')(postedTS.DateStart, 'yyyy-MM-dd') + "T" + $filter('date')(postedTS.DateStart, 'HH:mm');
            postedTS.DateFinish = $filter('date')(postedTS.DateFinish, 'yyyy-MM-dd') + "T" + $filter('date')(postedTS.DateFinish, 'HH:mm');
            $http.post(Constants.REST_URL + "api/TourStatus", postedTS, {
                headers: {
                    'Content-type': 'application/json'
                }
            }).then(function (data) {
                updateTourStatuses(data.data);
                console.log("fine");
            }, function () {
                console.log("error")
            });
        }


    };

    function updateTourStatuses(tourStatus){
        for (var i=0; i < $scope.tourStatuses.length; i++){
            if ($scope.tourStatuses[i].TourId === tourStatus.TourId){
                $scope.tourStatuses[i].DateFinish = tourStatus.DateFinish;
                $scope.tourStatuses[i].DateStart = tourStatus.DateStart;
                $scope.tourStatuses[i].TourStatusId = tourStatus.TourStatusId;
                $scope.tourStatuses[i].TourStatusType = tourStatus.TourStatusType;
                $scope.tourStatuses[i].dateStartView = $filter('date')(tourStatus.DateStart, 'dd-MM-yyyy HH:mm');
                $scope.tourStatuses[i].dateFinishView = $filter('date')(tourStatus.DateFinish, 'dd-MM-yyyy HH:mm');
                for (var j = 0; j < Constants.TOUR_STATUSES.length; j++) {
                    if (Constants.TOUR_STATUSES[j].KEY === $scope.tourStatuses[i].TourStatusType) {
                        $scope.tourStatuses[i].statusValue = Constants.TOUR_STATUSES[j].VALUE;
                        break;
                    }
                }
            }
        }
    }


    function forEditing(data){
        data.forEach(function(d){
            d.isEditing = false;
        });
        return data;
    }

    function correctTourStatusData(data) {
        data.forEach(function(status){
            for (var i = 0; i < Constants.TOUR_STATUSES.length; i++) {
                if (Constants.TOUR_STATUSES[i].KEY === status.TourStatusType) {
                    status.statusValue = Constants.TOUR_STATUSES[i].VALUE;
                    break;
                }
            }
            status.dateStartView = $filter('date')(status.DateStart, 'dd-MM-yyyy HH:mm');
            status.dateFinishView = $filter('date')(status.DateFinish, 'dd-MM-yyyy HH:mm');
        });
        return data;
    }




});
