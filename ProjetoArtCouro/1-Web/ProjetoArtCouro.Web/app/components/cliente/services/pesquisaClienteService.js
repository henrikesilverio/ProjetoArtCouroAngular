(function() {
    "use strict";

    function pesquisaClienteService($http, $q, urls) {
        function pesquisaCliente(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Cliente/PesquisarCliente", model)
                .success(function (response) {
                    deferred.resolve(response);
                }).error(function (err) {
                    deferred.reject(err);
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