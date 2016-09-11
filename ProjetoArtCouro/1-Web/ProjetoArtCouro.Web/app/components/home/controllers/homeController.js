(function () {
    "use strict";

    function homeController($scope, $location, authService) {
        $scope.variaveisResultado = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-primary", "drag": true },
            { "description": "JORNADA 2", "name": "V_JX", "classColor": "btn-primary", "drag": true }
        ];
        $scope.variaveisCalculo = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-success", "drag": true },
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-success", "drag": true }
        ];
        $scope.variaveisParametro = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-default", "drag": true },
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-default", "drag": true }
        ];
        $scope.operadores = [
            { "description": "Soma", "name": "+", "classColor": "btn-warning", "drag": true },
            { "description": "Adição", "name": "-", "classColor": "btn-warning", "drag": true },
            { "description": "Multiplicação", "name": "*", "classColor": "btn-warning", "drag": true },
            { "description": "Subtração", "name": "/", "classColor": "btn-warning", "drag": true },
            { "description": "Modulo", "name": "%", "classColor": "btn-warning", "drag": true },
            { "description": "Atribuição", "name": "=", "classColor": "btn-warning", "drag": true }
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