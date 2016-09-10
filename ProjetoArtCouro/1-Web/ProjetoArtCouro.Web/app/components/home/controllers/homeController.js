(function () {
    "use strict";

    function homeController($scope, $location, authService) {
        $scope.list1 = [
            { 'title': "Item 1", 'drag': true, 'classColor': "btn-primary" },
            { 'title': "Item 2", 'drag': true, 'classColor': "btn-primary" }
        ];
        $scope.list2 = [
            { 'title': "Item 1", 'drag': true, 'classColor': "btn-success" },
            { 'title': "Item 2", 'drag': true, 'classColor': "btn-success" }
        ];
        $scope.list3 = [
            { 'title': "Item 1", 'drag': true, 'classColor': "btn-info" },
            { 'title': "Item 2", 'drag': true, 'classColor': "btn-info" }
        ];
        $scope.list4 = [
            { 'title': "+", 'drag': true, 'classColor': "btn-default" },
            { 'title': "-", 'drag': true, 'classColor': "btn-default" },
            { 'title': "*", 'drag': true, 'classColor': "btn-default" },
            { 'title': "/", 'drag': true, 'classColor': "btn-default" },
            { 'title': "%", 'drag': true, 'classColor': "btn-default" }
        ];
        $scope.formula = [];
        // Limit items to be dropped in list1
        $scope.optionsList1 = {
            accept: function (dragEl) {
                if ($scope.list1.length >= 2) {
                    return false;
                } else {
                    return true;
                }
            }
        };
    };

    homeController.$inject = ["$scope", "$location", "authService"];
    angular.module("sbAdminApp")
        .controller("homeCtrl", homeController);
}());