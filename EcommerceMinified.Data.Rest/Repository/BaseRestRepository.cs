using System;
using System.Net;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace EcommerceMinified.Data.Rest.Repository;

public class BaseRestRepository
{
    protected readonly ILogger Logger;
    protected readonly RestClient Client;
    public EventHandler? OnUnauthorizedResponse;
    protected int RequestTimeout { get; set; }

    public BaseRestRepository(ILogger logger, string baseUrl)
    {
        Logger = logger;
        Client = new RestClient(baseUrl);
        RequestTimeout = 10;

        Client = new RestClient(new RestClientOptions(baseUrl));
    }

    protected async Task<T> ExecuteOrThrowAsync<T>(RestRequest request, bool throwException = false)
    {
        var response = await Client.ExecuteAsync<T>(request, CancellationToken.None);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            OnUnauthorizedResponse?.Invoke(response, EventArgs.Empty);
        }

        if (response.ErrorException != null && throwException)
        {
            throw new InvalidOperationException(
                $"Parse error of the BaseRestRepository response  ({request.Resource}): {response.StatusCode} - {response.Content}",
                response.ErrorException);
        }

        if (!response.IsSuccessful && throwException)
        {
            throw new HttpRequestException(
                $"Response error of BaseRestRepository ({request.Resource}): {response.StatusCode} - {response.Content}",
                null, response.StatusCode);
        }

        return response.Data!;
    }
}
