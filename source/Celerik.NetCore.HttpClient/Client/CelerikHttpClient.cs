using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Celerik.NetCore.HttpClient
{
    /// <summary>
    /// HttpClient to call services implementing the Celerik Api
    /// Protocol. The protocol is simple: all http body responses
    /// have: Data, Message, MessageType and Success.
    /// </summary>
    public class CelerikHttpClient
    {
        /// <summary>
        /// Reference to the current HttpClient.
        /// </summary>
        private System.Net.Http.HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="httpClient">Reference to the current
        /// HttpClient.</param>
        public CelerikHttpClient(System.Net.Http.HttpClient httpClient)
            => _httpClient = httpClient;

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TOutput">The type of the Data property
        /// in the ApiResponse.</typeparam>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>The task object representing the asynchronous
        /// operation.</returns>
        public async Task<ApiResponse<TOutput>> SendAsync<TOutput>(
            HttpMethod method,
            string controller,
            string endpoint,
            object payload = null)
        {
            return await SendAsync<TOutput>(
                method,
                controller,
                endpoint,
                null,
                CancellationToken.None,
                payload
            );
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TOutput">The type of the Data property
        /// in the ApiResponse.</typeparam>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="completionOption">When the operation should
        /// complete (as soon as a response is available or after reading
        /// the whole response content).</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>The task object representing the asynchronous
        /// operation.</returns>
        public async Task<ApiResponse<TOutput>> SendAsync<TOutput>(
            HttpMethod method,
            string controller,
            string endpoint,
            HttpCompletionOption completionOption,
            object payload = null)
        {
            return await SendAsync<TOutput>(
                method,
                controller,
                endpoint,
                completionOption,
                CancellationToken.None,
                payload
            );
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TOutput">The type of the Data property
        /// in the ApiResponse.</typeparam>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="completionOption">When the operation should
        /// complete (as soon as a response is available or after reading
        /// the whole response content).</param>
        /// <param name="cancellationToken">The cancellation token to cancel
        /// operation.</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>The task object representing the asynchronous
        /// operation.</returns>
        public async Task<ApiResponse<TOutput>> SendAsync<TOutput>(
            HttpMethod method,
            string controller,
            string endpoint,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken,
            object payload = null)
        {
            return await SendAsync<TOutput>(
                method,
                controller,
                endpoint,
                completionOption,
                cancellationToken,
                payload
            );
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TOutput">The type of the Data property
        /// in the ApiResponse.</typeparam>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="cancellationToken">The cancellation token to cancel
        /// operation.</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>The task object representing the asynchronous
        /// operation.</returns>
        public async Task<ApiResponse<TOutput>> SendAsync<TOutput>(
            HttpMethod method,
            string controller,
            string endpoint,
            CancellationToken cancellationToken,
            object payload = null)
        {
            return await SendAsync<TOutput>(
                method,
                controller,
                endpoint,
                null,
                cancellationToken,
                payload
            );
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TOutput">The type of the Data property
        /// in the ApiResponse.</typeparam>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="completionOption">When the operation should
        /// complete (as soon as a response is available or after reading
        /// the whole response content).</param>
        /// <param name="cancellationToken">The cancellation token to cancel
        /// operation.</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>The task object representing the asynchronous
        /// operation.</returns>
        private async Task<ApiResponse<TOutput>> SendAsync<TOutput>(
            HttpMethod method,
            string controller,
            string endpoint,
            HttpCompletionOption? completionOption,
            CancellationToken cancellationToken,
            object payload)
        {
            var url = GetUrl(method, controller, endpoint, payload);
            using var request = new HttpRequestMessage(method, url);

            if (method != HttpMethod.Get &&
                method != HttpMethod.Delete &&
                payload != null)
            {
                var json = JsonConvert.SerializeObject(payload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = null;

            if (completionOption == null &&
                cancellationToken == CancellationToken.None)
                response = await _httpClient.SendAsync(request);
            else if (completionOption != null &&
                cancellationToken == CancellationToken.None)
                response = await _httpClient.SendAsync(request, completionOption.Value);
            else if (completionOption == null &&
                cancellationToken != CancellationToken.None)
                response = await _httpClient.SendAsync(request, cancellationToken);
            else
                response = await _httpClient.SendAsync(request, completionOption.Value, cancellationToken);

            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var obj = JsonConvert.DeserializeObject<ApiResponse<TOutput>>(content);
                return obj;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                if (IsInvalidModelState(content))
                    content = GetFirstMessage(content);

                return new ApiResponse<TOutput>
                {
                    Data = default,
                    Message = content,
                    MessageType = "error",
                    Success = false
                };
            }

            throw new HttpRequestException(
                $@"Error calling service: '{0}'.StatusCode: '{1}'.Details: '{2}',
                request.RequestUri.ToString(),
                response.StatusCode,
                content"
            );
        }

        /// <summary>
        /// Builds the Url for calling a service based on the Method, Controller,
        /// Endpoint and Payload received as argument.
        /// </summary>
        /// <param name="method">The Http Method: DELETE, GET, POST, PUT.
        /// </param>
        /// <param name="controller">The name of the controller this
        /// client points to.</param>
        /// <param name="endpoint">The name of the endpoint method this
        /// client points to.</param>
        /// <param name="payload">Arguments of the service.</param>
        /// <returns>Url to call the service based on the Method, Controller,
        /// Endpoint and Payload received as argument.</returns>
        private string GetUrl(
            HttpMethod method,
            string controller,
            string endpoint,
            object payload = null)
        {
            var url = $"{controller}/{endpoint}";

            if ((method == HttpMethod.Get || method == HttpMethod.Delete) &&
                payload != null)
            {
                var builder = HttpUtility.ParseQueryString(string.Empty);
                var props = payload.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    var value = prop.GetValue(payload);
                    if (value != null)
                        builder[prop.Name] = value.ToString();
                }

                var query = builder.ToString();
                url = $"{url}?{query}";
            }

            return url;
        }

        /// <summary>
        /// Indicates if a service response is related to an Invalid
        /// Model State.
        /// </summary>
        /// <param name="response">Response received from the service.
        /// </param>
        /// <returns>True if the service response is related to an
        /// Invalid Model State.</returns>
        private static bool IsInvalidModelState(string response)
        {
            return !string.IsNullOrEmpty(response) &&
                response.Contains("\"errors\"") &&
                response.Contains("\"type\"") &&
                response.Contains("\"title\"") &&
                response.Contains("\"status\"") &&
                response.Contains("\"traceId\"");
        }

        /// <summary>
        /// Gets the first error message contained into the service
        /// response received as argument.
        /// </summary>
        /// <param name="response">Response received from the service.
        /// </param>
        /// <returns>First error message contained into the service
        /// response received as argument.</returns>
        private static string GetFirstMessage(string response)
        {
            try
            {
                var firstMessage = response.Substring(
                    response.IndexOf("[\"", StringComparison.InvariantCulture) + 2);
                firstMessage = firstMessage.Substring(0,
                    firstMessage.IndexOf("\"", StringComparison.InvariantCulture) - 1);

                return firstMessage;
            }
            catch (ArgumentOutOfRangeException)
            {
                return response;
            }
        }
    }
}
