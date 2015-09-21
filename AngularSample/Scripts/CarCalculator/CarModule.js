
carCalcApp.controller("CarController", function ($scope, $http, $route, NgTableParams, $location) {
   
    $scope.cars = [];
    $scope.selectedCar = {};
    $scope.newCar = {};

    $scope.getCars = function () {
        $http.get("/api/cars").success(function (data, status, headers, config) {
            $scope.cars = data;
            angular.forEach(data, function (car) {
                if (car.isSelected) {
                    $scope.selectedCar = car;
                    $scope.currentCarName = car.carName;
                }
            });

        }).error(function (data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });
    };
  
    $scope.addCar = function () {
        $http.post('/api/cars', $scope.newCar).success(function (data, status, headers, config) {
            $scope.newCar.id = data.id;
            $scope.cars.push($scope.newCar);
            $scope.newCar = {};

            $(".closeModal").click();
            toastr.success('Dodano nowy samochód!');
        }).error(function (data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });
       
    };

    $scope.deleteCar = function () {
        $http.delete('/api/cars/'+ $scope.selectedCar.id).success(function (data, status, headers, config) {
            var deletedCarName = $scope.selectedCar.carName;
            $scope.cars.pop($scope.selectedCar);
            $scope.selectedCar = { carName: '', id: -1 };
            toastr.success(deletedCarName + ' został usunięty z bazy');

        }).error(function (data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });

    };

    $scope.setDefault = function () {
        $http.post('/api/cars/'+$scope.selectedCar.id+'/setDefault').success(function (data, status, headers, config) {
            console.log('set default - success');
            toastr.success($scope.selectedCar.carName + ' został ustawiony jako domyślny');
            $route.reload();

        }).error(function (data, status, headers, config) {
            toastr.error(data, 'Błąd');
        });

    };
});
