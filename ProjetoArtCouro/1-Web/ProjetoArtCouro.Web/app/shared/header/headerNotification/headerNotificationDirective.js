"use strict";

angular.module("sbAdminApp")
	.directive("headerNotification", function () {
	    return {
	        templateUrl: "app/shared/header/headerNotification/headerNotificationView.html",
	        restrict: "E",
	        replace: true,
	        controller: function ($scope, $location, authService) {
	            $scope.message = "";
	            $scope.logOut = function () {
	                authService.logOut();
	                window.location = "/#/Login";
	            };
	        }
	    }
	});