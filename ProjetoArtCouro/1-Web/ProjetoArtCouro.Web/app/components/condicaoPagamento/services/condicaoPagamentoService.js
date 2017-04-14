(function () {
    "use strict";

    function condicaoPagamentoService($http, $q, urls, toastr) {
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

        function criarCondicaoPagamento(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/CondicaoPagamento/CriarCondicaoPagamento", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function editarCondicaoPagamento(model) {
            var deferred = $q.defer();
            $http.put(urls.BASE_API + "/api/CondicaoPagamento/EditarCondicaoPagamento", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirCondicaoPagamento(codigoCondicaoPagamento) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/CondicaoPagamento/ExcluirCondicaoPagamento/" + codigoCondicaoPagamento)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaCondicaoPagamento() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/CondicaoPagamento/ObterListaCondicaoPagamento")
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            criarCondicaoPagamento: criarCondicaoPagamento,
            editarCondicaoPagamento: editarCondicaoPagamento,
            excluirCondicaoPagamento: excluirCondicaoPagamento,
            obterListaCondicaoPagamento: obterListaCondicaoPagamento
        };
    }

    angular.module("sbAdminApp")
        .factory("condicaoPagamentoService", condicaoPagamentoService);
})();