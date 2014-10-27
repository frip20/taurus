app.controller('bitacoraController', function ($scope) {
    $scope.newItem = { createDate: new Date() };
    $scope.bitacora = [];

    $scope.changeMaquina = function (obj) {
        console.log(obj);
        console.log($scope.newItem);
    };

});