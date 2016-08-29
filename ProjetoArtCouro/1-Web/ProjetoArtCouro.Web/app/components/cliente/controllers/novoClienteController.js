(function () {
    "use strict";

    function novoClienteCtrl($scope, clienteService) {
        $scope.model = {
            "CodigoCliente": "",
            "Nome": "",
            "CPF": "",
            "RG": "",
            "EPessoaFisica": "True",
            "EstadoCivil": -1,
            "EstadosCivis": [
                {
                    "id": -1,
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
            "Endereco": -1,
            "Enderecos": [
                {
                    "id": -1,
                    "nome": "Selecione"
                },
                {
                    "id": 0,
                    "nome": "Novo"
                }
            ],
            "Telefone": -1,
            "Telefones": [
                {
                    "id": -1,
                    "nome": "Selecione"
                },
                {
                    "id": 0,
                    "nome": "Novo"
                }
            ],
            "Celular": -1,
            "Celulares": [
                {
                    "id": -1,
                    "nome": "Selecione"
                },
                {
                    "id": 0,
                    "nome": "Novo"
                }
            ],
            "Email": -1,
            "Emails": [
                {
                    "id": -1,
                    "nome": "Selecione"
                },
                {
                    "id": 0,
                    "nome": "Novo"
                }
            ],
            "Logradouro": "",
            "Numero": "",
            "Bairro": "",
            "Complemento": "",
            "Cidade": "",
            "CEP": "",
            "Estado": -1,
            "Estados": [
                {
                    "id": -1,
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