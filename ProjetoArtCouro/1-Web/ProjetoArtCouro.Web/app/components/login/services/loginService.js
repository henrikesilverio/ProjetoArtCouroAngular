(function () {
    "use strict";

    function authService($http, $q, urls, aiStorage, jwtHelper) {
        var authServiceFactory = {};
        var authentication = {
            isAuth: false,
            userName: "",
            remember: false,
            isTokenExpired: false
        };

        var logOut = function () {
            aiStorage.remove("access_token");
            aiStorage.remove("refresh_token");
            aiStorage.remove("userName");
            aiStorage.remove("remember");
            authentication.isAuth = false;
            authentication.userName = "";
            authentication.remember = false;
        };

        var login = function (loginData) {
            var deferred = $q.defer();
            $http({
                skipAuthorization: true,
                url: urls.BASE_API + "/api/security/token",
                method: "POST",
                headers: { 'Content-Type': "application/x-www-form-urlencoded" },
                data: $.param({
                    grant_type: "password",
                    username: loginData.userName,
                    password: loginData.password
                })
            }).success(function (response) {
                aiStorage.set("access_token", response.access_token);
                aiStorage.set("refresh_token", response.refresh_token);
                aiStorage.set("userName", loginData.userName);
                aiStorage.set("remember", loginData.remember);
                authentication.isAuth = true;
                authentication.userName = loginData.userName;
                authentication.remember = loginData.remember;
                deferred.resolve(response);
            }).error(function (err) {
                logOut();
                deferred.reject(err);
            });
            return deferred.promise;
        };

        var fillAuthData = function () {
            var authData = {
                token: aiStorage.get("access_token"),
                userName: aiStorage.get("userName"),
                remember: aiStorage.get("remember")
            }
            if (authData.token && authData.userName) {
                authentication.isAuth = true;
                authentication.userName = authData.userName;
                authentication.remember = authData.remember;
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