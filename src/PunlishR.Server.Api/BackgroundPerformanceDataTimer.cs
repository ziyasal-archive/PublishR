using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace PunlishR.Server.Api
{
    public class BackgroundPerformanceDataTimer : IDisposable
    {
        //private readonly PerformanceCounter _processorCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private Timer _taskTimer;
        private readonly IHubContext _hub;
        readonly Random _randomGenerator;

        public BackgroundPerformanceDataTimer()
        {
            _randomGenerator = new Random();
            _hub = GlobalHost.ConnectionManager.GetHubContext<PublishrApiHub>();

        }
        public void Start()
        {
            _taskTimer = new Timer(OnTimerElapsed, null, 1000, 1000);
        }

        private void OnTimerElapsed(object sender)
        {
            //var perfValue = _processorCounter.NextValue().ToString("0.0");

            var perfValue = _randomGenerator.Next(40, 70).ToString(CultureInfo.InvariantCulture);
            _hub.Clients.All.newCpuDataValue(perfValue);
        }

        public void Stop(bool immediate)
        {
            _taskTimer.Dispose();
        }

        public void Dispose()
        {
            _taskTimer.Dispose();
        }
    }
}