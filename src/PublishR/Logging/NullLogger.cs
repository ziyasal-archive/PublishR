using System;

namespace PublishR.Logging
{
    public class NullLogger : ILogger
    {
        public void Log(Exception exception, string message)
        {
            
        }
    }
}