app.controller('usersController', function ($scope, $modal, $http, $q, $location) {
    $scope.users = [];
    $scope.errorForm = '';

    $scope.init = function () {
        $scope.getAllUsers()
            .then(function (data) {
                $scope.users = data;
            }, function (data) {
                $scope.errorForm = data;
            });
    };

    $scope.getAllUsers = function () {
        var deferred = $q.defer();
        $http.get('api/users').success(function (apiData) {
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


});