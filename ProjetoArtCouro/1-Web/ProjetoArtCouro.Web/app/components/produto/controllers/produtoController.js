(function () {
    "use strict";

    function actionsHtml(data, type, full, meta) {
        return "<div class=\"btn-group\" role=\"toolbar\">" +
            "<button type=\"button\" class=\"btn btn-warning\" ng-click=\"editar(" + meta.row + ")\">" +
            "<i class=\"fa fa-edit\"></i>" +
            "</button>" +
            "<button type=\"button\" class=\"btn btn-danger\" ng-click=\"excluir(" + data.produtoCodigo + ")\">" +
            "<i class=\"fa fa-trash-o\"></i>" +
            "</button>" +
            "</div>";
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

    function modalEditarProdutoCtrl($scope, $uibModalInstance, model, editar) {
        $scope.model = model;
        $scope.confirmModalModel = {
            titulo: "Editar produto",
            salvar: function () {
                editar($scope.model);
                $uibModalInstance.close();
            },
            cancelar: function () {
                $uibModalInstance.close();
            }
        };
    }

    function produtoCtrl($scope, $compile, $uibModal, produtoService, DTOptionsBuilder, DTColumnBuilder) {
        $scope.model = {
            "Descricao": "",
            "Unidade": "",
            "Unidades": [],
            "PrecoCusto": "",
            "PrecoVenda": ""
        };

        $scope.nomeTabela = "Tabela de produtos";

        $scope.dtOptions = DTOptionsBuilder.fromFnPromise(function () {
            return produtoService.obterListaProduto($scope.model);
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
            DTColumnBuilder.newColumn("produtoCodigo").withTitle("Código").withOption("width", "10%"),
            DTColumnBuilder.newColumn("descricao").withTitle("Descrição").withOption("width", "35%"),
            DTColumnBuilder.newColumn("precoCusto").withTitle("Preço custo").withOption("width", "20%"),
            DTColumnBuilder.newColumn("precoVenda").withTitle("Preço venda").withOption("width", "20%"),
            DTColumnBuilder.newColumn(actionsHtml).withTitle("Ações").withOption("width", "15%").withOption("class", "text-center").notSortable()
        ];
        $scope.dtInstance = {}

        produtoService.obterListaUnidade().then(function (data) {
            $scope.model.Unidades = data;
            $scope.model.Unidades.unshift({
                "codigo": "",
                "nome": "Selecione"
            });
        });

        $scope.adicionar = function (formulario) {
            if (formulario.$valid) {
                var model = angular.copy($scope.model);
                var unidade = _.find($scope.model.Unidades, function (item) {
                    return item.codigo.toString() === $scope.model.Unidade;
                });
                model["UnidadeId"] = unidade.codigo;
                model["UnidadeNome"] = unidade.nome;
                produtoService.criarProduto(model)
                    .then(function () {
                        $scope.model.Descricao = "";
                        $scope.model.Unidade = "";
                        $scope.model.PrecoCusto = "";
                        $scope.model.PrecoVenda = "";
                        $scope.dtInstance.reloadData();
                        formulario.$setPristine();
                    });
            }
        };

        $scope.editarProduto = function (model) {
            var modelCopy = angular.copy(model);
            var unidade = _.find(model.Unidades, function (item) {
                return item.codigo.toString() === model.Unidade;
            });
            modelCopy["UnidadeId"] = unidade.codigo;
            modelCopy["UnidadeNome"] = unidade.nome;
            produtoService.editarProduto(modelCopy)
                .then(function () {
                    $scope.dtInstance.reloadData();
                });
        }

        $scope.editar = function (indiceLinhaSelecionada) {
            var produto = $scope.dtInstance.dataTable.fnGetData(indiceLinhaSelecionada);
            var model = {
                "ProdutoCodigo": produto.produtoCodigo,
                "Descricao": produto.descricao,
                "Unidade": produto.unidadeId.toString(),
                "Unidades": $scope.model.Unidades,
                "PrecoCusto": produto.precoCusto.replace("R$ ", ""),
                "PrecoVenda": produto.precoVenda.replace("R$ ", "")
            };
            $uibModal.open({
                templateUrl: "app/shared/modal/modalProduto.html",
                controller: modalEditarProdutoCtrl,
                size: "lg",
                resolve: {
                    model: model,
                    editar: function () {
                        return $scope.editarProduto;
                    }
                }
            });
        };

        $scope.excluirProduto = function () {
            produtoService.excluirProduto($scope.codigoProduto)
                .then(function () {
                    $scope.dtInstance.reloadData();
                });
        }

        $scope.excluir = function (codigoProduto) {
            $scope.codigoProduto = codigoProduto;
            $uibModal.open({
                templateUrl: "app/shared/modal/confirm.html",
                controller: modalExcluirCtrl,
                resolve: {
                    excluir: function () {
                        return $scope.excluirProduto;
                    }
                }
            });
        }
    };

    produtoCtrl.$inject = ["$scope", "$compile", "$uibModal", "produtoService", "DTOptionsBuilder", "DTColumnBuilder"];

    angular.module("sbAdminApp")
        .controller("produtoCtrl", produtoCtrl);
})();
