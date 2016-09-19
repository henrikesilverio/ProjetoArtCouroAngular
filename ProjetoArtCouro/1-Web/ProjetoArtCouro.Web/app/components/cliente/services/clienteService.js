(function () {
    "use strict";

    function clienteService($http, $q, urls, toastr) {
        function criarCliente(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Cliente/CriarCliente", model)
                .success(function (response) {
                    deferred.resolve(response);
                    toastr.success(response.mensagem);
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

        function obterListaEstado() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Pessoa/ObterListaEstado")
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

        function obterListaEstadoCivil() {
            var deferred = $q.defer();
            $http.get(urls.BASE_API + "/api/Pessoa/ObterListaEstadoCivil")
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
            criarCliente: criarCliente,
            obterListaEstado: obterListaEstado,
            obterListaEstadoCivil: obterListaEstadoCivil
        };
    }

    angular.module("sbAdminApp")
        .factory("clienteService", clienteService);
})();