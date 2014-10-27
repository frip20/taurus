app.factory('departmentService', function ($http, $q) {
    var _factory = {};

    _factory.getAllDepartments = function () {
        var deferred = $q.defer();
        $http.get('api/departamento').success(function (apiData) {
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