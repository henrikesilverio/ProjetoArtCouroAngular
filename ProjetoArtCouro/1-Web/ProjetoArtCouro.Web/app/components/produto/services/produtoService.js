(function() {
    "use strict";

    function produtoService($http, $q, urls, toastr) {
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

        function criarProduto(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Produto/CriarProduto", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function editarProduto(model) {
            var deferred = $q.defer();
            $http.put(urls.BASE_API + "/api/Produto/EditarProduto", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirProduto(codigoProduto) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/Produto/ExcluirProduto/" + codigoProduto)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaProduto() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Produto/ObterListaProduto")
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaUnidade() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Produto/ObterListaUnidade")
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }
        
        return {
            criarProduto: criarProduto,
            editarProduto: editarProduto,
            excluirProduto: excluirProduto,
            obterListaUnidade: obterListaUnidade,
            obterListaProduto: obterListaProduto
        };
    }

    angular.module("sbAdminApp")
        .factory("produtoService", produtoService);
})();