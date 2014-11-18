app.directive('anguPages', [function () {
    return {
        restrict: 'E',
        scope: {
            "id": "@id",
            "placeholder": "@placeholder",
            "listItems": "=listitems",
            "filterOption": "=filteroption",
            "post": "@post",
            "url": "@url"
        },
        template: '<div ng-show="totalItems>0" class="paginator" id="{{id}}_paginator"><div>Total items: {{ totalItems }}</div> <div ng-show="totalPages>1">Paginas: {{ currentPage }} of {{ totalPages }}</div> <div ng-show="totalPages>1"><input ng-click="currentPage=1" ng-disabled="totalPages<=1 || currentPage<=1" class="navbtn first" type="button" value="">  <input ng-click="currentPage=currentPage-1" ng-disabled="totalPages<=1 || currentPage<=1" type="button" class="navbtn previous" value="">  <input ng-click="currentPage=currentPage+1" ng-disabled="totalPages <= 1 || currentPage == totalPages" class="navbtn next" type="button" value="">  <input ng-disabled="totalPages <= 1 || currentPage == totalPages" type="button" class="navbtn last" value="" ng-click="currentPage=totalPages"></div><div>',
        controller: function ($scope, $element, $attrs, $q, $http) {
            $scope.search = function () {
                var _promise;
                if ($scope.post) {
                    _promise = $scope.postItems();
                } else {
                    _promise = $scope.getItems();
                }

                _promise
                    .then(function (data) {
                        $scope.data = angular.copy(data);

                        $scope.totalItems = data.length;
                        $scope.totalPages = Math.ceil(data.length / $scope.rowsByPage);

                        if (data.length > $scope.rowsByPage) {
                            $scope.listItems = data.splice(0, $scope.rowsByPage);
                        } else {
                            $scope.listItems = data;
                        }

                    }, function (data) {
                        $scope.errorForm = data;
                    });
                };

            $scope.getItems = function () {
                var deferred = $q.defer();
                $http.get($scope.url).success(function (apiData) {
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

            $scope.postItems = function () {
                var deferred = $q.defer();
                $http.post($scope.url, $scope.filterOption).success(function (apiData) {
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
        },
        link: function ($scope, element, attrs) {
            $scope.totalPages = 0;
            $scope.totalItems = 0;
            $scope.rowsByPage = 50;
            $scope.data = [];
            $scope.currentPage = 1;
            $scope.errorForm = '';

            if ($scope.post == null || $scope.post === 'undefined') {
                $scope.post = false;
            }

            if ($scope.filterOption) {
                $scope.$watch('filterOption', function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        $scope.search();
                    }
                }, true);
            }

            if ($scope.url) {
                $scope.search();
            };

            

            $scope.$watch('currentPage', function (newValue, oldValue) {
                if (newValue != oldValue) {
                    var _index = (($scope.currentPage - 1) * $scope.rowsByPage);
                    var _totalInArray = _index + $scope.rowsByPage;
                    $scope.listItems = $scope.data.filter(function (value, index) {
                        return (index >= (($scope.currentPage - 1) * $scope.rowsByPage) && index <= _totalInArray);
                    });
                }
            }, true);
        }
    }
} ]);