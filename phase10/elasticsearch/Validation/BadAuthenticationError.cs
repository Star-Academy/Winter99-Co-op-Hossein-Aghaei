using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class BadAuthenticationError : IPipeLineError
    {
        public PipelineFailure Name { get; } = PipelineFailure.BadAuthentication;
        public Exception ThrowException()
        {
            var exception = new Exception("You mess in security things!");
            return exception;
        }
    }
}