(function () {
    "use strict";

    function loginController($scope, $location, authService) {
        $scope.loginData = {
            userName: "",
            password: ""
        };
        $scope.message = "";
        $scope.submetido = false;
        $scope.login = function () {
            $scope.submetido = true;
            if ($scope.loginForm.$valid) {
                authService.login($scope.loginData)
                .then(function () {
                    $location.path("/home");
                },
                function (err) {
                    $scope.message = err.error_description;
                });
            }
        };
    };

    loginController.$inject = ["$scope", "$location", "authService"];
    angular.module("sbAdminApp")
        .controller("loginCtrl", loginController);
}());