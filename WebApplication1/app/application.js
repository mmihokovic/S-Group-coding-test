var myApp = angular.module("myApp", []);

myApp.factory('mapserviceService',
  ['$http', function($http) {

    var serviceBase = '/api/mapservice';

    var mapserviceServiceFactory = {};

    mapserviceServiceFactory.getService = function() {
      return $http.get(serviceBase + '/GetService');
    };

    mapserviceServiceFactory.getLayers = function () {
      return $http.get(serviceBase + '/GetLayers');
    };

    mapserviceServiceFactory.getQueriableLayers = function () {
      return $http.get(serviceBase + '/GetQueriableLayers');
    };

    mapserviceServiceFactory.getMapImage = function (xMin, yMin, xMax, yMax) {
      return $http({
        method: 'GET',
        url: serviceBase + '/GetMapImage',
        params: {
              xMin: xMin,
              yMin: yMin,
              xMax: xMax,
              yMax: yMax
        }
      });
    };

    return mapserviceServiceFactory;
  }]);

myApp.controller("homeController", ["$scope", "mapserviceService", function ($scope, mapserviceService) {
  $scope.serviceSpec = {};
  $scope.data = {}
  $scope.data.layers = [];
  $scope.data.queriableLayers = [];

  $scope.xMin = -127.8;
  $scope.yMin = 5.8;
  $scope.xMax = -63.5;
  $scope.yMax = 70.1;

  $scope.imageUrl = "";

  $scope.errorMessage = "";

  $scope.getService = function () {
    mapserviceService.getService()
      .then(function(data) {
        $scope.serviceSpec = data.data;
        },
        function() {
          $scope.errorMessage = "Error getting service info";
        });
  };


  $scope.getLayers = function () {
    mapserviceService.getLayers()
      .then(function (data) {
        $scope.data.layers = data.data;
        },
        function () {
          $scope.errorMessage = "Error getting layers";
        });
  };


  $scope.getQueriableLayers = function () {
    mapserviceService.getQueriableLayers()
      .then(function (data) {
        $scope.data.queriableLayers = data.data;
        },
        function () {
          $scope.errorMessage = "Error getting queriableLayers";
        });
  };

  $scope.getImage = function() {
    mapserviceService.getMapImage($scope.xMin, $scope.yMin, $scope.xMax, $scope.yMax)
      .then(function (data) {
        $scope.imageUrl = data.data;
        },
        function () {
          $scope.errorMessage = "Error getting queriableLayers";
        });
  }

  var init = function () {
    $scope.getService();
    $scope.getLayers();
    $scope.getQueriableLayers();
  };

  init();
}]);