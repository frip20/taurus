
app.factory('areasService', function ($http, $q) {
    var _factory = {};

    _factory.getAllAreas = function () {
        var deferred = $q.defer();
        $http.get('api/areas').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on areasService');
        });
        return deferred.promise;
    };

    _factory.requestArea = function (area, action) {
        var deferred = $q.defer();
        $http.post('api/areas', { Area: area, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on areasService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('areasController', function ($scope, areasService, departmentService) {
    $scope.areas = [];
    $scope.departments = [];
    $scope.area = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    areasService.getAllAreas()
            .then(function (data) {
                $scope.areas = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    departmentService.getAllDepartments()
            .then(function (data) {
                $scope.departments = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    $scope.editItem = function (item, index) {
        $scope.area = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.area = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.area = { Id: 0 };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (area, index) {
        areasService.requestArea(area, 2)
            .then(function (data) {
                $scope.areas.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        areasService.requestArea($scope.area, action)
            .then(function (data) {
                if (action == 1)
                    $scope.areas.push(data);
                else
                    $scope.areas[$scope.editing] = data;

                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    };

});