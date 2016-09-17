(function() {
    "use strict";

    angular
        .module("sbAdminApp", [
            "oc.lazyLoad",
            "ui.router",
            "ui.bootstrap",
            "angular-loading-bar",
            "angular-jwt",
            "angular-storage",
            "datatables",
            "datatables.bootstrap",
            "ngAnimate",
            "ngDragDrop",
            "ngSanitize"
        ]).constant("urls", {
            BASE_API: "http://localhost:15317"
        });
})();