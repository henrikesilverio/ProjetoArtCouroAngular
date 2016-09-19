(function() {
    "use strict";

    function pesquisaClienteService($http, $q, urls, toastr) {
        function pesquisaCliente(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Cliente/PesquisarCliente", model)
                .success(function (response) {
                    deferred.resolve(response.objetoRetorno);
                }).error(function (err) {
                    deferred.reject(err);
                    if (err.data) {
                        toastr.error(err.data.mensagem);
                    } else {
                        toastr.error(err.mensagem);
                    }
                });
            return deferred.promise;
        }

        return {
            pesquisaCliente: pesquisaCliente
        };
    }

    angular.module("sbAdminApp")
        .factory("pesquisaClienteService", pesquisaClienteService);
})();