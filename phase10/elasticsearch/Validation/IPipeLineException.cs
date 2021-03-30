using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public interface IPipeLineException
    {
        PipelineFailure Name { get; } 
        Exception ThrowException();
    }
}