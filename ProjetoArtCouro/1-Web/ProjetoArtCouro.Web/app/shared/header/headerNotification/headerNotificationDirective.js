"use strict";

angular.module("sbAdminApp")
	.directive("headerNotification", function () {
	    return {
	        templateUrl: "app/shared/header/headerNotification/headerNotificationView.html",
	        restrict: "E",
	        replace: true
	    }
	});