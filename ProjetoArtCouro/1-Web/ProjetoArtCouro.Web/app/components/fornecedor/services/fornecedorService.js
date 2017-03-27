(function () {
    "use strict";

    function fornecedorService($http, $q, urls, toastr) {
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

        function criarFornecedor(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Fornecedor/CriarFornecedor", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function editarFornecedor(model) {
            var deferred = $q.defer();
            $http.put(urls.BASE_API + "/api/Fornecedor/EditarFornecedor", model)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function pesquisarFornecedorPorCodigo(codigoFornecedor) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Fornecedor/PesquisarFornecedorPorCodigo",
                { "codigoFornecedor": codigoFornecedor })
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaEstado() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Pessoa/ObterListaEstado")
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function obterListaEstadoCivil() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Pessoa/ObterListaEstadoCivil")
                .success(function (response) {
                    deferred.resolve(response.objetoRetorno);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            criarFornecedor: criarFornecedor,
            editarFornecedor: editarFornecedor,
            pesquisarFornecedorPorCodigo: pesquisarFornecedorPorCodigo,
            obterListaEstado: obterListaEstado,
            obterListaEstadoCivil: obterListaEstadoCivil
        };
    }

    angular.module("sbAdminApp")
        .factory("fornecedorService", fornecedorService);
})();