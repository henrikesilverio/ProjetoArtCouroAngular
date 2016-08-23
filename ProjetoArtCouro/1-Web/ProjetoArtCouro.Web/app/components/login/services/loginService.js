(function () {
    "use strict";

    function authService($http, $q, urls, aiStorage, jwtHelper) {
        var authServiceFactory = {};
        var authentication = {
            isAuth: false,
            userName: "",
            isTokenExpired: false
        };

        var logOut = function () {
            aiStorage.remove("jwt");
            authentication.isAuth = false;
            authentication.userName = "";
        };

        var login = function (loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post(urls.BASE_API + "/api/security/token", data, { headers: { 'Content-Type': "application/x-www-form-urlencoded" } })
                .success(function (response) {
                    aiStorage.set("jwt", { token: response.access_token, userName: loginData.userName });
                    authentication.isAuth = true;
                    authentication.userName = loginData.userName;
                    deferred.resolve(response);
                }).error(function (err) {
                    logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        };

        var fillAuthData = function () {
            var authData = aiStorage.get("jwt");
            if (authData) {
                authentication.isAuth = true;
                authentication.userName = authData.userName;
                authentication.isTokenExpired = jwtHelper.isTokenExpired(authData.token);
            }
        }

        authServiceFactory.login = login;
        authServiceFactory.logOut = logOut;
        authServiceFactory.fillAuthData = fillAuthData;
        authServiceFactory.authentication = authentication;

        return authServiceFactory;
    };

    angular.module("sbAdminApp")
        .factory("authService", authService);
})();