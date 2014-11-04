
app.factory('sistemasService', function ($http, $q) {
    var _factory = {};

    _factory.getAllSistemas = function () {
        var deferred = $q.defer();
        $http.get('api/sistema?search=').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on sistemasService');
        });
        return deferred.promise;
    };

    _factory.requestSistema = function (sistema, action) {
        var deferred = $q.defer();
        $http.post('api/sistema', { Sistema: sistema, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on sistemasService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('sistemasController', function ($scope, sistemasService) {
    $scope.sistemas = [];
    $scope.sistema = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    sistemasService.getAllSistemas()
            .then(function (data) {
                $scope.sistemas = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    $scope.editItem = function (item, index) {
        $scope.sistema = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.sistema = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.sistema = { Id: 0 };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (sistema, index) {
        sistemasService.requestSistema(sistema, 2)
            .then(function (data) {
                $scope.sistemas.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        if ($scope.validForm()) {
            sistemasService.requestSistema($scope.sistema, action)
                .then(function (data) {
                    if (action == 1) {
                        $scope.sistemas.push(data);
                    }
                    else {
                        $scope.sistemas[$scope.editing] = data;
                    }
                    $scope.Cancelar();
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    };

    $scope.validForm = function () {
        if ($scope.sistema.Description == null) {
            $scope.errorForm = 'El campo descripcion es requerido';
            return false;
        }
        return true;
    };

});