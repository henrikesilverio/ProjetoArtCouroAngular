(function () {
    "use strict";

    function pesquisaFornecedorService($http, $q, urls, toastr) {
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

        function pesquisaFornecedor(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Fornecedor/PesquisarFornecedor", model)
                .success(function (response) {
                    deferred.resolve(response.objetoRetorno);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirFornecedor(codigoFornecedor) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/Fornecedor/ExcluirFornecedor/" + codigoFornecedor)
                .success(function (response) {
                    deferred.resolve(response.objetoRetorno);
                    toastr.success(response.mensagem);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            pesquisaFornecedor: pesquisaFornecedor,
            excluirFornecedor: excluirFornecedor
        };
    }

    angular.module("sbAdminApp")
        .factory("pesquisaFornecedorService", pesquisaFornecedorService);
})();