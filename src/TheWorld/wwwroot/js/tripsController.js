//tripController.js
(function () {
    "use strict";

    //uso el modulo

    angular.module("app-trips")
    .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;
       // vm.name = "manu";
        vm.trips = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newTrip = {};
        $http.get("/Api/Trips")
            .then(function (response) {
                //exito
                angular.copy(response.data, vm.trips);
            }, function (error) {
                vm.errorMessage = "No se pudieron cargar los datos" + error;
            })

        .finally(function(){
            vm.isBusy=false;
        });

        vm.addTrip = function () {
            //vm.trips.push({ nombre: vm.newTrip.nombre, creacion: new Date() });
            //vm.newTrip = {};
            vm.isBusy = true;
            $http.post("/Api/Trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    vm.errorMessage="Falló al guardar el viaje :"+vm.newTrip.nombre;

                })
            .finally(function () {
                vm.isBusy = false;
            });



        };
    }
})();