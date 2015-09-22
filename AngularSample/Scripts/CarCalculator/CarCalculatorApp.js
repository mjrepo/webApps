window.mobilecheck = function() {
  var check = false;
  (function(a){if(/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a)||/1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0,4)))check = true})(navigator.userAgent||navigator.vendor||window.opera);
  return check;
}

var carCalcApp = angular.module("CarCalculatorApp", ['ngAnimate', 'ngTable', 'ngResource', 'ngRoute', "chart.js"]);

carCalcApp.controller("CarCalculatorController", function ($scope, $http, $filter, NgTableParams, $location) {
    $scope.fuelEntries = [];
    $scope.selectedCar = {};
    $scope.lastDistance = 0;
    $scope.currentEntry = {};
    $scope.resetCurrentEntry = function() {
        $scope.currentEntry.date = new Date(Date.now());
       
    }

    $scope.checkIfMobile = function() {
        if (mobilecheck()) {
            toastr.success('Mobile');
        } else {
            toastr.success('Not Mobile');
        }
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
    '$routeProvider', function ($routeProvider, $locationProvider) {
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