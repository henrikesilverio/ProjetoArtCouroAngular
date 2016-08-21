(function () {
    "use strict";


    function authInterceptorService(aiStorage, $q, $location) {

        var authInterceptorServiceFactory = {};

        var request = function (config) {
            config.headers = config.headers || {};
            var authData = aiStorage.get("jwt");
            if (authData) {
                config.headers.Authorization = "Bearer " + authData.token;
            }
            return config;
        }

        var responseError = function (rejection) {
            if (rejection.status === 401) {
                $location.path("/login");
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = request;
        authInterceptorServiceFactory.responseError = responseError;

        return authInterceptorServiceFactory;
    };

    authInterceptorService.$inject = ["aiStorage", "$q", "$location"];
    angular.module("sbAdminApp")
        .factory("authInterceptorService", authInterceptorService);
})();