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
                abstract: true,
                templateUrl: "app/shared/main/mainView.html",
                resolve: {
                    loadScripts: loadScripts
                }
            }).state("home", {
                parent: "root",
                url: "/home",
                templateUrl: "app/components/home/views/homeView.html"
            }).state("cadastro", {
                abstract: true,
                parent: "root"
            }).state("cliente", {
                parent: "cadastro",
                url: "/cliente",
                templateUrl: "app/components/cliente/views/clienteView.html"
            }).state("fornecedor", {
                parent: "cadastro",
                url: "/fornecedor",
                templateUrl: "app/components/fornecedor/views/fornecedorView.html"
            }).state("funcionario", {
                parent: "cadastro",
                url: "/funcionario",
                templateUrl: "app/components/funcionario/views/funcionarioView.html"
            }).state("produto", {
                parent: "cadastro",
                url: "/produto",
                templateUrl: "app/components/produto/views/produtoView.html"
            });
    }

    angular
        .module("sbAdminApp")
        .config(config)
        .run(function ($rootScope, $state) {
            $rootScope.$state = $state;
        });
})();