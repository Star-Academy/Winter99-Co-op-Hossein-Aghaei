using System;
using elasticsearch.model;
using Elasticsearch.Net;
using Nest;

namespace elasticsearch
{
    public static class ResponseValidator
    {

        public static T Validate<T>(this T response) where T : IResponse
        {
            if (response.IsValid)
                return response;
            if (response.OriginalException is ElasticsearchClientException exception)
                CheckOriginalException(exception);
            return response;
        }

        public static ISearchResponse<Doc> Validate(this ISearchResponse<Doc> response)
        {
            if (IsResponseValid(response))
                return response;
            if (response.OriginalException is ElasticsearchClientException exception)
                CheckOriginalException(exception);
            return response;
        }

        private static void CheckOriginalException(ElasticsearchClientException exception)
        {
            throw exception.FailureReason switch
            {
                PipelineFailure.MaxTimeoutReached => new ServerTimeOutException(),
                PipelineFailure.BadAuthentication => new BadAuthenticationException(),
                PipelineFailure.BadResponse => new BadResponseException(),
                PipelineFailure.BadRequest => new BadRequestException(),
                PipelineFailure.MaxRetriesReached => new MaxRetriesException(),
                PipelineFailure.Unexpected => new UnexpectedException(),
                _ => new Exception("BBin chikar kardi ke residi be difault baad 7 khan case!!")
            };
        }

        private static bool IsResponseValid(ISearchResponse<Doc> response)
        {
            if (!response.IsValid) return false;
            if (response.Shards.Failed != 0)
                throw new AllDataNotFoundException();
            return true;
        }
    }
}