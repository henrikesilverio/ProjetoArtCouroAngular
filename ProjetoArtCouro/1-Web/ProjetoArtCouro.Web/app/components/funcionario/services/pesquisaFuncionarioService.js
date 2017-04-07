(function () {
    "use strict";

    function pesquisaFuncionarioService($http, $q, urls, toastr) {
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

        function pesquisaFuncionario(model) {
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/Funcionario/PesquisarFuncionario", model)
                .success(function (response) {
                    successWithReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        function excluirFuncionario(codigoFuncionario) {
            var deferred = $q.defer();
            $http.delete(urls.BASE_API + "/api/Funcionario/ExcluirFuncionario/" + codigoFuncionario)
                .success(function (response) {
                    successWithoutReturn(deferred, response);
                }).error(function (err) {
                    error(err, deferred);
                });
            return deferred.promise;
        }

        return {
            pesquisaFuncionario: pesquisaFuncionario,
            excluirFuncionario: excluirFuncionario
        };
    }

    angular.module("sbAdminApp")
        .factory("pesquisaFuncionarioService", pesquisaFuncionarioService);
})();