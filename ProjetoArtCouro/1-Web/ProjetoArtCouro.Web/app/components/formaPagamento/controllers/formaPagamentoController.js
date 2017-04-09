(function () {
    "use strict";

    function actionsHtml(data, type, full, meta) {
        return "<div class=\"btn-group\" role=\"toolbar\">" +
            "<button type=\"button\" class=\"btn btn-warning\" ng-click=\"editar(" + meta.row + ")\">" +
            "<i class=\"fa fa-edit\"></i>" +
            "</button>" +
            "<button type=\"button\" class=\"btn btn-danger\" ng-click=\"excluir(" + data.formaPagamentoCodigo + ")\">" +
            "<i class=\"fa fa-trash-o\"></i>" +
            "</button>" +
            "</div>";
    }

    function ativo(data) {
        return data.ativo ? "Sim" : "Não";
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

    function modalEditarFormaPagamentoCtrl($scope, $uibModalInstance, model, editar) {
        $scope.model = model;
        $scope.confirmModalModel = {
            titulo: "Editar forma de pagamento",
            salvar: function () {
                editar($scope.model);
                $uibModalInstance.close();
            },
            cancelar: function () {
                $uibModalInstance.close();
            }
        };
    }

    function formaPagamentoCtrl($scope, $compile, $uibModal, formaPagamentoService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.model = {
            "Descricao": "",
            "Ativo": undefined
        };

        $scope.nomeTabela = "Tabela de forma de pagamento";

        $scope.dtOptions = DTOptionsBuilder.fromFnPromise(function () {
            return formaPagamentoService.obterListaFormaPagamento($scope.model);
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

        $scope.dtColumns = [
            DTColumnBuilder.newColumn("formaPagamentoCodigo").withTitle("Código").withOption("width", "10%"),
            DTColumnBuilder.newColumn("descricao").withTitle("Descrição").withOption("width", "55%"),
            DTColumnBuilder.newColumn(ativo).withTitle("Ativo").withOption("width", "20%"),
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").withOption("width", "15%").withOption("class", "text-center").notSortable()
        ];
        $scope.dtInstance = {}

        $scope.adicionar = function (formulario) {
            if (formulario.$valid) {
                var model = angular.copy($scope.model);
                formaPagamentoService.criarFormaPagamento(model)
                    .then(function () {
                        $scope.model.Descricao = "";
                        $scope.model.Ativo = undefined;
                        $scope.dtInstance.reloadData();
                        formulario.$setPristine();
                    });
            }
        };

        $scope.editarFormaPagamento = function (model) {
            var modelCopy = angular.copy(model);
            formaPagamentoService.editarFormaPagamento(modelCopy)
                .then(function () {
                    $scope.dtInstance.reloadData();
                });
        }

        $scope.editar = function (indiceLinhaSelecionada) {
            var formaPagamento = $scope.dtInstance.dataTable.fnGetData(indiceLinhaSelecionada);
            var model = {
                "FormaPagamentoCodigo": formaPagamento.formaPagamentoCodigo,
                "Descricao": formaPagamento.descricao,
                "Ativo": formaPagamento.ativo
            };
            $uibModal.open({
                templateUrl: "app/shared/modal/modalFormaPagamento.html",
                controller: modalEditarFormaPagamentoCtrl,
                size: "lg",
                resolve: {
                    model: model,
                    editar: function () {
                        return $scope.editarFormaPagamento;
                    }
                }
            });
        };

        $scope.excluirFormaPagamento = function () {
            formaPagamentoService.excluirFormaPagamento($scope.formaPagamentoCodigo)
                .then(function () {
                    $scope.dtInstance.reloadData();
                });
        }

        $scope.excluir = function (formaPagamentoCodigo) {
            $scope.formaPagamentoCodigo = formaPagamentoCodigo;
            $uibModal.open({
                templateUrl: "app/shared/modal/confirm.html",
                controller: modalExcluirCtrl,
                resolve: {
                    excluir: function () {
                        return $scope.excluirFormaPagamento;
                    }
                }
            });
        }
    };

    formaPagamentoCtrl.$inject = ["$scope", "$compile", "$uibModal", "formaPagamentoService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("formaPagamentoCtrl", formaPagamentoCtrl);
})();