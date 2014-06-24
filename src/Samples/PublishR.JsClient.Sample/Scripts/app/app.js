var publishR = (function ($p) {
    "use strict";
    var bootstrapper = function (config) {

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
    $p.Bootstrapper = bootstrapper;
    return $p;
}(publishR || (publishR = {})));
