function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

angular.module("JinStore", [])



angular.module("JinStore").controller("TripsController", ["$http", "$scope", function ($http, $scope) {
    var origin = getParameterByName("origin");
    var destination = getParameterByName("destination");
    var departureTime = getParameterByName("departureTime");
    var adultCount = getParameterByName("adultCount");
    $http.get('/trips.json?origin=' + origin + '&destination=' + destination + '&departureTime=' + departureTime + '&adultCount=' + adultCount).success(function (data) {
        $scope.trips = data;
    });

    
     //* activate code below to alow api access
   
    //$http.get('/api/trips?origin=' + origin + '&destination=' + destination + '&departureTime=' + departureTime + '&adultCount=' + adultCount).success(function (data) {
    //    $scope.trips = data;
    //});

 
}]);

angular.module("JinStore").controller("TripController", ["$http", "$scope", function ($http, $scope) {

    $scope.tripSelect = function () {
        $http.put("/api/trips?id=" + $scope.$parent.x.id + 
            "&origin=" + $scope.$parent.x.slice[0].segment[0].leg[0].origin+
            "&destination=" + $scope.$parent.x.slice[0].segment[$scope.$parent.x.slice[0].segment.length - 1].leg[0].destination+
            "&departureTime=" + $scope.$parent.x.slice[0].segment[0].leg[0].departureTime+
            "&arrivalTime=" + $scope.$parent.x.slice[0].segment[$scope.$parent.x.slice[0].segment.length - 1].leg[0].arrivalTime

            ).success(function (data) {
                window.location.assign("/cart/index/" + data);
            });
        //alert("trip selected:" + $scope.$parent.x.id);
    }


}]);



