app.factory('stockService', function ($http, $q) {
    var _factory = {};

    _factory.getStockById = function (id) {
        var deferred = $q.defer();
        $http.get('api/stock?stockId=' + id).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    _factory.insertStock = function (stock, action) {
        var deferred = $q.defer();
        $http.post('api/stock', { Stock: stock, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    _factory.deleteStock = function (stockId) {
        var deferred = $q.defer();
        console.log(stockId);
        $http.post('api/stock', { Stock: { Id: stockId }, Action: 2 }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    _factory.getMovimientos = function (criteria) {
        var deferred = $q.defer();
        $http.post('api/stockmov', criteria).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    _factory.updateItem = function (item, action, type, stockId) {
        var deferred = $q.defer();
        $http.post('api/stockitem', {Action: action, StockItem: item, Type: type, StockId: stockId}).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    return _factory;
});

app.controller('stockController', function ($scope, $modal, $http, $q, $routeParams, $location, stockService, loginService) {
    $scope.stock = { Id: 0, Items: [], CreateDate: new Date() };
    $scope.newItem = {};
    $scope.indexEdit = -1;
    $scope.errorForm = '';
    $scope.doingPoliza = false;
    $scope.polizaActions = [];
    $scope.canSendPoliza = loginService.hasPermission(105);

    $scope.init = function () {
        if ($routeParams.stock_id != null) {
            $scope.stock.Id = $routeParams.stock_id;
            stockService.getStockById($routeParams.stock_id)
                .then(function (data) {
                    $scope.proveedorSelected = { 'title': data.Proveedor.Description, 'description': data.Proveedor.RFC, 'image': '', 'originalObject': data.Proveedor };
                    $scope.stock = data;
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    }

    $scope.addItem = function () {
        if ($scope.validForm()) {
            if ($scope.indexEdit >= 0) {
                stockService.updateItem($scope.newItem, 3, 1, $scope.stock.Id)//entrada
                .then(function (data) {
                    $scope.stock.Items[$scope.indexEdit] = $scope.newItem;
                    $scope.clearForm();
                }, function (data) {
                    $scope.errorForm = data;
                });
            } else {
                $scope.stock.Items.push($scope.newItem);
                $scope.clearForm();
            }
            
            //close popup
            this.$hide();
        }
    };

    $scope.editItem = function (item, index) {
        $scope.indexEdit = index;
        angular.copy(item, $scope.newItem);
        $scope.articuloSelected = { 'title': item.Articulo.Description, 'description': item.Articulo.Parte, 'image': '', 'originalObject': item.Articulo };
        //$scope.sisSelected = { title: item.Sistema.Description, description: item.Sistema.Clave, image: '', originalObject: item.Sistema };
        var myOtherModal = $modal({ scope: $scope, template: '/templates/addItemStock.htm', show: true });
    };

    $scope.deleteItem = function (item, index) {
        if (item.Id > 0) {
            stockService.updateItem(item, 2, 1)//entrada
                .then(function (data) {
                    $scope.stock.Items.splice(index, 1);
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
        
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

    $scope.addStock = function (action) {
        if ($scope.validStock()) {
            $scope.stock.Type = 1; //Entrada

            stockService.insertStock($scope.stock, action)//1:ADD  3:UPDATE
                .then(function (data) {
                    $location.path('/stockmovs');
                }, function (data) {
                    $scope.errorForm = data;
                });

        }
    };

    $scope.cancelar = function () {
        $location.path('/stockmovs');
    };

    $scope.updateStock = function () {
        if ($scope.validStock()) {
            $scope.stock.Type = 1; //Entrada

            stockService.insertStock($scope.stock)
                .then(function (data) {
                    $location.path('/stockmovs');
                }, function (data) {
                    $scope.errorForm = data;
                });

        }
    };

    $scope.readyForPoliza = function () {
        var ready = true;
        for (var i = 0; i < $scope.stock.Items.length; i++) {
            if (ready && $scope.stock.Items[i].Cuenta == null)
                ready = false;
        }

        if (!ready) {
            $scope.errorForm = 'No todos los articulos tienen una cuenta asignada';
        } else {
            $scope.doingPoliza = true;
            $scope.polizaActions.push('Generando poliza');
            $scope.polizaActions.push('Enviando informacion a Contpaq');


        }
    }

});

app.controller('stockMovController', function ($scope, $modal, $http, $q, $location, $window, stockService) {
    $scope.movimientos = [];
    $scope.criteria = {};

    $scope.init = function () {
        stockService.getMovimientos($scope.criteria)
            .then(function (data) {
                $scope.movimientos = data;
            }, function (data) {
                console.log('error!!');
            });
    };


    $scope.searchMovimientos = function () {
        if ($scope.validSearch()) {
            stockService.getMovimientos($scope.criteria)
                .then(function (data) {
                    $scope.movimientos = data;
                }, function (data) {
                    console.log('error!!');
                });
        }
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

    $scope.editStock = function (id, type) {
        if (type == 1)
            $location.path("/stockin/" + id);
        else
            $location.path("/stockout/" + id);
    };

    $scope.deleteItem = function (id, index) {
        stockService.deleteStock(id)
            .then(function (data) {
                $window.alert('El registro a sido eliminado.');
                $scope.movimientos.splice(index, 1);
            }, function (data) {
                $scope.errorForm = data;
            });
    };

});




app.controller('stockOutController', function ($scope, $modal, $http, $q, $routeParams, $location, $window, stockService, departmentService, loginService) {
    $scope.stock = { Id: 0, Items: [], CreateDate: new Date(), Uso: null, Type: 2 };
    $scope.departments = [];
    $scope.newItem = {};
    $scope.indexEdit = -1;
    $scope.errorForm = '';
    $scope.doingPoliza = false;
    $scope.polizaActions = [];
    $scope.canSendPoliza = loginService.hasPermission(105);

    $scope.init = function () {
        departmentService.getAllDepartments()
            .then(function (data) {
                $scope.departments = data;
            }, function (data) {
                $scope.errorForm = data;
            });

        if ($routeParams.stock_id != null) {
            $scope.stock.Id = $routeParams.stock_id;
            stockService.getStockById($routeParams.stock_id)
                .then(function (data) {
                    //$scope.proveedorSelected = { 'title': data.Proveedor.Description, 'description': data.Proveedor.RFC, 'image': '', 'originalObject': data.Proveedor };
                    $scope.stock = data;
                }, function (data) {
                    $scope.errorForm = data;
                });
        }
    };

    $scope.closePopup = function () {
        $scope.clearForm();
        this.$hide();
    };

    $scope.clearForm = function () {
        $scope.indexEdit = -1;
        $scope.newItem = {};
        $scope.articuloSelected = {};
        $scope.errorForm = '';
    };

    $scope.addItem = function () {
        if ($scope.validForm()) {
            if ($scope.indexEdit >= 0) {
                stockService.updateItem($scope.newItem, 3, 2, $scope.stock.Id) //salida
                .then(function (data) {
                    $scope.stock.Items[$scope.indexEdit] = data;
                    $scope.clearForm();
                }, function (data) {
                    $scope.errorForm = data;
                });
            } else {
                $scope.stock.Items.push($scope.newItem);
                $scope.clearForm();
            }
            

            //close popup
            this.$hide();
        }
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
            $scope.errorForm = 'El sistema es requerido';
            return false;
        }
        return true;
    };

    $scope.validStock = function () {
        if ($scope.stock.Departamento == null) {
            $scope.errorForm = 'El departamento es requerido';
            return false;
        }

        if ($scope.stock.Uso == null) {
            var _uso = $window.document.getElementById('uso_value').value.trim();
            if (_uso == null || _uso === '') {
                $scope.errorForm = 'El campo para uso en es requerido';
                return false;
            }
            else {
                $scope.stock.Uso = { Id: 0, Description: _uso };
            }
        }

        if ($scope.stock.Responsable == null) {
            $scope.errorForm = 'El responsable es requerido';
            return false;
        }

        if ($scope.stock.CreateDate == null) {
            $scope.errorForm = 'El campo fecha es requerido';
            return false;
        }

        return true;
    };

    $scope.addStock = function (action) {
        if ($scope.validStock()) {
            stockService.insertStock($scope.stock, action)//1:ADD  3:UPDATE
                .then(function (data) {
                    $location.path('/stockmovs');
                }, function (data) {
                    $scope.errorForm = data;
                });

        }
    };


    $scope.cancelar = function () {
        $location.path('/stockmovs');
    };

    $scope.updateStock = function () {
        if ($scope.validStock()) {
            stockService.insertStock($scope.stock)
                .then(function (data) {
                    $location.path('/stockmovs');
                }, function (data) {
                    $scope.errorForm = data;
                });

        }
    };

    $scope.deleteItem = function (item, index) {
        if (item.Id > 0) {
            stockService.updateItem(item, 2, 2)//salida
            .then(function (data) {
                $scope.stock.Items.splice(index, 1);
            }, function (data) {
                $scope.errorForm = data;
            });
        }
    };

    $scope.editItem = function (item, index) {
        $scope.indexEdit = index;
        item.Stock = { Id: $scope.stock.Id };
        angular.copy(item, $scope.newItem);
        $scope.articuloSelected = { 'title': item.Articulo.Description, 'description': item.Articulo.Parte, 'image': '', 'originalObject': item.Articulo };
        $scope.sisSelected = { title: item.Sistema.Description, description: item.Sistema.Clave, image: '', originalObject: item.Sistema };
        var myOtherModal = $modal({ scope: $scope, template: '/templates/addItemStockOut.htm', show: true });
    };

    $scope.readyForPoliza = function () {
        var ready = true;
        for (var i = 0; i < $scope.stock.Items.length; i++) {
            if (ready && $scope.stock.Items[i].Cuenta == null)
                ready = false;
        }

        if (!ready) {
            $scope.errorForm = 'No todos los articulos tienen una cuenta asignada';
        } else {
            $scope.doingPoliza = true;
            $scope.polizaActions.push('Generando poliza');
            $scope.polizaActions.push('Enviando informacion a Contpaq');

            
        }
    }
});