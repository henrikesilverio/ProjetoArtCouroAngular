(function() {
    "use strict";

    angular
        .module("sbAdminApp")
        .constant("urls", {
            BASE_API: "http://localhost:15317"
        }).constant("tipoPapelPessoaEnum", {
            NENHUM: 0,
            PESSOAFISICA: 1,
            PESSOAJURIDICA: 2,
            FUNCIONARIO: 3,
            CLIENTE: 4,
            FORNECEDOR: 5
        });
})();