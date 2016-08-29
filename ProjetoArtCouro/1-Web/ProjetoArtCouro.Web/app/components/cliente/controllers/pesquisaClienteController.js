(function () {
    "use strict";

    function pesquisaClienteCtrl($scope, pesquisaClienteService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.model = {
            "CodigoCliente": "",
            "Nome": "",
            "CPFCNPJ": "",
            "Email": "",
            "EPessoaFisica": "True"
        };
        $scope.nomeTabela = "Tabela de clientes";
        $scope.dtOptions = DTOptionsBuilder
            .newOptions()
            .withBootstrap()
            .withDisplayLength(15)
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
            return "<button class=\"btn btn-warning\" ng-click=\"editar(" + data.CodigoCliente + ")\">" +
                "   <i class=\"fa fa-edit\"></i>" +
                "</button>&nbsp;" +
                "<button class=\"btn btn-danger\" ng-click=\"deletar(" + data.CodigoCliente + ")\">" +
                "   <i class=\"fa fa-trash-o\"></i>" +
                "</button>";
        }

        $scope.dtColumns = [
            DTColumnBuilder.newColumn("CodigoCliente").withTitle("Código"),
            DTColumnBuilder.newColumn("Nome").withTitle("Nome"),
            DTColumnBuilder.newColumn("CPFCNPJ").withTitle("CPF/CNPJ"),
            DTColumnBuilder.newColumn("Email").withTitle("Email"),
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").notSortable()
        ];
        $scope.dtInstance = {}
        $scope.pesquisar = function () {
            pesquisaClienteService.pesquisaCliente($scope.model)
                .then(function (data) {
                    $scope.dtInstance = data.ObjetoRetorno;
                },
                function(err) {
                    $scope.message = err.Message;
                });
        };
    }

    pesquisaClienteCtrl.$inject = ["$scope", "pesquisaClienteService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("pesquisaClienteCtrl", pesquisaClienteCtrl);
})();