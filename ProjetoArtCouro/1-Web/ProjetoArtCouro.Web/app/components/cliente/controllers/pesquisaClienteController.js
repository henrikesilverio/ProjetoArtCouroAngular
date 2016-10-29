﻿(function () {
    "use strict";

    function pesquisaClienteCtrl($scope, $compile, pesquisaClienteService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.model = {
            "CodigoCliente": "",
            "Nome": "",
            "CPFCNPJ": "",
            "Email": "",
            "EPessoaFisica": "True"
        };
        $scope.nomeTabela = "Tabela de clientes";
        $scope.dtOptions = DTOptionsBuilder.fromFnPromise(function() {
                return pesquisaClienteService.pesquisaCliente($scope.model);
            })
            .withBootstrap()
            .withDisplayLength(15)
            .withOption('fnRowCallback',
                function (nRow) {
                    $compile(nRow)($scope);
                })
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>")
            .withLanguageSource("app/shared/json/dataTableLanguage.json");
            

        $scope.editar = function(codigoCliente) {
            $scope.dtInstance.reloadData();
        }
        $scope.deletar = function(codigoCliente) {
            $scope.dtInstance.reloadData();
        }

        function actionsHtml(data) {
            return "<button class=\"btn btn-warning\" ng-click=\"editar(" + data.codigo + ")\">" +
                "   <i class=\"fa fa-edit\"></i>" +
                "</button>&nbsp;" +
                "<button class=\"btn btn-danger\" ng-click=\"deletar(" + data.codigo + ")\">" +
                "   <i class=\"fa fa-trash-o\"></i>" +
                "</button>";
        }

        function cpfOrCnpj(data) {
            return data.cpf || data.cnpj;
        }

        function email(data) {
            return data.meioComunicacao.email || "N/I";
        }

        $scope.dtColumns = [
            DTColumnBuilder.newColumn("codigo").withTitle("Código").withOption("width", "10%"),
            DTColumnBuilder.newColumn("nome").withTitle("Nome").withOption("width", "35%"),
            DTColumnBuilder.newColumn(cpfOrCnpj).withTitle("CPF/CNPJ").withOption("width", "20%"),
            DTColumnBuilder.newColumn(email).withTitle("Email").withOption("width", "20%"),
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").withOption("width", "15%").notSortable()
        ];
        $scope.dtInstance = {}
        $scope.pesquisar = function () {
            $scope.dtInstance.reloadData();
        };
    }

    pesquisaClienteCtrl.$inject = ["$scope", "$compile", "pesquisaClienteService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("pesquisaClienteCtrl", pesquisaClienteCtrl);
})();