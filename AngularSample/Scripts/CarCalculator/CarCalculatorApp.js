﻿

var carCalcApp = angular.module("CarCalculatorApp", ['ngAnimate', 'ngTable', 'ngResource', 'ngRoute', "chart.js"]);

carCalcApp.controller("CarCalculatorController", function ($scope, $http, $filter, NgTableParams, $location) {
    $scope.fuelEntries = [];
    $scope.selectedCar = {};
    $scope.lastDistance = 0;
    $scope.currentEntry = {};
    $scope.resetCurrentEntry = function() {
        $scope.currentEntry.date = new Date(Date.now());
       
    }

    $scope.resetCurrentEntry();
   
    $scope.addError = true;

    $scope.getEntries = function() {
        $http.get("/api/fuelEntryModels").success(function(data, status, headers, config) {
            $scope.fuelEntries = data;
            $scope.refreshChart();
        }).error(function(data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });
    };

    $scope.setDefault = function () {
        $http.post('/api/cars/'+$scope.selectedCar.id+'/setDefault').success(function (data, status, headers, config) {
            console.log('set default - success');
            $scope.tableParams.reload();
            toastr.success($scope.selectedCar.carName + ' został ustawiony jako domyślny');
            $location.path('/list');

        }).error(function (data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });

    };


    $scope.groupBy = 'year';
    $scope.newRowId = -1;

    $scope.tableParams = new NgTableParams({
        group: 'year'
        }, {
           
            getData: function ($defer, params) {

                $http.get("/api/fuelEntryModels").success(function (data, status, headers, config) {
                    params.total(data.length);
                    $scope.fuelEntries = data;
                    $scope.refreshChart();
                    $defer.resolve(data);
                }).error(function (data, status, headers, config) {
                    toastr.error(data, 'Błąd');
                });
            }
        });

 
    $scope.addFuelEntry = function() {

        if (!$scope.fuelEntryForm.$valid) {

            toastr.error('Formularz zawiera błędne dane', 'Błąd');
            return;
        }
        $scope.currentEntry.carId = $scope.selectedCar.id;

        $http.post('/api/fuelEntryModels',  $scope.currentEntry).success(function (data, status, headers, config) {
           

            $scope.fuelEntries.push($scope.currentEntry);
            $scope.lastDistance = $scope.currentEntry.lastDistance;
            $scope.currentEntry = {};
            $scope.currentEntry.distance = $scope.lastDistance;

            $scope.newRowId = data.id;

            $scope.resetCurrentEntry();
            //
            toastr.success('Pomyślnie dodano nowy wpis', 'Nowe tankowanie');
            $scope.tableParams.reload();

            //$(".closeModal").click();

        }).error(function(data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });


        
    };

    $scope.calcSum = function (data) {
        var distanceSum = 0;
        var amountSum = 0;
        angular.forEach(data, function (item) {
            distanceSum += item.currentDistance;
            amountSum += item.amountOfFuel;
        });

        var amountPer100Km = amountSum / distanceSum * 100;

        return {a: amountPer100Km, b: amountSum};
    }
    
    $scope.labels = [];
    $scope.series = ['Przebieg'];
    $scope.data = [];
    $scope.distanceSeries = [];

    $scope.refreshChart = function() {
        angular.forEach($scope.fuelEntries, function (entry) {

            $scope.labels.push(entry.date);
            $scope.distanceSeries.push(entry.distance);
        });
        $scope.distanceSeries = [];
        $scope.data.push($scope.distanceSeries);
    }

    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };

    $scope.refreshChart();
});

carCalcApp.config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/dashboard', { templateUrl: '/CarCalculator/CarCalculator/dashboard', controller: 'CarCalculatorController' })
            .when('/list', { templateUrl: '/CarCalculator/CarCalculator/list', controller: 'CarCalculatorController' })
            .when('/add', { templateUrl: '/CarCalculator/CarCalculator/newEntry', controller: 'CarCalculatorController' })
            .when('/stats', { templateUrl: '/CarCalculator/CarCalculator/statistics', controller: 'CarCalculatorController' })
            .when('/', { templateUrl: '/CarCalculator/CarCalculator/list', controller: 'CarCalculatorController' });
    }
]);

carCalcApp.controller("TabController", function($scope) {
    $scope.currentTab = -1;

    $scope.isSelected = function(value) {
        return $scope.currentTab === value;
    }

    $scope.setTab = function(value) {
        $scope.currentTab = value;
    };
});