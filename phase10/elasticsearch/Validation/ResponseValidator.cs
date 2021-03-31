using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using elasticsearch.model;
using Elasticsearch.Net;
using Nest;

namespace elasticsearch.Validation
{
    public static class ResponseValidator
    {
        private static readonly Dictionary<PipelineFailure, Exception> Errors = GetErrors();

        private static Dictionary<PipelineFailure, Exception> GetErrors()
        {
            var pingFailures = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(IPipeLineError).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract && !type.IsInterface && type.IsPublic)
                .ToList();

            var errors = pingFailures
                .Select(pingFailure =>
                    (IPipeLineError) Activator.CreateInstance(pingFailure)!)
                .ToDictionary(exception => exception.Name,
                    exception => exception.ThrowException());

            return errors;
        }

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
            if (response.OriginalException is not ElasticsearchClientException exception) return response;
            CheckOriginalException(exception);
            return response;
        }

        private static void CheckOriginalException(ElasticsearchClientException exception)
        {
            if (exception.FailureReason == null)
                return;
            throw Errors.GetValueOrDefault((PipelineFailure) exception.FailureReason,
                new Exception("An UnKnown error occurred"));
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