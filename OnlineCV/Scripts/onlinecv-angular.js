
var HomeController = function ($scope, $http) {
    $scope.mycv = null;
    $http.get('Content/mycv.json')
        .success(function (data) {
            $scope.mycv = data;
        })
        .error(function (data, status, error, config) {
            alert(error);
            $scope.mycv = [{ heading: "Error", description: "Could not load json   data" }];
        });
    
}

// The $inject property of every controller 
// (and pretty much every other type of object in Angular) 
// needs to be a string array equal to the controllers arguments, only as strings
HomeController.$inject = ['$scope','$http'];

// AngularJS App creation
var onlineCVApp = angular.module('onlineCVApp', []);

onlineCVApp.controller('HomeController', HomeController);