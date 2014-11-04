app.factory('bicatoraService', function ($http, $q) {
    var _factory = {};

    _factory.getBitacoraByMaquina = function (maquinaId) {
        var deferred = $q.defer();
        $http.get('api/bitacora?maquinaId=' + maquinaId).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on bicatoraService');
        });
        return deferred.promise;
    };

    _factory.addBicatora = function (bitacora, action) {
        var deferred = $q.defer();
        $http.post('api/bitacora', { Bitacora: bitacora, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on bicatoraService');
        });
        return deferred.promise;
    };

    _factory.deleteBicatora = function (bitacoraId) {
        var deferred = $q.defer();
        $http.post('api/bitacora', { Bitacora: {Id: bitacoraId}, Action: 2 }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on bicatoraService');
        });
        return deferred.promise;
    };

    return _factory;

});

app.controller('bitacoraController', function ($scope, bicatoraService) {
    $scope.newItem = { createDate: new Date(), Maquina: { Id: 0} };
    $scope.bitacora = [];
    $scope.index = -1;


    $scope.changeMaquina = function (obj) {
        bicatoraService.getBitacoraByMaquina($('#maquina_value').val())
            .then(function (data) {
                $scope.bitacora = data;
            }, function (data) {
                $scope.errorForm = data;
            });
    };


    $scope.addItem = function (action) {
        if ($scope.validForm()) {
            bicatoraService.addBicatora($scope.newItem, action)
                .then(function (data) {
                    if (action == 1)
                        $scope.bitacora.push(data);
                    else
                        $scope.bitacora[$scope.index] = data;

                    $scope.newItem = { createDate: new Date(), Maquina: data.Maquina, Proveedor: { Id: 0} };
                    $scope.index = -1;
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    };

    $scope.editItem = function (item, index) {
        $scope.index = index;
        $scope.newItem = angular.copy(item);
        
    };

    $scope.cancelEdit = function () {
        $scope.index = -1;
        $scope.newItem = { createDate: new Date(), Maquina: $scope.newItem.Maquina, Proveedor: { Id: 0} };
    };

    $scope.deleteItem = function (item, index) {
        bicatoraService.deleteBicatora(item)
                .then(function (data) {
                    $scope.bitacora.splice(index, 1);
                }, function (data) {
                    $scope.errorForm = data;
                });
    };

    $scope.validForm = function () {
        if ($scope.newItem.Consumo == null) {
            $scope.errorForm = 'El campo consumo es requerido';
            return false;
        }

        if ($scope.newItem.Proveedor == null) {
            $scope.errorForm = 'El campo proveedor es requerido';
            return false;
        }

        if ($scope.newItem.Cantidad == null) {
            $scope.errorForm = 'El campo cantidad es requerido';
            return false;
        }

        if ($scope.newItem.Costo == null) {
            $scope.errorForm = 'El campo costo es requerido';
            return false;
        }

        if ($scope.newItem.Kms == null) {
            $scope.errorForm = 'El campo kilometraje es requerido';
            return false;
        }

        return true;
    };

});