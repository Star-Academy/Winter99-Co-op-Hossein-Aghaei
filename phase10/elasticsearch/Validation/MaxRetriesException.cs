using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class MaxRetriesException : IPipeLineException
    {

        public PipelineFailure Name { get; } = PipelineFailure.MaxRetriesReached;
        public Exception ThrowException()
        {
            var exception = new Exception("Mashti fek kon server khodete, kamtar request dede khoda vakili");
            return exception;
        }
    }
}