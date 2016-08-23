(function () {
    "use strict";

    function pesquisaClienteCtrl($scope, pesquisaClienteService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.nomeTabela = "Tabela de clientes";
        $scope.dtOptions = DTOptionsBuilder
            .newOptions()
            .withBootstrap()
            .withDisplayLength(15)
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>");
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("Codigo").withTitle("Código"),
            DTColumnBuilder.newColumn("Nome").withTitle("Nome"),
            DTColumnBuilder.newColumn("CPFCNPJ").withTitle("CPF/CNPJ"),
            DTColumnBuilder.newColumn("Email").withTitle("Email"),
            DTColumnBuilder.newColumn(null).withTitle("Ações").notSortable()
        ];
    }

    pesquisaClienteCtrl.$inject = ["$scope", "pesquisaClienteService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("pesquisaClienteCtrl", pesquisaClienteCtrl);
})();