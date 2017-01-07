angular.module("JinStore", [])



angular.module("JinStore").controller("TripsController", ["$http", "$scope", function ($http, $scope) {

    $http.get('/trips.json').success(function (data) {
        $scope.trips = data;
      });

     //* activate code below to alow api access
    //$http.get('/api/trips').success(function (data) {
    //    $scope.trips = data;
    //});

 
}]);



