(function () {
    function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $httpProvider, cfpLoadingBarProvider) {
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
                },
                {
                    name: "toggle-switch",
                    files: [
                        "bower_components/angular-toggle-switch/angular-toggle-switch.min.js",
                        "bower_components/angular-toggle-switch/angular-toggle-switch.css"
                    ]
                }),
                $ocLazyLoad.load(
                {
                    name: "ngAnimate",
                    files: ["bower_components/angular-animate/angular-animate.js"]
                }),
                $ocLazyLoad.load(
                {
                    name: "ngCookies",
                    files: ["bower_components/angular-cookies/angular-cookies.js"]
                }),
                $ocLazyLoad.load(
                {
                    name: "ngResource",
                    files: ["bower_components/angular-resource/angular-resource.js"]
                }),
                $ocLazyLoad.load(
                {
                    name: "ngSanitize",
                    files: ["bower_components/angular-sanitize/angular-sanitize.js"]
                }),
                $ocLazyLoad.load(
                {
                    name: "ngTouch",
                    files: ["bower_components/angular-touch/angular-touch.js"]
                });
        }

        $stateProvider
            .state("root", {
                abstract: true,
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                },
                controller: function ($scope, $state, authService) {
                    $scope.message = "";
                    $scope.logOut = function () {
                        authService.logOut();
                        $state.go("auth.login");
                    };
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
                url: "/Login",
                controller: "loginCtrl"
                //Home
            }).state("root.home", {
                url: "/home",
                templateUrl: "app/components/home/views/homeView.html"
                //Cadastro
            }).state("cadastro", {
                abstract: true,
                templateUrl: "app/shared/main/mainView.html",
                data: {
                    requiresLogin: true
                },
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("cadastro.cliente", {
                url: "/pesquisaClienteView",
                templateUrl: "app/components/cliente/views/pesquisaClienteView.html"
            }).state("cadastro.fornecedor", {
                url: "/fornecedor",
                templateUrl: "app/components/fornecedor/views/fornecedorView.html"
            }).state("cadastro.funcionario", {
                url: "/funcionario",
                templateUrl: "app/components/funcionario/views/funcionarioView.html"
            }).state("cadastro.produto", {
                url: "/produto",
                templateUrl: "app/components/produto/views/produtoView.html"
            }).state("cadastro.formaPagamento", {
                url: "/formaPagamento",
                templateUrl: "app/components/formaPagamento/views/formaPagamentoView.html"
            }).state("cadastro.condicaoPagamento", {
                url: "/condicaoPagamento",
                templateUrl: "app/components/condicaoPagamento/views/condicaoPagamentoView.html"
            }).state("cadastro.usuarios", {
                url: "/usuarios",
                templateUrl: "app/components/usuario/views/usuarioView.html"
                //Operações
            }).state("operacoes", {
                abstract: true,
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

        $httpProvider.interceptors.push("authInterceptorService");
    }

    angular
        .module("sbAdminApp")
        .config(config)
        .run(function ($rootScope, $state, authService, jwtHelper, aiStorage) {
            $rootScope.$state = $state;
            authService.fillAuthData();
            $rootScope.$on("$stateChangeStart", function (e, to) {
                if (to.data && to.data.requiresLogin) {
                    if (!aiStorage.get("jwt") ||
                        jwtHelper.isTokenExpired(aiStorage.get("jwt").token)) {
                        e.preventDefault();
                        $state.go("auth.Login");
                    }
                } else if (to.name === "auth.Login" && authService.authentication.isAuth) {
                    e.preventDefault();
                    $state.go("root.home");
                }
            });
        });
})();