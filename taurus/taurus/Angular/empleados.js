
app.factory('empleadosService', function ($http, $q) {
    var _factory = {};

    _factory.getAllEmpleados = function () {
        var deferred = $q.defer();
        $http.get('api/empleados?search=').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on empleadosService');
        });
        return deferred.promise;
    };

    _factory.requestEmpleado = function (emp, action) {
        var deferred = $q.defer();
        $http.post('api/areas', { Empleado: emp, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on empleadosService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('empleadosController', function ($scope, areasService, empleadosService) {
    $scope.areas = [];
    $scope.empleados = [];
    $scope.empleado = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    areasService.getAllAreas()
            .then(function (data) {
                $scope.areas = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    $scope.editItem = function (item, index) {
        $scope.empleado = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.empleado = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.empleado = { Id: 0 };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (empleado, index) {
        empleadosService.requestArea(empleado, 2)
            .then(function (data) {
                $scope.empleados.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        empleadosService.requestArea($scope.empleado, action)
            .then(function (data) {
                if (action == 1)
                    $scope.empleados.push(data);
                else
                    $scope.empleados[$scope.editing] = data;

                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    };

});