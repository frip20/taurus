app.directive('maquinariaDrop', function ($http, $timeout) {
    this.onSelectedHandler = null;
    return {
        restrict: 'E',
        scope: {
            "id": "@id",
            "selectedScope": "=selectedscope",
            "onSelectedChanged": "&onselchanged"
        },
        //template: '<select id="{{id}}_value" ng-model="selectedScope" class="form-control" ng-options="item.Id as item.fullName for item in items track by item.Id" ng-change="onSelectedChanged()"></select>',
        template: '<select id="{{id}}_value" ng-model="selectedScope" class="form-control" ng-options="item.fullName for item in items track by item.Id" ng-change="onSelectedChanged()"></select>',
        link: function ($scope, element, attrs) {
            $scope.items = [];
            $scope.error = '';

            $http.get('api/maquinaria').success(function (apiData) {
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