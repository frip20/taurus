app.factory('compacService', function ($http, $q) {
    var _factory = {};

    _factory.addPoliza = function () {
        var deferred = $q.defer();
        $http.post('api/compac', { Area: area, Action: action }).success(function (apiData) {
            if (apiData.Status == 'OK') {
                deferred.resolve(apiData.jData);
            } else {
                deferred.reject(apiData.jData)
            }
        }).error(function () {
            deferred.reject('Error on compacService');
        });
        return deferred.promise;
    };

    return _factory;
});