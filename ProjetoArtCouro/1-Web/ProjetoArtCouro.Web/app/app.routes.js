(function () {
    "use strict";

    function configuration($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $httpProvider, jwtOptionsProvider, cfpLoadingBarProvider) {
        $ocLazyLoadProvider.config({
            debug: false,
            events: true
        });

        cfpLoadingBarProvider.spinnerTemplate = "<div class=\"spiner-example\"><div class=\"sk-spinner sk-spinner-rotating-plane\"></div></div>";

        $urlRouterProvider.otherwise("/home");

        function loadScripts($ocLazyLoad) {
            return $ocLazyLoad.load(
            {
                name: "sbAdminApp",
                files: [
                    "app/shared/sidebar/sidebarDirective.js",
                    "app/shared/sidebar/sidebarSearch/sidebarSearchDirective.js"
                ]
            });
        }

        $stateProvider
            .state("root", {
                abstract: true,
                controller: function ($scope) {
                    $scope.data = new Date();
                },
                controllerAs: "ctrl",
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
                //Login
            }).state("auth", {
                abstract: true,
                templateUrl: "app/components/login/views/loginView.html",
                controller: "loginCtrl",
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("auth.Login", {
                url: "/Login"
                //Home
            }).state("root.home", {
                url: "/home",
                controller: "homeCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/home/views/homeView.html"
                //Cadastro
            }).state("cadastro", {
                abstract: true,
                controller: function ($scope) {
                    $scope.data = new Date();
                },
                controllerAs: "ctrl",
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
                // Cliente
            }).state("cadastro.pesquisaCliente", {
                url: "/PesquisaCliente",
                controller: "pesquisaClienteCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/cliente/views/pesquisaClienteView.html"
            }).state("cadastro.novoCliente", {
                url: "/NovoCliente",
                controller: "novoClienteCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/cliente/views/clienteView.html"
            }).state("cadastro.editarCliente", {
                url: "/EditarCliente/:codigoCliente",
                controller: "editarClienteCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/cliente/views/clienteView.html"
                // Fornecedor
            }).state("cadastro.pesquisaFornecedor", {
                url: "/PesquisaFornecedor",
                controller: "pesquisaFornecedorCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/fornecedor/views/pesquisaFornecedorView.html"
            }).state("cadastro.novoFornecedor", {
                url: "/NovoFornecedor",
                controller: "novoFornecedorCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/fornecedor/views/fornecedorView.html"
            }).state("cadastro.editarFornecedor", {
                url: "/EditarFornecedor/:codigoFornecedor",
                controller: "editarFornecedorCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/fornecedor/views/fornecedorView.html"
                // Funcionario
            }).state("cadastro.pesquisaFuncionario", {
                url: "/funcionario",
                controller: "pesquisaFuncionarioCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/funcionario/views/pesquisaFuncionarioView.html"
            }).state("cadastro.novoFuncionario", {
                url: "/NovoFuncionario",
                controller: "novoFuncionarioCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/funcionario/views/funcionarioView.html"
            }).state("cadastro.editarFuncionario", {
                url: "/EditarFuncionario/:codigoFuncionario",
                controller: "editarFuncionarioCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/funcionario/views/funcionarioView.html"
                // Produto
            }).state("cadastro.produto", {
                url: "/produto",
                controller: "produtoCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/produto/views/produtoView.html"
                // Forma de Pagamento
            }).state("cadastro.formaPagamento", {
                url: "/formaPagamento",
                controller: "formaPagamentoCtrl",
                controllerAs: "ctrl",
                templateUrl: "app/components/formaPagamento/views/formaPagamentoView.html"
                // Condicao de Pagamento
            }).state("cadastro.condicaoPagamento", {
                url: "/condicaoPagamento",
                templateUrl: "app/components/condicaoPagamento/views/condicaoPagamentoView.html"
                // Usuarios
            }).state("cadastro.usuarios", {
                url: "/usuarios",
                templateUrl: "app/components/usuario/views/usuarioView.html"
                //Operações
            }).state("operacoes", {
                abstract: true,
                controller: function ($scope) {
                    $scope.data = new Date();
                },
                controllerAs: "ctrl",
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("operacoes.compra", {
                url: "/compra",
                templateUrl: "app/components/compra/views/compraView.html"
            }).state("operacoes.venda", {
                url: "/venda",
                templateUrl: "app/components/venda/views/vendaView.html"
            }).state("operacoes.contaPagar", {
                url: "/contaPagar",
                templateUrl: "app/components/contaPagar/views/contaPagarView.html"
            }).state("operacoes.contaReceber", {
                url: "/contaReceber",
                templateUrl: "app/components/contaReceber/views/contaReceberView.html"
                //Relatorios
            }).state("relatorios", {
                abstract: true,
                controller: function ($scope) {
                    $scope.data = new Date();
                },
                controllerAs: "ctrl",
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("relatorios.estoque", {
                url: "/estoque",
                templateUrl: "app/components/estoque/views/estoqueView.html"
            }).state("configuracoes", {
                abstract: true,
                controller: function ($scope) {
                    $scope.data = new Date();
                },
                controllerAs: "ctrl",
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("configuracoes.grupoConfiguracao", {
                url: "/grupoConfiguracao",
                templateUrl: "app/components/configuracao/views/grupoView.html"
            }).state("configuracoes.configuracaoUsuario", {
                url: "/configuracaoUsuario",
                templateUrl: "app/components/configuracao/views/configuracaoUsuarioView.html"
            }).state("configuracoes.alterarSenha", {
                url: "/alterarSenha",
                templateUrl: "app/components/usuario/views/alterarSenhaView.html"
            });

        jwtOptionsProvider.config({
            tokenGetter: function (jwtHelper, $uibModal, $http, options, urls) {
                var extension = options.url.substr(options.url.length - 5);
                var token = localStorage.getItem("access_token");
                if (extension === ".html" || extension === ".json" || !token) {
                    return null;
                }

                var remember = JSON.parse(localStorage.getItem("remember"));
                var refreshToken = localStorage.getItem("refresh_token");
                if (remember && jwtHelper.isTokenExpired(token)) {
                    return $http({
                        url: urls.BASE_API + "/api/security/token",
                        method: "POST",
                        skipAuthorization: true,
                        headers: {
                            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                        },
                        data: $.param({
                            grant_type: "refresh_token",
                            refresh_token: refreshToken
                        })
                    }).then(function (response) {
                        token = response.data.access_token;
                        refreshToken = response.data.refresh_token;
                        localStorage.setItem("access_token", token);
                        localStorage.setItem("refresh_token", refreshToken);
                        return token;
                    }, function () {
                        localStorage.removeItem("access_token");
                        localStorage.removeItem("refresh_token");
                    });
                } else {
                    //var modalInstance = $uibModal.open({
                    //    animation: true,
                    //    ariaLabelledBy: "modal-title-top",
                    //    ariaDescribedBy: "modal-body-top",
                    //    templateUrl: "app/components/login/views/loginView.html",
                    //    size: "sm",
                    //    controller: function ($uibModalInstance) {
                    //        $uibModalInstance.close(token);
                    //    }
                    //});
                    //return modalInstance.result;
                    return token;
                }
            },
            whiteListedDomains: ["localhost"]
        });

        $httpProvider.interceptors.push("jwtInterceptor");
    }

    angular
        .module("sbAdminApp")
        .config(configuration)
        .run(function ($rootScope, $state, authService) {
            $rootScope.$state = $state;
            $rootScope.$on("$stateChangeStart", function (e, to) {
                authService.fillAuthData();
                if (to.data && to.data.requiresLogin) {
                    if (!authService.authentication.isAuth ||
                        authService.authentication.isTokenExpired) {
                        e.preventDefault();
                        $state.go("auth.Login");
                    }
                } else if (to.name === "auth.Login" &&
                    authService.authentication.isAuth &&
                    !authService.authentication.isTokenExpired) {
                    e.preventDefault();
                    $state.go("root.home");
                }
            });
        });
})();