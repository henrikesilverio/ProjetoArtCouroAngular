(function () {
    "use strict";

    function homeController($scope, $compile, $q, DTOptionsBuilder, DTColumnBuilder) {
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

        };

        function actionsHtml() {
            return "<input type=\"text\" " +
                "class=\"form-control\" " +
                "uib-datepicker-popup=\"dd/mm/yyyy\" " +
                "show-button-bar=\"false\" " +
                "ng-model=\"dt2\" " +
                "is-open=\"abrir\" " +
                "datepicker-options=\"dateOptions\" " +
                "ng-required=\"true\" " +
                "ng-change=\"teste(this)\" " +
                "ng-click=\"abrir=true\"/>";
        }

        $scope.dtColumns = [
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").notSortable()
        ];
        $scope.dtInstance = {};
        $scope.dtOptions = DTOptionsBuilder.fromFnPromise(function() {
                var defer = $q.defer();
                defer.resolve([{ "nome": "" }]);
                return defer.promise;
            })
            .withBootstrap()
            .withDisplayLength(15)
            .withOption("bFilter", false)
            .withOption('fnRowCallback',
                function(nRow) {
                    $compile(nRow)($scope);
                })
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>")
            .withLanguageSource("app/shared/json/dataTableLanguage.json");

        $scope.dateOptions = {
            formatYear: "yyyy",
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1,
            showWeeks: false
        };

        $scope.teste = function(obj) {
            console.log($scope.dt2);
        }
    };

    homeController.$inject = ["$scope", "$compile", "$q", "DTOptionsBuilder", "DTColumnBuilder"];
    angular.module("sbAdminApp")
        .controller("homeCtrl", homeController);
}());