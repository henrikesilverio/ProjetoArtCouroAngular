(function () {
    "use strict";

    function novoClienteCtrl($scope, $state, clienteService, tipoPapelPessoaEnum) {
        $scope.model = {
            "CodigoCliente": "",
            "Nome": "",
            "CPF": "",
            "RG": "",
            "EPessoaFisica": "True",
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

        clienteService.obterListaEstado().then(function(data) {
            $scope.model.Estados = data;
            $scope.model.Estados.unshift({
                "codigo": "",
                "nome": "Selecione"
            });
        });

        clienteService.obterListaEstadoCivil().then(function (data) {
            $scope.model.EstadosCivis = data;
            $scope.model.EstadosCivis.unshift({
                "codigo": "",
                "nome": "Selecione"
            });
        });

        $scope.nomeCabecalho = "Novo Cliente";
        $scope.salvar = function(valido) {
            if (valido) {
                var model = $scope.model;
                model["EstadoCivilId"] = model.EstadoCivil;
                model["PapelPessoa"] = tipoPapelPessoaEnum.CLIENTE;
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
                clienteService.criarCliente(model)
                    .then(function() {
                        $state.go("cadastro.pesquisaCliente");
                    });
            }
        }
    }

    novoClienteCtrl.$inject = ["$scope", "$state", "clienteService", "tipoPapelPessoaEnum"];

    angular.module("sbAdminApp")
        .controller("novoClienteCtrl", novoClienteCtrl);
})();