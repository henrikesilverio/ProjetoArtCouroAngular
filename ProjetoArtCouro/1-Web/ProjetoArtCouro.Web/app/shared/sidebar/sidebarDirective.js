(function() {
    "use strict";

    angular.module("sbAdminApp")
        .directive("sidebar", [
            "$timeout", function($timeout) {
                return {
                    restrict: "A",
                    link: function($scope, $element) {
                        $timeout(function() {
                            $element.metisMenu();
                        });
                    }
                }
            }
        ]);
})();