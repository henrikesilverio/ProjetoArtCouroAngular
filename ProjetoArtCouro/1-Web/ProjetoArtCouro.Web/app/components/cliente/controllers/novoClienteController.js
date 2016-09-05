(function () {
    "use strict";

    function novoClienteCtrl($scope, clienteService) {
        $scope.model = {
            "CodigoCliente": "",
            "Nome": "",
            "CPF": "",
            "RG": "",
            "EPessoaFisica": "True",
            "EstadoCivil": "",
            "EstadosCivis": [
                {
                    "id": "",
                    "nome": "Selecione"
                },
                {
                    "id": 1,
                    "nome": "Solteiro(a)"
                },
                {
                    "id": 2,
                    "nome": "Casado(a)"
                },
                {
                    "id": 3,
                    "nome": "Divorciado(a)"
                },
                {
                    "id": 4,
                    "nome": "Viúvo(a)"
                },
                {
                    "id": 5,
                    "nome": "Separado(a)"
                }
            ],
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
            "Telefones": [
                {
                    "id": -1,
                    "nome": "Novo"
                }
            ],
            "Celular": "",
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
            "Estados": [
                {
                    "id": "",
                    "nome": "Selecione"
                }
            ]
        };
        $scope.nomeCabecalho = "Novo Cliente";
        $scope.salvar = function() {
            
        }
    }

    novoClienteCtrl.$inject = ["$scope", "clienteService"];

    angular.module("sbAdminApp")
        .controller("novoClienteCtrl", novoClienteCtrl);
})();