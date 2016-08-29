(function() {
    "use strict";

    function clienteService($http, $q, urls) {
        function criarCliente(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Cliente/CriarCliente", model)
                .success(function (response) {
                    deferred.resolve(response);
                }).error(function (err) {
                    deferred.reject(err);
                });
            return deferred.promise;
        }

        return {
            criarCliente: criarCliente
        };
    }

    angular.module("sbAdminApp")
        .factory("clienteService", clienteService);
})();