(function () {
    "use strict";

    function homeController($scope, DTOptionsBuilder, DTColumnBuilder) {
        $scope.variaveisResultado = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-primary", "type": "R", "drag": true },
            { "description": "JORNADA 2", "name": "V_JX", "classColor": "btn-primary", "type": "R", "drag": true }
        ];
        $scope.variaveisCalculo = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-success", "type": "C", "drag": true },
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-success", "type": "C", "drag": true }
        ];
        $scope.variaveisParametro = [
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-default", "type": "P", "drag": true },
            { "description": "JORNADA 1", "name": "V_JT", "classColor": "btn-default", "type": "P", "drag": true }
        ];
        $scope.operadores = [
            { "description": "Soma", "name": "+", "classColor": "btn-warning", "type": "O", "drag": true },
            { "description": "Adição", "name": "-", "classColor": "btn-warning", "type": "O", "drag": true },
            { "description": "Multiplicação", "name": "*", "classColor": "btn-warning", "type": "O", "drag": true },
            { "description": "Subtração", "name": "/", "classColor": "btn-warning", "type": "O", "drag": true },
            { "description": "Modulo", "name": "%", "classColor": "btn-warning", "type": "O", "drag": true },
            { "description": "Atribuição", "name": "=", "classColor": "btn-warning", "type": "O", "drag": true }
        ];
        $scope.formula = [];
        $scope.optionsResultado = {
            accept: function ($element) {
                var obj = angular.element($element).scope();
                return obj.item.type === "R";
            }
        };
        $scope.optionsCalculo = {
            accept: function ($element) {
                var obj = angular.element($element).scope();
                return obj.item.type === "C";
            }
        };
        $scope.optionsParametro = {
            accept: function ($element) {
                var obj = angular.element($element).scope();
                return obj.item.type === "P";
            }
        };
        $scope.optionsOperacao = {
            accept: function ($element) {
                var obj = angular.element($element).scope();
                return obj.item.type === "O";
            }
        };

        $scope.onDrop = function ($event) {
            $scope.expression = "";
            var obj = angular.element($event.target).scope();
            $scope.dtInstance.DataTable.ngDestroy();
            $scope.dtColumns = [];
            for (var i = 0; i < obj.formula.length; i++) {
                $scope.expression += obj.formula[i].name;
                if (obj.formula[i].type === "R") {
                    $scope.dtColumns.push(DTColumnBuilder.newColumn(obj.formula[i].name).withTitle(obj.formula[i].description));
                }
            }
            $scope.dtInstance = {};
            $scope.dtOptions = DTOptionsBuilder
                .newOptions()
                .withBootstrap()
                .withDisplayLength(15)
                .withOption('bFilter', false)
                .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                    "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>")
                .withLanguageSource("app/shared/json/dataTableLanguage.json");
        };

        $scope.dtColumns = [
            DTColumnBuilder.newColumn("").notVisible()
        ];
        $scope.dtInstance = {};
        $scope.dtOptions = DTOptionsBuilder
            .newOptions()
            .withBootstrap()
            .withDisplayLength(15)
            .withOption('bFilter', false)
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>")
            .withLanguageSource("app/shared/json/dataTableLanguage.json");
    };

    homeController.$inject = ["$scope", "DTOptionsBuilder", "DTColumnBuilder"];
    angular.module("sbAdminApp")
        .controller("homeCtrl", homeController);
}());