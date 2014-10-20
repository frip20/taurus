app.controller('stockController', function ($scope, $modal, $http, $q, $routeParams, $location) {
    $scope.stock = { Id: 0, Items: [], CreateDate: new Date() };
    $scope.newItem = {};
    $scope.indexEdit = -1;
    $scope.errorForm = '';

    $scope.init = function () {
        if ($routeParams.stock_id != null) {
            $scope.getStockById($routeParams.stock_id)
                .then(function (data) {
                    $scope.proveedorSelected = { 'title': data.Proveedor.Description, 'description': data.Proveedor.RFC, 'image': '', 'originalObject': data.Proveedor };
                    $scope.stock = data;
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    }

    $scope.getStockById = function (id) {
        var deferred = $q.defer();
        $scope.stock.Id = id;
        //$http.get('api/stockmov', { params: { criteria: $scope.criteria} }).success(function (apiData) {
        $http.get('api/stock?stockId=' + id).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(0)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    $scope.addItem = function () {
        if ($scope.validForm()) {

            if ($scope.indexEdit >= 0) {
                $scope.stock.Items[$scope.indexEdit] = $scope.newItem;
            } else {
                $scope.stock.Items.push($scope.newItem);
            }

            $scope.clearForm();

            //close popup
            this.$hide();
        }
    };

    $scope.editItem = function (item, index) {
        $scope.indexEdit = index;
        angular.copy(item, $scope.newItem);
        $scope.articuloSelected = { 'title': item.Articulo.Description, 'description': item.Articulo.Parte, 'image': '', 'originalObject': item.Articulo };
        $scope.sisSelected = { title: item.Sistema.Description, description: item.Sistema.Clave, image: '', originalObject: item.Sistema };
        var myOtherModal = $modal({ scope: $scope, template: '/templates/addItemStock.htm', show: true });
    };

    $scope.deleteItem = function (item, index) {
        $scope.stock.Items.splice(index, 1);
    };

    $scope.closePopup = function () {
        $scope.clearForm();
        this.$hide();
    };

    $scope.validForm = function () {
        if ($scope.newItem.Cantidad == null) {
            $scope.errorForm = 'El campo cantidad es requerido';
            return false;
        }

        if ($scope.newItem.Articulo == null) {
            $scope.errorForm = 'El campo Articulo es requerido';
            return false;
        }

        if ($scope.newItem.Sistema == null) {
            $scope.errorForm = 'El campo sistema es requerido';
            return false;
        }

        if ($scope.newItem.Unitario == null) {
            $scope.errorForm = 'El campo precio unitario es requerido';
            return false;
        }
        return true;
    };

    $scope.clearForm = function () {
        $scope.indexEdit = -1;
        $scope.newItem = {};
        $scope.articuloSelected = {};
        $scope.sisSelected = {}
        $scope.errorForm = '';
    };

    $scope.totalItems = function () {
        if ($scope.stock.Items) {
            var _total = 0;
            angular.forEach($scope.stock.Items, function (value, key) {
                _total += (value.Cantidad * value.Unitario);
            });
            return _total;
        } else { return 0; }
    };

    $scope.validStock = function () {
        if ($scope.stock.Proveedor == null) {
            $scope.errorForm = 'El campo proveedor es requerido';
            return false;
        }

        if ($scope.stock.Factura == null) {
            $scope.errorForm = 'El campo factura es requerido';
            return false;
        }

        if ($scope.stock.CreateDate == null) {
            $scope.errorForm = 'El campo fecha es requerido';
            return false;
        }

        return true;
    };

    $scope.addStock = function () {
        if ($scope.validStock()) {
            $scope.stock.Type = 1; //Entrada

            var deferred = $q.defer();
            $http.post('api/stock', $scope.stock).success(function (apiData) {
                if (apiData.Status == 'OK') {
                    deferred.resolve(apiData.jData);
                } else {
                    deferred.reject(0)
                }
            }).error(function () {
                deferred.reject(0);
            });
            return deferred.promise;
        }
    };

    $scope.cancelar = function () {
        $location.path('/stockmovs');
    };


});

app.controller('stockMovController', function ($scope, $modal, $http, $q, $location) {
    $scope.movimientos = [];
    $scope.criteria = {};

    $scope.init = function () {
        $scope.getMovimientos()
            .then(function (data) {
                $scope.movimientos = data;
            }, function (data) {
                console.log('error!!');
            });
    };


    $scope.searchMovimientos = function () {
        if ($scope.validSearch()) {
            $scope.getMovimientos()
                .then(function (data) {
                    $scope.movimientos = data;
                }, function (data) {
                    console.log('error!!');
                });
        }
    };

    $scope.getMovimientos = function () {
        var deferred = $q.defer();
        //$http.get('api/stockmov', { params: { criteria: $scope.criteria} }).success(function (apiData) {
        $http.post('api/stockmov', $scope.criteria).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(0)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    $scope.validSearch = function () {
        if ($scope.criteria.startDate != null) {
            if ($scope.criteria.endDate == null) {
                $scope.criteria.endDate = $scope.criteria.startDate;
            } else {

                if ($scope.criteria.endDate < $scope.criteria.startDate) {
                    $scope.errorForm = 'La fecha de inicio es mayor a la fecha fin';
                    return false;
                }
            }
        }
        return true;
    };

    $scope.editStock = function (id) {
        $location.path("/stockin/" + id);
    };

});