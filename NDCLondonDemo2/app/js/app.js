var tt = window.tt || {};

tt.app = angular.module("myApp", ["ngRoute"]);

tt.app.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "app/views/start.html", controller: "StartController" })
        .otherwise({ redirectTo: "/" });
});