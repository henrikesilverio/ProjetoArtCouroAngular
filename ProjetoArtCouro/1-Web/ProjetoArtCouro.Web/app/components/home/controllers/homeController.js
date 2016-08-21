(function () {
    "use strict";

    function homeController($scope, $location, authService) {
        
    };

    homeController.$inject = ["$scope", "$location", "authService"];
    angular.module("sbAdminApp")
        .controller("homeCtrl", homeController);
}());