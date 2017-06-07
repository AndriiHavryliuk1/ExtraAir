'use strict';

var app = angular.module('extraAir');
app.controller('tourDetailController', function ($rootScope, $scope, $location, $filter, $http, $routeParams, $timeout, getService, airportsService, jwtHelper,
                                                 tourDetailsService, toursService, crossingService, sendMailService) {
    $scope.tourSearchInfo = crossingService.getTour() !== undefined ? crossingService.getTour() : getSearchInfoURL();
    setupURL();
    $scope.activePlacesList = [];
    $scope.ordering = false;
    $scope.showCheckStep = false;
    $scope.methodRegistration = {};

    var utils = new CommonUtils();
    toursService.getTour($routeParams.id).then(function (data) {
        $scope.tour = data;
        var coef = $scope.tourSearchInfo.tourClass === 'Economy' ? Constants.PRICE_COEF.ECONOMY : Constants.PRICE_COEF.BUSSINESS;
        correctData(coef);
    });


    $scope.setActivePlaces = function (coordinate) {
        $scope.allPlaces.forEach(function (place) {
            place.forEach(function (point) {
                if (coordinate.x === point.coordinate.x && coordinate.y === point.coordinate.y) {
                    if (point.active) {
                        point.active = false;
                        for (var i = 0; i < $scope.activePlacesList.length; i++) {
                            if ($scope.activePlacesList[i].coordinate.x === coordinate.x
                                && $scope.activePlacesList[i].coordinate.y === coordinate.y) {

                                for (var j = 0; j < $scope.allPassengers.length; j++) {
                                    if ($scope.activePlacesList[i].passengerIndex === j) {
                                        $scope.allPassengers[j].coordinate = null;
                                        $scope.allPassengers[j].coordinateValue = null;
                                        break;
                                    }
                                }
                                $scope.activePlacesList.splice(i, 1);
                                break;
                            }
                        }
                    }
                    else {
                        if ($scope.activePlacesList.length >= $scope.tourSearchInfo.passengerCount) {
                            return;
                        }
                        point.active = true;
                        for (var i = 0; i < $scope.allPassengers.length; i++) {
                            if ($scope.allPassengers[i].coordinate === null) {
                                $scope.activePlacesList.push({
                                    coordinate: point.coordinate,
                                    value: point.value,
                                    passenger: $scope.allPassengers[i].name + ' ' + $scope.allPassengers[i].surname,
                                    passengerIndex: i
                                });
                                $scope.allPassengers[i].coordinate = point.coordinate;
                                $scope.allPassengers[i].coordinateValue = point.value;
                                break;
                            }
                        }
                    }
                }
            });
        });
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.openOrderingForm = function () {
        if (!localStorage.getItem('token')) {
            alert("Будь ласка увійдіть в систему!");
        }
        $scope.ordering = true;
        $scope.allPassengers = getAllPassenger();
        setTourDetails();
    };

    $scope.lastStep = function () {
        if (!validationLastStep()) {
            return;
        }
        $scope.finishTourResult = {};
        $scope.finishTourResult.Price = parseInt($scope.tour.Price) * $scope.allPassengers.length;

        $scope.showCreditCard = parseInt($scope.methodRegistration.value) === 0;

        var BookedPlaces = [];
        $scope.allPassengers.forEach(function (pas) {
            BookedPlaces.push({
                PointX: pas.coordinate.x,
                PointY: pas.coordinate.y,
                ComfortType: $scope.tourSearchInfo.tourClass === 'Economy' ? 0 : 1
            });

            switch (parseInt(pas.baggage.external)) {
                case 0:
                    pas.baggage.externalValue = "Немає";
                    break;
                case 27:
                    pas.baggage.externalValue = "23 кг(+27$)";
                    break;
                case 38:
                    pas.baggage.externalValue = "32 кг(+38$)";
                    break;
            }

            switch (parseInt(pas.baggage.inner)) {
                case 0:
                    pas.baggage.innerValue = "Маленька(безкоштовна)";
                    break;
                case 14:
                    pas.baggage.innerValue = "Велика(+14$)";
                    break;
            }

            $scope.finishTourResult.Price += parseInt(pas.baggage.inner) + parseInt(pas.baggage.external);
        });

        $scope.finishTourResult.Price += parseInt($scope.methodRegistration.value);
        $scope.methodRegisterValue = parseInt($scope.methodRegistration.value) === 0 ? 'Онлайн' : 'Аеропорт(+10$)';

        var TourDetails = {
            DateStart: getGeneralDateFormat($scope.tourSearchInfo.dateStartR, $scope.tourSearchInfo.timeStart),
            DateFinish: getGeneralDateFormat($scope.tourSearchInfo.dateFinishR, $scope.tourSearchInfo.timeFinish),
            CurrentCountOfBusinessPassenger: $scope.tourSearchInfo.tourClass !== 'Economy' ? $scope.allPassengers.length : 0,
            CurrentCountOfEconomyPassenger: $scope.tourSearchInfo.tourClass === 'Economy' ? $scope.allPassengers.length : 0,
            Temporary: true,
            TourId: $routeParams.id
        };

        tourDetailsService.saveTourDetails(TourDetails).then(function (data) {
            BookedPlaces.forEach(function (b) {
                b.TourDetailsId = data.TourDetailsId;
            });
            $scope.tourDetailId = data.TourDetailsId;
            $http.post(Constants.REST_URL + "api/BookedPlaces", BookedPlaces, {
                headers: {
                    'Content-type': 'application/json'
                }
            }).then(function () {
                console.log("fine");
            }, function () {
                console.log("error")
            });
        }, function (data) {
            console.log("fail");
        });
        $scope.showCheckStep = true;
        timerStart();
    };


    $scope.orderTickets = function () {
        var data = prepareDataForOrder();
        $http.post(Constants.REST_URL + "api/Orders?tourDetailsId=" + $scope.tourDetailId, data, {
            headers: {
                'Content-type': 'application/json'
            }
        }).then(function () {
            alert("Квиток замовлено успішно! Перевірте свою пошту для детальної інформації");
            var html = 'Ви успішно здійснили замовлення на рейс № ' + $routeParams.id + '(' + $scope.tour.AirportFrom.Name
            + ' - ' + $scope.tour.AirportTo.Name +'), та купили такі квитки: <br>';
            $scope.allPassengers.forEach(function(pas) {
                html +=  "<div>";
                html +=  "<h3>" + (pas.id + 1) + " Квиток</h3>";
                html +=  "<h4>Ініціали пасажира: " + pas.name + " " + pas.surname + "</h4>";
                html +=  "<h5>Місце: " + pas.coordinateValue + "</h5>";
            });
            html += "<h4>Метод реєстрації - " + $scope.methodRegisterValue + "</h4>";
            html += "<h4><label>Загальна ціна: " + $scope.finishTourResult.Price + "$</label></h4>";
            sendMailService.sendMail(html);
            $location.path('ordersList');
        }, function () {
            alert("Сталася помилка під час замовлення квитка!");
        });
    };

    $scope.cancel = function() {
        if ($scope.tourDetailId === undefined) {
            return;
        }

        $http.delete(Constants.REST_URL + "api/TourDetails/" + $scope.tourDetailId, {
            headers: {
                'Content-type': 'application/json'
            }
        }).then(function () {
            $scope.openOrderingForm();
            $scope.showCheckStep = false;
            $scope.showCreditCard = false;
            console.log("fine");
        }, function () {
            console.log("error")
        });
    };

    function prepareDataForOrder() {
        var Passengers = [];
        $scope.allPassengers.forEach(function (pas) {
            Passengers.push({
                PassengerType: 0,
                FirstName: pas.name,
                LastName: pas.surname,
                Gender: pas.gender,
                CoordinateValue: pas.coordinateValue,
                IdCard: null,
                TicketPrice: parseInt(pas.baggage.external) + parseInt(pas.baggage.inner) + parseInt($scope.tour.Price),
                BaggageInternal: $scope.tourSearchInfo.tourClass !== "Economy" ? true : !!parseInt(pas.baggage.inner),
                BaggageeExternal: $scope.tourSearchInfo.tourClass !== "Economy" ? true : !!parseInt(pas.baggage.external)
            });
        });
        var Tours = [];
        Tours.push({
            TourId: $scope.tour.TourId
        });
        return {
            Date: new Date(),
            Price: $scope.finishTourResult.Price,
            Paid: $scope.showCreditCard,
            UserId: jwtHelper.decodeToken(localStorage.getItem('token')).id,
            DateStartTour: getGeneralDateFormat($scope.tourSearchInfo.dateStartR, $scope.tourSearchInfo.timeStart),
            DateFinishTour: getGeneralDateFormat($scope.tourSearchInfo.dateFinishR, $scope.tourSearchInfo.timeFinish),
            Passengers: Passengers,
            Tours: Tours
        };
    }


    function validationLastStep() {
        if ($scope.activePlacesList.length !== $scope.allPassengers.length) {
            alert("Виберіть місця!");
            return false;
        }
        for (var i = 0; i < $scope.allPassengers.length; i++) {
            var pas = $scope.allPassengers[i];
            if (!pas.name || !pas.surname || pas.gender === null) {
                alert("Заповніть дані про пасажирів!");
                return false;
            }
        }
        return true;
    }

    function setTourDetails() {
        $scope.allPlaces = $scope.allPlaces !== undefined ? $scope.allPlaces : getAllPlaces();
        var additionUrl = {
            comfort: $scope.tourSearchInfo.tourClass === 'Economy' ? 0 : 1,
            dateStart: getGeneralDateFormat($scope.tourSearchInfo.dateStartR, $scope.tourSearchInfo.timeStart),
            dateFinish: getGeneralDateFormat($scope.tourSearchInfo.dateFinishR, $scope.tourSearchInfo.timeFinish)
        };
        tourDetailsService.getTourDetails($routeParams.id, additionUrl).then(function (data) {
            setCheckedPlaces(data.BookedPoints);
            console.log(data);
        }, function (data) {
            console.log(data);
        })
    }

    function getAllPassenger() {
        var list = [];
        for (var i = 0; i < $scope.tourSearchInfo.passengerCount; i++) {
            list.push({
                id: i,
                name: '',
                surname: '',
                gender: null,
                idCard: null,
                baggage: {
                    inner: 0,
                    external: 0
                },
                coordinate: null,
                coordinateValue: null
            });
        }
        return list;
    }

    function setCheckedPlaces(bookedPoints) {
        if (bookedPoints === undefined) {
            return;
        }

        $scope.allPlaces.forEach(function (place) {
            place.forEach(function (point) {
                for (var i = 0; i < bookedPoints.length; i++) {
                    if (point.coordinate.x === bookedPoints[i].X && point.coordinate.y === bookedPoints[i].Y) {
                        point.booked = true;
                        break;
                    }
                }
            });
        });
    }


    function getAllPlaces() {
        var tmp = $scope.tourSearchInfo.tourClass === 'Economy' ? 'CountOfEconomyPassenger' : 'CountOfBusinessPassenger';
        var list = [];
        var res = [];
        var literals = ['A', 'B', 'C', 'D', 'E', 'F'];
        for (var i = 1, x = 0, y = 0; i <= $scope.tour.Plane.MaxCountPassenger[tmp]; i++, x++) {
            list.push({
                value: (y + 1) + literals[x],
                coordinate: {
                    x: x,
                    y: y
                },
                booked: false,
                active: false
            });
            if (i % 6 === 0) {
                res.push(list);
                list = [];
                x = -1;
                y++;
            }
        }
        return res;
    }

    function getGeneralDateFormat(date, time) {
        var from = date.split("-");
        return from[2] + '-' + from[1] + '-' + from[0] + 'T' + time + ':00';
    }

    function setupURL() {
        $location.search('dateStart', $scope.tourSearchInfo.dateStartR);
        $location.search('dateFinish', $scope.tourSearchInfo.dateFinishR);
        $location.search('timeStart', $scope.tourSearchInfo.timeStart);
        $location.search('timeFinish', $scope.tourSearchInfo.timeFinish);
        $location.search('dayStart', $scope.tourSearchInfo.dayStartO);
        $location.search('dayFinish', $scope.tourSearchInfo.dayFinishO);
        $location.search('tourClass', $scope.tourSearchInfo.tourClass);
        $location.search('passengerCount', $scope.tourSearchInfo.passengerCount);
    }

    function getSearchInfoURL() {
        return {
            dateStartR: $location.search().dateStart,
            dateFinishR: $location.search().dateFinish,
            timeStart: $location.search().timeStart,
            timeFinish: $location.search().timeFinish,
            dayStartO: $location.search().dayStart,
            dayFinishO: $location.search().dayFinish,
            tourClass: $location.search().tourClass,
            passengerCount: $location.search().passengerCount
        }
    }

    function correctData(coef) {
        $scope.tour.timeStart = $filter('date')($scope.tour.DateStart, 'HH:mm');
        $scope.tour.timeFinish = $filter('date')($scope.tour.DateFinish, 'HH:mm');
        $scope.tour.dateStartR = $scope.tourSearchInfo.dateStartR;
        $scope.tour.dateFinishR = $scope.tourSearchInfo.dateFinishR;
        $scope.tour.Price = ($scope.tour.Price * coef).toFixed(0);
        $scope.tour.dayStart = utils.translateDays($scope.tourSearchInfo.dayStartO);
        $scope.tour.dayFinish = utils.translateDays($scope.tourSearchInfo.dayFinishO);
    }

    function timerStart() {
        $scope.name = 'Superhero';
        $scope.counter = 300;
        $scope.onTimeout = function () {
            if ($scope.counter > 0) $scope.counter--;
            mytimeout = $timeout($scope.onTimeout, 1000);
        };
        var mytimeout = $timeout($scope.onTimeout, 1000);
    }
});