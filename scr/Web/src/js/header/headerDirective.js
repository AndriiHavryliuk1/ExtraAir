var app = angular.module('extraAir');
app.directive("customheader", function($rootScope) {
    return {
        restrict: 'E',
        templateUrl: 'js/header/headerTemplate.html',
        link: function($scope, $element, iAttributes, ctrl){

            $rootScope.$on('headerDirective', function(event, data) {

                console.log("apdated!");
                if ($rootScope.isAutorized){
                    var elem = document.getElementById("forUnautorized");
                    elem.nextElementSibling.remove();
                    $element.append('<ul class="nav navbar-nav navbar-right" id="forAutorized" ng-if="isAutorized"><li><a href="#/">Registration</a></li> <li><a href="/" ng-click="logout()">Logout</a></li></ul>');
                }
                else {
                    var elem = document.getElementById("forAutorized");
                    elem.nextElementSibling.remove();
                    $element.append('<ul class="nav navbar-nav navbar-right" id="forUnautorized" ng-if="!isAutorized"> <li><a href="#/registration"><span>Registration</span></a> </li><li><a href="#/login"><span class="glyphicon glyphicon-user" aria-hidden="true"></span><span>Login</span></a> </li></ul>');
                }
            });
        }
    }
});
