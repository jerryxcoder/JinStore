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

angular.module("JinStore").controller("TripController", ["$http", "$scope", function ($http, $scope) {

    $scope.tripSelect = function () {
        $http.put("/api/trips?id=" + $scope.$parent.x.id + "&from=" + $scope.$parent.x.slice[0].segment[0].leg[0].origin);
        //alert("trip selected:" + $scope.$parent.x.id);
    }


}]);



