using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System.Text;


namespace HW14.MovieManager.Service.Common;

public abstract class HttpClientBase
{
    public static async Task<RequestResult<T>> Post<T>(HttpClient httpClient, Uri uri, string content)
    {
        HttpRequestMessage request = new (HttpMethod.Post, uri);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        return await PerformHttpRequest<T>(httpClient, request);
    }

    public static async Task<RequestResult<T>> Get<T>(HttpClient httpClient, Uri uri)
    {
        HttpRequestMessage request = new (HttpMethod.Get, uri);

        return await PerformHttpRequest<T>(httpClient, request);
    }

    private static async Task<RequestResult<T>> PerformHttpRequest<T>(HttpClient httpClient, HttpRequestMessage request)
    {
        const int MaxRetryAttempts = 5;

        RequestResult<T> result = new ();

        AsyncRetryPolicy _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(MaxRetryAttempts, i => TimeSpan.FromSeconds(i * 5));

        try
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                HttpResponseMessage response = (await httpClient.SendAsync(request)).EnsureSuccessStatusCode();
                result.Value = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            });

            result.Stop();
            return result;
        }
        catch(Exception ex)
        {
            result.Exception = ex;
            result.Stop();
            return result;
        }
    }
}