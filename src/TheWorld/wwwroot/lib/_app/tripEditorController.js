!function(){"use strict";function a(a,t){var n=this;n.nombre=a.tripName,n.paradas=[],n.errorMessage="",n.isBusy=!0,n.newStop={};var o="/Api/Trips/"+n.nombre+"/stops";t.get(o).then(function(a){angular.copy(a.data,n.paradas),r(n.paradas)},function(a){n.errorMessage="Fall� al caragar las paradas del viaje"+n.nombre})["finally"](function(){n.isBusy=!1}),n.addStop=function(){n.isBusy=!0,t.post(o,n.newStop).then(function(a){n.paradas.push(a.data),r(n.paradas),n.newStop={}},function(a){n.errorMessage="Fall� al agregar nueva parada"+a})["finally"](function(){n.isBusy=!1})}}function r(a){var r=_.map(a,function(a){return{lat:a.latitud,"long":a.longitud,info:a.nombre}});travelMap.createMap({stops:r,selector:"#map",currentStop:0,initialZoom:4})}a.$inject=["$routeParams","$http"],angular.module("app-trips").controller("tripEditorController",a)}();