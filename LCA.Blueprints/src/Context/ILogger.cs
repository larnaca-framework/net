namespace LCA.Blueprints
{
    public interface ILogger
    {
        public void Trace(string log) => Log(log, ELogLevel.Trace);
        public void Debug(string log) => Log(log, ELogLevel.Debug);
        public void Information(string log) => Log(log, ELogLevel.Information);
        public void Warning(string log) => Log(log, ELogLevel.Warning);
        public void Error(string log) => Log(log, ELogLevel.Error);
        public void Critical(string log) => Log(log, ELogLevel.Critical);
        void Log(string log, ELogLevel level);
    }
}