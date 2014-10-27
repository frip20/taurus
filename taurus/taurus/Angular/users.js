app.factory('userService', function ($http, $q) {
    var _factory = {};

    _factory.postUserData = function (user, action) {
        var deferred = $q.defer();
        $http.post('api/users', { User: user, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on service');
        });
        return deferred.promise;
    };

    _factory.getAllUsers = function () {
        var deferred = $q.defer();
        $http.get('api/users').success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on service');
        });
        return deferred.promise;
    };

    return _factory;

});


app.controller('usersController', function ($scope, $http, $q, $location, userService) {
    $scope.user = { userRol: {} };
    $scope.users = [];
    $scope.errorForm = '';

    $scope.init = function () {
        userService.getAllUsers()
            .then(function (data) {
                $scope.users = data;
            }, function (data) {
                $scope.errorForm = data;
            });
    };

    $scope.lockUser = function (id, index, lock) {
        $scope.user.Id = id;
        userService.postUserData($scope.user, (lock ? 4 : 5))
            .then(function (data) {
                $scope.users[index] = data;
            }, function (data) {
                $scope.errorForm = data;
            });
    };


    $scope.addUser = function () {
        $location.path('/edituser/0');
    };

    $scope.editUser = function (userId) {
        $location.path('/edituser/' + userId);
    };

});


app.controller('editUserController', function ($scope, $http, $q, $location, $routeParams, userService) {
    $scope.user = { userRol: {} };
    $scope.roles = [];
    $scope.errorForm = '';

    $http.get('api/rol').success(function (apiData) {
        if (apiData.Status == 'OK') {
            $scope.roles = apiData.jData;
        } else {
            $scope.roles = [];
        }
    }).error(function () {
        $scope.errorForm = 'Error loading the rol';
    });

    $scope.init = function () {
        $scope.user.Id = $routeParams.userId;

        if ($scope.user.Id > 0) {
            userService.postUserData($scope.user, 6)
            .then(function (data) {
                $scope.user = data;
            }, function (data) {
                $scope.errorForm = data;
            });
        }
    };

    $scope.validForm = function () {
        $scope.errorForm = '';
        if ($scope.user.userName == null) {
            $scope.errorForm = 'El nombre de usuario es requerido';
            return false;
        }

        if ($scope.user.Password == null) {
            $scope.errorForm = 'La contraseña es requerida';
            return false;
        }

        if ($scope.user.userRol.Id == null) {
            $scope.errorForm = 'El rol es requerido';
            return false;
        }
        return true;
    };

    $scope.Cancel = function () {
        $location.path('/adminuser');
    };

    $scope.addUser = function () {
        if ($scope.validForm()) {
            console.log($scope.user);
            var action = ($scope.user.Id == null || $scope.user.Id <= 0) ? 1 : 3; //1:ADD 3:UPDATE
            userService.postUserData($scope.user, action)
            .then(function (data) {
                $scope.Cancel();
            }, function (data) {
                $scope.errorForm = data;
            });
        }
    };
});