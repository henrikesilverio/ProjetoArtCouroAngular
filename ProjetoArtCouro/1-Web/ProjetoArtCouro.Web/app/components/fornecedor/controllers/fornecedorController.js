﻿(function () {
    "use strict";

    function carregarModel($scope, fornecedorService) {
        $scope.model = {
            "CodigoFornecedor": "",
            "Nome": "",
            "CPF": "",
            "RG": "",
            "EPessoaFisica": true,
            "EstadoCivil": "",
            "EstadosCivis": [],
            "Endereco": "",
            "Enderecos": [
                {
                    "id": "",
                    "nome": "Selecione"
                },
                {
                    "id": -1,
                    "nome": "Novo"
                }
            ],
            "Telefone": "",
            "NovoTelefone": "",
            "Telefones": [
                {
                    "id": "",
                    "nome": "Selecione"
                },
                {
                    "id": -1,
                    "nome": "Novo"
                }
            ],
            "Celular": "",
            "NovoCelular": "",
            "Celulares": [
                {
                    "id": "",
                    "nome": "Selecione"
                },
                {
                    "id": -1,
                    "nome": "Novo"
                }
            ],
            "Email": "",
            "NovoEmail": "",
            "Emails": [
                {
                    "id": "",
                    "nome": "Selecione"
                },
                {
                    "id": -1,
                    "nome": "Novo"
                }
            ],
            "Logradouro": "",
            "Numero": "",
            "Bairro": "",
            "Complemento": "",
            "Cidade": "",
            "CEP": "",
            "Estado": "",
            "Estados": []
        };

        fornecedorService.obterListaEstado().then(function (data) {
            $scope.model.Estados = data;
            $scope.model.Estados.unshift({
                "codigo": "",
                "nome": "Selecione"
            });
        });

        fornecedorService.obterListaEstadoCivil().then(function (data) {
            $scope.model.EstadosCivis = data;
            $scope.model.EstadosCivis.unshift({
                "codigo": "",
                "nome": "Selecione"
            });
        });
    }

    function preencherEnderecosMeiosDeComunicacao($scope, enderecos, meioComunicacao) {
        var mapas = [
            { "model": "Telefones", "data": "telefones" },
            { "model": "Celulares", "data": "celulares" },
            { "model": "Emails", "data": "emails" },
            { "model": "Enderecos", "data": "enderecos" }
        ];
        _.each(mapas, function (mapeamento) {
            if (meioComunicacao[mapeamento.data]) {
                $scope.model[mapeamento.model] = _.map(meioComunicacao[mapeamento.data], function (item) {
                    return {
                        "id": item.codigo,
                        "nome": item.nome
                    };
                });
            }
            if (enderecos && mapeamento.model === "Enderecos") {
                $scope.model[mapeamento.model] = _.map(enderecos, function (endereco) {
                    return {
                        "id": endereco.enderecoId,
                        "nome": "Logradouro: " + endereco.logradouro + ", N: " + endereco.numero + ", Cidade: " + endereco.cidade + ", CEP: " + endereco.cep
                    };
                });
            }
            $scope.model[mapeamento.model].unshift({
                "codigo": "",
                "nome": "Selecione"
            });
            $scope.model[mapeamento.model].push({
                "id": -1,
                "nome": "Novo"
            });
        });
    }

    function eventos($scope) {
        $scope.limparCamposTipoPessoa = function () {
            $scope.model.Nome = "";
            $scope.model.CPF = "";
            $scope.model.CNPJ = "";
            $scope.model.Contato = "";
            $scope.model.RG = "";
            $scope.model.Sexo = "";
            $scope.model.EstadoCivil = "";
        }

        $scope.limparCamposEndereco = function () {
            $scope.model.Logradouro = "";
            $scope.model.Numero = "";
            $scope.model.Bairro = "";
            $scope.model.Complemento = "";
            $scope.model.Cidade = "";
            $scope.model.CEP = "";
            $scope.model.Estado = "";
        }

        $scope.limparCampoNovoTelefone = function () {
            $scope.model.NovoTelefone = "";
        }

        $scope.limparCampoNovoCelular = function () {
            $scope.model.NovoCelular = "";
        }

        $scope.limparCampoEmail = function () {
            $scope.model.NovoEmail = "";
        }
    }

    function novoFornecedorCtrl($scope, $state, fornecedorService, tipoPapelPessoaEnum) {
        carregarModel($scope, fornecedorService);
        $scope.ehEdicao = false;
        $scope.nomeCabecalho = "Novo Fornecedor";
        eventos($scope);
        $scope.salvar = function (valido) {
            if (valido) {
                var model = angular.copy($scope.model);
                model["EstadoCivilId"] = model.EstadoCivil;
                model["PapelPessoa"] = tipoPapelPessoaEnum.FORNECEDOR;
                model["Endereco"] = {
                    "EnderecoId": model.Endereco,
                    "Logradouro": model.Logradouro,
                    "Numero": model.Numero,
                    "Bairro": model.Bairro,
                    "Complemento": model.Complemento,
                    "Cidade": model.Cidade,
                    "CEP": model.CEP,
                    "UFId": model.Estado
                };
                model["Enderecos"] = null;
                model["MeioComunicacao"] = {
                    "TelefoneId": model.Telefone,
                    "Telefone": model.NovoTelefone,
                    "CelularId": model.Celular,
                    "Celular": model.NovoCelular,
                    "EmailId": model.Email,
                    "Email": model.NovoEmail
                }
                fornecedorService.criarFornecedor(model)
                    .then(function () {
                        $state.go("cadastro.pesquisaFornecedor");
                    });
            }
        }
    }

    function editarFornecedorCtrl($scope, $state, $stateParams, fornecedorService, tipoPapelPessoaEnum) {
        carregarModel($scope, fornecedorService);
        fornecedorService.obterFornecedorPorCodigo($stateParams.codigoFornecedor).then(function (data) {
            $scope.model.EPessoaFisica = data.ePessoaFisica;
            $scope.model.CodigoFornecedor = data.codigo;
            $scope.model.CNPJ = data.cnpj;
            $scope.model.Contato = data.contato;
            $scope.model.RazaoSocial = data.razaoSocial;
            $scope.model.Nome = data.nome;
            $scope.model.CPF = data.cpf;
            $scope.model.RG = data.rg;
            $scope.model.Sexo = data.sexo;
            $scope.model.EstadoCivil = data.ePessoaFisica ? data.estadoCivilId.toString() : "";
            $scope.model.Endereco = data.endereco.enderecoId.toString();
            $scope.model.Telefone = data.meioComunicacao.telefoneId.toString();
            $scope.model.Enderecos = [];
            $scope.model.Telefones = [];
            $scope.model.Celulares = [];
            $scope.model.Emails = [];
            preencherEnderecosMeiosDeComunicacao($scope, data.enderecos, data.meioComunicacao);

        });
        $scope.ehEdicao = true;
        $scope.nomeCabecalho = "Editar Fornecedor";
        eventos($scope);
        $scope.salvar = function (valido) {
            if (valido) {
                var model = angular.copy($scope.model);
                model["EstadoCivilId"] = model.EstadoCivil;
                model["PapelPessoa"] = tipoPapelPessoaEnum.FORNECEDOR;
                model["Endereco"] = {
                    "EnderecoId": model.Endereco,
                    "Logradouro": model.Logradouro,
                    "Numero": model.Numero,
                    "Bairro": model.Bairro,
                    "Complemento": model.Complemento,
                    "Cidade": model.Cidade,
                    "CEP": model.CEP,
                    "UFId": model.Estado
                };
                model["MeioComunicacao"] = {
                    "TelefoneId": model.Telefone,
                    "Telefone": model.NovoTelefone,
                    "CelularId": model.Celular,
                    "Celular": model.NovoCelular,
                    "EmailId": model.Email,
                    "Email": model.NovoEmail
                }
                model["Codigo"] = $stateParams.codigoFornecedor;
                fornecedorService.editarFornecedor(model)
                    .then(function () {
                        $state.go("cadastro.pesquisaFornecedor");
                    });
            }
        }
    }

    novoFornecedorCtrl.$inject = ["$scope", "$state", "fornecedorService", "tipoPapelPessoaEnum"];

    editarFornecedorCtrl.$inject = ["$scope", "$state", "$stateParams", "fornecedorService", "tipoPapelPessoaEnum"];

    angular.module("sbAdminApp")
        .controller("novoFornecedorCtrl", novoFornecedorCtrl)
        .controller("editarFornecedorCtrl", editarFornecedorCtrl);
})();