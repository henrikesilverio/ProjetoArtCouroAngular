"use strict";

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */

angular.module("sbAdminApp")
  .directive("sidebar", ["$location", "$timeout", function ($location, $timeout) {
      return {
          templateUrl: "app/shared/sidebar/sidebarView.html",
          restrict: "E",
          replace: true,
          link: function ($scope, $element) {
              $timeout(function () {
                  $element.metisMenu();
              });
          }
      }
  }]);
