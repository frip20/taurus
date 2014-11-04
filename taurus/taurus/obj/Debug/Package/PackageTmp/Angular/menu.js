app.controller('menuController', function ($scope, $http, loginService, sessionService) {
    $scope.actions = [];

    //$scope.loginUser = function () {
    //    loginService.login($scope.user, $scope);
    //};

    var loadActions = function (uid) {
        $http.post('api/menu', { Id: uid })
            .then(function (apiData) {
                if (apiData.data.Status == 'OK') {
                    $scope.actions = apiData.data.jData;
                }
            });
    };

    $scope.init = function () {
        loginService.islogged()
            .then(function (data) {
                loadActions(data);
            }, function (data) {
                console.log('error!!');
            });
    };

});

app.directive('menuItem', function () {
    return {
        restrict: 'E',
        scope: {
            mitem: '='
        },
        controller: function ($scope, $element) {
            if ($scope.mitem.routePath === 'GROUP') {
                $element.html('<li class="menu header"><span>' + $scope.mitem.Description + '</span></li>');
            } else {
                $element.html('<li class="menu item"><a href="#' + $scope.mitem.routePath + '" id="menu_' + $scope.mitem.Id + '"><span class="menu icon ' + $scope.mitem.iconPath + '">e</span><span>' + $scope.mitem.Description + '</span></a></li>');
            }
        }
    };
});

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/stockin', {
            templateUrl: 'templates/stockin.htm',
            controller: 'stockController'
        })
        .when('/stockin/:stock_id', {
            templateUrl: 'templates/stockin.htm',
            controller: 'stockController'
        })
        .when('/stockout', {
            templateUrl: 'templates/stockout.htm',
            controller: 'stockOutController'
        })
        .when('/stockout/:stock_id', {
            templateUrl: 'templates/stockout.htm',
            controller: 'stockOutController'
        })
        .when('/stockmovs', {
            templateUrl: 'templates/stockmovs.htm',
            controller: 'stockMovController'
        })
        .when('/adminuser', {
            templateUrl: 'templates/users.htm',
            controller: 'usersController'
        })
        .when('/edituser/:userId', {
            templateUrl: 'templates/edituser.htm',
            controller: 'editUserController'
        })
        .when('/bitacora', {
            templateUrl: 'templates/bitacora.htm',
            controller: 'bitacoraController'
        })
        .when('/areas', {
            templateUrl: 'templates/areas.htm',
            controller: 'areasController'
        })
        .when('/articulos', {
            templateUrl: 'templates/articulos.htm',
            controller: 'productosController'
        })
        .when('/categorias', {
            templateUrl: 'templates/categorias.htm',
            controller: 'categoriasController'
        })
        .when('/empleados', {
            templateUrl: 'templates/empleados.htm',
            controller: 'empleadosController'
        })
        .when('/maquinaria', {
            templateUrl: 'templates/maquinaria.htm',
            controller: 'maquinariaController'
        })
        .when('/sistemas', {
            templateUrl: 'templates/sistemas.htm',
            controller: 'sistemasController'
        })
        .when('/logout', {
            templateUrl: 'templates/logout.htm',
            controller: 'logoutController'
        })
        .otherwise({
            redirectTo: '/home'
        });
} ]);
