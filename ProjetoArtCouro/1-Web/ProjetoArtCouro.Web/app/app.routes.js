(function () {
    function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
        $ocLazyLoadProvider.config({
            debug: false,
            events: true
        });

        $urlRouterProvider.otherwise("/home");

        function loadScripts($ocLazyLoad) {
            return $ocLazyLoad.load(
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
                templateUrl: "app/shared/main/mainView.html",
                resolve: {
                    loadScripts: loadScripts
                }
                //Home
            }).state("home", {
                parent: "root",
                url: "/home",
                templateUrl: "app/components/home/views/homeView.html"
                //Cadastro
            }).state("cliente", {
                parent: "root",
                url: "/pesquisaClienteView",
                templateUrl: "app/components/cliente/views/pesquisaClienteView.html"
            }).state("fornecedor", {
                parent: "root",
                url: "/fornecedor",
                templateUrl: "app/components/fornecedor/views/fornecedorView.html"
            }).state("funcionario", {
                parent: "root",
                url: "/funcionario",
                templateUrl: "app/components/funcionario/views/funcionarioView.html"
            }).state("produto", {
                parent: "root",
                url: "/produto",
                templateUrl: "app/components/produto/views/produtoView.html"
            }).state("formaPagamento", {
                parent: "root",
                url: "/formaPagamento",
                templateUrl: "app/components/formaPagamento/views/formaPagamentoView.html"
            }).state("condicaoPagamento", {
                parent: "root",
                url: "/condicaoPagamento",
                templateUrl: "app/components/condicaoPagamento/views/condicaoPagamentoView.html"
            }).state("usuarios", {
                parent: "root",
                url: "/usuarios",
                templateUrl: "app/components/usuario/views/usuarioView.html"
                //Operações
            }).state("compra", {
                parent: "root",
                url: "/compra",
                templateUrl: "app/components/compra/views/compraView.html"
            }).state("venda", {
                parent: "root",
                url: "/venda",
                templateUrl: "app/components/venda/views/vendaView.html"
            }).state("contaPagar", {
                parent: "root",
                url: "/contaPagar",
                templateUrl: "app/components/contaPagar/views/contaPagarView.html"
            }).state("contaReceber", {
                parent: "root",
                url: "/contaReceber",
                templateUrl: "app/components/contaReceber/views/contaReceberView.html"
                //Relatorios
            }).state("estoque", {
                parent: "root",
                url: "/estoque",
                templateUrl: "app/components/estoque/views/estoqueView.html"
            }).state("grupoConfiguracao", {
                parent: "root",
                url: "/grupoConfiguracao",
                templateUrl: "app/components/configuracao/views/grupoView.html"
            }).state("configuracaoUsuario", {
                parent: "root",
                url: "/configuracaoUsuario",
                templateUrl: "app/components/configuracao/views/configuracaoUsuarioView.html"
            }).state("alterarSenha", {
                parent: "root",
                url: "/alterarSenha",
                templateUrl: "app/components/usuario/views/alterarSenhaView.html"
            });
    }

    angular
        .module("sbAdminApp")
        .config(config)
        .run(function ($rootScope, $state) {
            $rootScope.$state = $state;
        });
})();