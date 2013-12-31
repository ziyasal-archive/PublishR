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
