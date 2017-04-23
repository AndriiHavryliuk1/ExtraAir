var app = angular.module('extraAir');

app.service('crossingService', function($rootScope) {
    // private variable
    var tour = undefined;
    var tourDetail = undefined;

    return {
        getTour: function() {
            return tour;
        },
        setTour: function(value) {
            tour = value;
        },
        getTourDetail: function(){
            return tourDetail;
        },
        setTourDetail: function(value){
            tourDetail = value;
        }
    };
});