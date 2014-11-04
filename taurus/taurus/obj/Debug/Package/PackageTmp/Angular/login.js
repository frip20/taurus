//Login logic
app.controller('logoutController', function ($scope, loginService) {
    loginService.logout();

});

app.controller('loginController', function ($scope, loginService) {
    $scope.user = {};

    $scope.loginUser = function () {
        loginService.login($scope.user, $scope);
    };

});

app.factory('loginService', function ($http, $location, $window, $q, sessionService, alertService) {
    var _factory = {};

    _factory.login = function (data, scope) {
        var promise = $http.post('api/login', data); //send data to user.php
        promise.then(function (apiData) {
            if (apiData.data.Status == 'OK') {
                var user = apiData.data.jData;
                scope.user = user;
                sessionService.set('uid', user.Id);
                $window.location = '/home';
            }
            else {
                alertService.clear();
                alertService.addAlert('danger', apiData.data.jData);
                //scope.messageProvider = apiData.data.jData;
                $location.path('/login');
            }
        });
    };

    _factory.logout = function () {
        var deferred = $q.defer();
        $http.get('api/logout').success(function (apiData) {
            if (apiData.Status == 'OK') {
                sessionService.destroy('uid');
                deferred.resolve(apiData.jData);
                $window.location = '/login';
            } else {
                deferred.reject(0)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    _factory.islogged = function () {
        var deferred = $q.defer();
        $http.get('api/login').success(function (apiData) {
            if (apiData.Status == 'OK') {
                sessionService.set('uid', apiData.jData);
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(0)
            }
        }).error(function () {
            deferred.reject(0);
        });
        return deferred.promise;
    };

    return _factory;

});