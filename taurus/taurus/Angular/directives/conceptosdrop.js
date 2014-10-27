﻿app.directive('conceptosDrop', function ($http) {
    this.onSelectedHandler = null;
    return {
        restrict: 'E',
        scope: {
            "id": "@id",
            "selectedScope": "=selectedscope"
        },
        template: '<select id="{{id}}_value" ng-model="selectedScope" class="form-control" ng-options="item.Id as item.Description for item in items" ></select>',
        link: function ($scope, element, attrs) {
            $scope.items = [];
            $scope.error = '';

            $http.get('api/conceptos').success(function (apiData) {
                if (apiData.Status == 'OK') {
                    $scope.items = apiData.jData;
                } else {
                    $scope.error = apiData.jData;
                }
            }).error(function () {
                deferred.reject('Error on dropdown directive');
            });
        }
    }
});