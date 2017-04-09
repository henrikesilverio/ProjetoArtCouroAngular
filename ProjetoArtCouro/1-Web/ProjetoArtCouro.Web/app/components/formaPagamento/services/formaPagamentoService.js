(function () {
    "use strict";

    function formaPagamentoService($http, $q, urls, toastr) {
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

        function criarFormaPagamento(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/FormaPagamento/CriarFormaPagamento", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function editarFormaPagamento(model) {
            var deferred = $q.defer();
            $http.put(urls.BASE_API + "/api/FormaPagamento/EditarFormaPagamento", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirFormaPagamento(codigoFormaPagamento) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/FormaPagamento/ExcluirFormaPagamento/" + codigoFormaPagamento)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaFormaPagamento() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/FormaPagamento/ObterListaFormaPagamento")
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            criarFormaPagamento: criarFormaPagamento,
            editarFormaPagamento: editarFormaPagamento,
            excluirFormaPagamento: excluirFormaPagamento,
            obterListaFormaPagamento: obterListaFormaPagamento
        };
    }

    angular.module("sbAdminApp")
        .factory("formaPagamentoService", formaPagamentoService);
})();