using System;

namespace PublishR.Logging
{
    public interface ILogger
    {
        void Log(Exception exception, string message);
    }
}