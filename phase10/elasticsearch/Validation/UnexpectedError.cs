﻿using System;
using Elasticsearch.Net;

namespace elasticsearch.Validation
{
    public abstract class UnexpectedError : IPipeLineError
    {
        public PipelineFailure Name { get; } = PipelineFailure.Unexpected;
        public Exception ThrowException()
        {
            var exception = new Exception("agha man ino aslan gardan nemigiram, chikar mikoni jan man??");
            return exception;
        }
    }
}