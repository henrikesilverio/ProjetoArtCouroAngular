(function () {
    "use strict";

    angular.module("sbAdminApp")
        .factory("authService", function ($http, $q, urls, aiStorage) {
            var authServiceFactory = {};
            var authentication = {
                isAuth: false,
                userName: ""
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
                }
            }

            authServiceFactory.login = login;
            authServiceFactory.logOut = logOut;
            authServiceFactory.fillAuthData = fillAuthData;
            authServiceFactory.authentication = authentication;

            return authServiceFactory;
        });
})();