using System;
using elasticsearch.model;
using Elasticsearch.Net;
using elasticsearch.Validation;
using Nest;
using NSubstitute;
using Xunit;

namespace elasticsearchTest
{
    public class ResponseValidatorTest
    {
        [Fact]
        public void Validate_ShouldThrowBadAuthenticationException_WhenBadAuthenticationIsTrue()
        {
            var elasticsearchClientException = new ElasticsearchClientException(PipelineFailure.BadAuthentication, "exception", new Exception());
            var searchResponse = Substitute.For<ISearchResponse<Doc>>();
            
            searchResponse.IsValid.Returns(false);
            searchResponse.OriginalException.Returns(elasticsearchClientException);
            
            Assert.Throws<Exception>(searchResponse.Validate);
        }
        
        [Fact]
        public void Validate_ShouldThrowAllDataNotFoundException_WhenFailedShardsIsNotZero()
        {
            var elasticsearchClientException = new ElasticsearchClientException(PipelineFailure.Unexpected, "exception", new Exception());
            var iSearchResponse = Substitute.For<ISearchResponse<Doc>>();
            
            iSearchResponse.IsValid.Returns(false);
            iSearchResponse.OriginalException.Returns(elasticsearchClientException);
            
            Assert.Throws<Exception>(iSearchResponse.Validate);
        }
    }
}