using System;

namespace LCA.Blueprints
{
    public class ContextDetails
    {

#pragma warning disable 8618
        // maybe we can delete this constructor and convert the class into an interface
        public ContextDetails() { }
#pragma warning restore 8618
        public ContextDetails(string operationName, string traceId, string spanId, DateTime start, DateTime? finish = null)
        {
            OperationName = operationName;
            Start = start;
            TraceId = traceId;
            SpanId = spanId;
        }
        public string OperationName { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public string TraceId { get; set; }
        public string SpanId { get; set; }
    }
}
