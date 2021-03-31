using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public interface IPipeLineError
    {
        PipelineFailure Name { get; } 
        Exception ThrowException();
    }
}