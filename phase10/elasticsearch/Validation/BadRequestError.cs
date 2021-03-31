using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class BadRequestError : IPipeLineError
    {

        public PipelineFailure Name { get; } = PipelineFailure.BadRequest;
        public Exception ThrowException()
        {
            var exception = new Exception("there is some bugs in request, fix then request again\n" +
                                          "Bikar nistim ma ke golam alaki mizani");
            return exception;
        }
    }
}