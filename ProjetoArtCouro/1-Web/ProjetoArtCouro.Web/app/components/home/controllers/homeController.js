(function () {
    "use strict";

    function homeController($scope, $compile, $q, DTOptionsBuilder, DTColumnBuilder) {
        $scope.operadores = [
            { "id": "", "description": "Selecione", "name": "+", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 1, "description": "Soma", "name": "+", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 2, "description": "Adição", "name": "-", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 3, "description": "Multiplicação", "name": "*", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 4, "description": "Subtração", "name": "/", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 5, "description": "Modulo", "name": "%", "classColor": "btn-warning", "type": "O", "drag": true },
            { "id": 6, "description": "Atribuição", "name": "=", "classColor": "btn-warning", "type": "O", "drag": true }
        ];
        function dataHtml(data, type, full, meta) {
            return "<input type=\"text\" " +
                "class=\"form-control form-control-table\" " +
                "uib-datepicker-popup=\"dd/MM/yyyy\" " +
                "show-button-bar=\"false\" " +
                "ng-model=\"data\" " +
                "is-open=\"abrir\" " +
                "datepicker-options=\"dateOptions\" " +
                "ng-required=\"true\" " +
                "ng-change=\"dataChange(" + meta.row + ")\" " +
                "ng-click=\"abrir=true\"/>";
        }

        function selecaoHtml(data, type, full, meta) {
            return "<select class=\"form-control form-control-table\" " +
                "id=\"selecao\" " +
                "name=\"selecao\" " +
                "ng-model=\"selecao\" " +
                "ng-change=\"selecaoChange(" + meta.row + ")\" " +
                "ng-init=\"selecao = operadores[0]\"" +
                "ng-options=\"option.description for option in operadores track by option.id\">" +
                "</select>";
        }

        $scope.dtColumns = [
            DTColumnBuilder.newColumn(dataHtml).withTitle("Data").notSortable(),
            DTColumnBuilder.newColumn(selecaoHtml).withTitle("Lista").notSortable()
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
            startingDay: 1,
            showWeeks: false
        };

        $scope.dataChange = function (obj) {
            console.log(Date.parse($scope.data));
        }

        $scope.selecaoChange = function (obj) {
            console.log($scope.selecao);
        }
    };

    homeController.$inject = ["$scope", "$compile", "$q", "DTOptionsBuilder", "DTColumnBuilder"];
    angular.module("sbAdminApp")
        .controller("homeCtrl", homeController);
}());