//var performanceDataHub;
//$(document).ready(function () {
//    $.connection.hub.loging = true;
//    performanceDataHub = $.connection.performanceDataHub;

//    performanceDataHub.client.newCpuDataValue = function (data) {
//        console.log("Data received : " + data);
//    };
//    $.connection.hub.url = 'http://localhost:4416/signalr';

//    $.connection.hub.start().done(function () {
//        console.log("App connected.");
//    }).fail(function (err) {
//        console.log("fail :" + err);
//    });
//});


var publishrR = (function ($p) {
    "use strict";
    var bootsrapper = function (config) {

        var initHub;
        initHub = function ($options) {
            console.log("App started.");
            var connection = $.hubConnection($options.url);

            var proxy = connection.createHubProxy('publishrApiHub');
            connection.start().done(function () {
                console.log("App connected.");
            });
            proxy.on('newCpuDataValue', function (data) {
                console.log("Data received : " + data);
            });
        };

        return {
            start: function ($options) {
                initHub($options);
            }
        };
    };
    $p.Bootsrapper = bootsrapper;
    return $p;
}(publishrR || (publishrR = {})));