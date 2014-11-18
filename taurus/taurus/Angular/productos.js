
app.factory('productosService', function ($http, $q) {
    var _factory = {};

    _factory.getAllProductos = function () {
        var deferred = $q.defer();
        $http.get('api/articulo?search=').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on productosService');
        });
        return deferred.promise;
    };

    _factory.requestProducto = function (producto, action) {
        var deferred = $q.defer();
        $http.post('api/articulo', { Articulo: producto, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on productosService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('productosController', function ($scope, productosService) {
    $scope.searchBy = { Action: 7, Articulo: {} };
    $scope.productos = [];
    $scope.producto = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    $scope.editItem = function (item, index) {
        $scope.producto = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.producto = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.producto = { Id: 0 };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (producto, index) {
        productosService.requestProducto(producto, 2)
            .then(function (data) {
                $scope.productos.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        productosService.requestProducto($scope.producto, action)
            .then(function (data) {
                if (action == 1)
                    $scope.productos.push(data);
                else
                    $scope.productos[$scope.editing] = data;

                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    };

    $scope.filterBy = function () {
        $scope.searchBy = { Action: 7, Articulo: { Description: $('#searchDesc').val(), Parte: $('#searchParte').val()} };
    };

    $scope.clearFilter = function () {
        if ($('#searchDesc').val().trim() != '' || $('#searchParte').val().trim()) {
            $('#searchForm input[type=text]').val('');
            $scope.searchBy = { Action: 7, Articulo: {} };
        }
    }

});