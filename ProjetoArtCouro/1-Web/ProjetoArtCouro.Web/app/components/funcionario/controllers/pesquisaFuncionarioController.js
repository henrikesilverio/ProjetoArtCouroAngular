(function () {
    "use strict";

    function actionsHtml(data) {
        return "<div class=\"btn-group\" role=\"toolbar\">" +
            "<button type=\"button\" class=\"btn btn-warning\" ng-click=\"editar(" + data.codigo + ")\">" +
            "<i class=\"fa fa-edit\"></i>" +
            "</button>" +
            "<button type=\"button\" class=\"btn btn-danger\" ng-click=\"excluir(" + data.codigo + ")\">" +
            "<i class=\"fa fa-trash-o\"></i>" +
            "</button>" +
            "</div>";
    }

    function cpfOrCnpj(data) {
        return data.cpf || data.cnpj;
    }

    function email(data) {
        return data.meioComunicacao.email || "N/I";
    }

    function nome(data) {
        return data.nome || data.razaoSocial;
    }

    function modalExcluirCtrl($scope, $uibModalInstance, excluir) {
        $scope.confirmModalModel = {
            title: "Atenção!",
            text: "Tem certeza que deseja excluir ?",
            sim: function () {
                excluir();
                $uibModalInstance.close();
            },
            nao: function () {
                $uibModalInstance.close();
            }
        };
    }

    function pesquisaFuncionarioCtrl($scope, $compile, $state, $uibModal, toastr, pesquisaFuncionarioService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.model = {
            "CodigoFuncionario": "",
            "Nome": "",
            "CPFCNPJ": "",
            "CPF": "",
            "CNPJ": "",
            "Email": "",
            "EPessoaFisica": true
        };
        $scope.nomeTabela = "Tabela de Funcionarios";
        $scope.dtOptions = DTOptionsBuilder.fromFnPromise(function () {
            $scope.model.CPFCNPJ = $scope.model.EPessoaFisica ? $scope.model.CPF : $scope.model.CNPJ;
            return pesquisaFuncionarioService.pesquisaFuncionario($scope.model);
        })
            .withBootstrap()
            .withDisplayLength(15)
            .withOption("createdRow",
                function (nRow) {
                    $compile(nRow)($scope);
                })
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>")
            .withLanguageSource("app/shared/json/dataTableLanguage.json");

        $scope.liparCamposCPFCNPJ = function () {
            $scope.model.CPF = "";
            $scope.model.CNPJ = "";
        }

        $scope.editar = function (codigoFuncionario) {
            if (codigoFuncionario) {
                $state.go("cadastro.editarFuncionario", { codigoFuncionario: codigoFuncionario });
            }
        }

        $scope.excluirFuncionario = function () {
            pesquisaFuncionarioService.excluirFuncionario($scope.codigoFuncionario)
                .then(function () {
                    $scope.dtInstance.reloadData();
                });
        }

        $scope.excluir = function (codigoFuncionario) {
            $scope.codigoFuncionario = codigoFuncionario;
            $uibModal.open({
                templateUrl: "app/shared/modal/confirm.html",
                controller: modalExcluirCtrl,
                resolve: {
                    excluir: function () {
                        return $scope.excluirFuncionario;
                    }
                }
            });
        }

        $scope.dtColumns = [
            DTColumnBuilder.newColumn("codigo").withTitle("Código").withOption("width", "10%"),
            DTColumnBuilder.newColumn(nome).withTitle("Nome").withOption("width", "35%"),
            DTColumnBuilder.newColumn(cpfOrCnpj).withTitle("CPF/CNPJ").withOption("width", "20%"),
            DTColumnBuilder.newColumn(email).withTitle("Email").withOption("width", "20%"),
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").withOption("width", "15%").withOption("class", "text-center").notSortable()
        ];
        $scope.dtInstance = {}
        $scope.pesquisar = function () {
            $scope.dtInstance.reloadData();
        };
    }

    pesquisaFuncionarioCtrl.$inject = ["$scope", "$compile", "$state", "$uibModal", "toastr", "pesquisaFuncionarioService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("pesquisaFuncionarioCtrl", pesquisaFuncionarioCtrl);
})();