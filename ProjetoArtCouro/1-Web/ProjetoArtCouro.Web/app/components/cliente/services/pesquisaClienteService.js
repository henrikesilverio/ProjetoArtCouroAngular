(function () {
    "use strict";

    function pesquisaClienteService($http, $q, urls, toastr) {
        function successWithReturn(deferred, response) {
            deferred.resolve(response.objetoRetorno);
        }

        function successWithoutReturn(deferred, response) {
            deferred.resolve(response);
            toastr.success(response.mensagem);
        }

        function error(err, deferred) {
            deferred.reject(err);
            if (_.isEmpty(err)) {
                toastr.error("Objeto erro vazio.");
            }
            else {
                toastr.error(err.exceptionMessage);
                console.log(err.message);
                console.log(err.exceptionType);
                console.log(err.stackTrace);
            }
        }

        function pesquisaCliente(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Cliente/PesquisarCliente", model)
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirCliente(codigoCliente) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/Cliente/ExcluirCliente/" + codigoCliente)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            pesquisaCliente: pesquisaCliente,
            excluirCliente: excluirCliente
        };
    }

    angular.module("sbAdminApp")
        .factory("pesquisaClienteService", pesquisaClienteService);
})();