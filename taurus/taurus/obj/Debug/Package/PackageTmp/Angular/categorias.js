
app.factory('categoriasService', function ($http, $q) {
    var _factory = {};

    _factory.getAllCategorias = function () {
        var deferred = $q.defer();
        $http.get('api/categorias').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on categoriasService');
        });
        return deferred.promise;
    };

    _factory.requestCategoria = function (categoria, action) {
        var deferred = $q.defer();
        $http.post('api/categorias', { Categoria: categoria, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on categoriasService');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('categoriasController', function ($scope, categoriasService) {
    $scope.categorias = [];
    $scope.categoria = { Id: 0 };
    $scope.editing = -1;
    $scope.errorForm = '';

    categoriasService.getAllCategorias()
            .then(function (data) {
                $scope.categorias = data;
            }, function (data) {
                $scope.errorForm = data;
            });

    $scope.editItem = function (item, index) {
        $scope.categoria = angular.copy(item);
        $scope.editing = index;
    };

    $scope.Cancelar = function () {
        $scope.categoria = { Id: 0 };
        $scope.editing = -1;
        $scope.errorForm = '';
    };

    $scope.newItem = function () {
        $scope.categoria = { Id: 0 };
        $scope.editing = 0;
    };

    $scope.deleteItem = function (categoria, index) {
        categoriasService.requestCategoria(categoria, 2)
            .then(function (data) {
                $scope.categorias.splice(index, 1);
                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    }

    $scope.addItem = function (action) {
        categoriasService.requestCategoria($scope.categoria, action)
            .then(function (data) {
                if (action == 1)
                    $scope.categorias.push(data);
                else
                    $scope.categorias[$scope.editing] = data;

                $scope.Cancelar();
            }, function (data) {
                $scope.errorForm = data;
            });
    };

});