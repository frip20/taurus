
app.factory('maquinariaService', function ($http, $q) {
    var _factory = {};

    _factory.getAllMaquinas = function () {
        var deferred = $q.defer();
        $http.get('api/maquinaria').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on maquinariaService');
        });
        return deferred.promise;
    };

    _factory.requestMaquina = function (maquina, action) {
        var deferred = $q.defer();
        $http.post('api/maquinaria', { Maquina: maquina, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on maquinariaService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('maquinariaController', function ($scope, areasService, maquinariaService) {
    $scope.searchBy = { Action: 7, Maquina: {} };
    $scope.areas = [];
    $scope.maquinas = [];
    $scope.maquina = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    areasService.getAllAreas()
            .then(function (data) {
                $scope.areas = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    $scope.editItem = function (item, index) {
        $scope.maquina = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.maquina = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.maquina = { Id: 0, Operador: { Id: 0} };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (maquina, index) {
        maquinariaService.requestMaquina(maquina, 2)
            .then(function (data) {
                $scope.maquinas.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        if ($scope.validForm()) {
            maquinariaService.requestMaquina($scope.maquina, action)
                .then(function (data) {
                    if (action == 1)
                        $scope.maquinas.push(data);
                    else
                        $scope.maquinas[$scope.editing] = data;

                    $scope.Cancelar();
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    };

    $scope.validForm = function () {

        if ($scope.maquina.Area == null) {
            $scope.errorForm = 'El campo Area es requerido';
            return false;
        }

        return true;
    };

    $scope.filterBy = function () {
        $scope.searchBy = { Action: 7, Maquina: { Description: $('#searchDesc').val(), Placa: $('#searchPlaca').val(), Operador: { Description: $('#searchOp').val()}} };
    };

    $scope.clearFilter = function () {
        if ($('#searchDesc').val().trim() != '' || $('#searchPlaca').val().trim() || $('#searchOp').val().trim()) {
            $('#searchForm input[type=text]').val('');
            $scope.searchBy = { Action: 7, Articulo: {} };
        }
    }

});