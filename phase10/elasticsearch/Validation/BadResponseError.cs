using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class BadResponseError : IPipeLineError
    {

        public PipelineFailure Name { get; } = PipelineFailure.BadResponse;
        public Exception ThrowException()
        {
            var exception = new Exception("Response isn't you expected honey!");
            return exception;
        }
    }
}