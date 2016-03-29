//tripEditorController.js

(function () {

    "use strict";

    angular.module("app-trips")
    .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {

        var vm = this;
        vm.nombre = $routeParams.tripName;
        vm.paradas = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/Api/Trips/" + vm.nombre + "/stops";

        $http.get(url)
        .then(function (response) {
            angular.copy(response.data, vm.paradas);
            _showMap(vm.paradas);
        }, function (error) {
            vm.errorMessage = "Falló al caragar las paradas del viaje" + vm.nombre;
        })
        .finally(function () {
            vm.isBusy = false;

        });

        vm.addStop = function(){
            vm.isBusy = true;
            $http.post(url,vm.newStop)
               .then(function (response) {
                    vm.paradas.push(response.data);
                    _showMap(vm.paradas);
                    vm.newStop = {};

                }, function (error) {
                    vm.errorMessage = "Falló al agregar nueva parada"+error;
                })
                .finally(function () {

                    vm.isBusy = false;
                    
                });

        };
   }

    function _showMap(stops) {
        //if (stops && stops.length > 0) {

        var mapStops = _.map(stops, function (item) {
            return {
                lat: item.latitud,
                long: item.longitud,
                info: item.nombre
            };
        });
        travelMap.createMap({
            stops: mapStops,
            selector: "#map",
            currentStop: 0,
            initialZoom: 4
        });
    //};
   
    }
})();