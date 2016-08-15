"use strict";

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */

angular.module("sbAdminApp")
  .directive("sidebarSearch", function () {
      return {
          templateUrl: "app/shared/sidebar/sidebarSearch/sidebarSearchView.html",
          restrict: "E",
          replace: true,
          scope: {
          },
          controller: function ($scope) {
              $scope.selectedMenu = "home";
          }
      }
  });
