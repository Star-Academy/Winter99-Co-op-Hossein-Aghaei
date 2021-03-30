using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class ServerTimeOutException : IPipeLineException
    {
        public PipelineFailure Name { get; } = PipelineFailure.MaxTimeoutReached;
        public Exception ThrowException()
        {
            var exception = new Exception("Server is busy\nPlease Try again in a few Seconds");
            return exception;
        }
    }
}