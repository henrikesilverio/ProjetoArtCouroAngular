using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace ProjetoArtCouro.Test.API.Infra
{
    public sealed class ServiceRequest : IDisposable
    {
        private readonly TestServer _testServer;

        public ServiceRequest(TestServer testServer)
        {
            _testServer = testServer;
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }

        public HttpResponseMessage Post(object objectParameter, string apiEndPoint)
        {
            return ExecuteAction(objectParameter, apiEndPoint, HttpMethod.Post);
        }

        public HttpResponseMessage Put(object objectParameter, string apiEndPoint)
        {
            return ExecuteAction(objectParameter, apiEndPoint, HttpMethod.Put);
        }

        public HttpResponseMessage Get(string apiEndPoint)
        {
            return ExecuteAction(null, apiEndPoint, HttpMethod.Get);
        }

        public HttpResponseMessage Get(object objectParameter, string apiEndPoint)
        {
            return ExecuteAction(objectParameter, apiEndPoint, HttpMethod.Get);
        }

        public HttpResponseMessage Delete(string apiEndPoint)
        {
            return ExecuteAction(null, apiEndPoint, HttpMethod.Delete);
        }

        private HttpResponseMessage ExecuteAction(object objectParameter, string apiEndPoint, HttpMethod method)
        {
            var serviceUrl = "http://localhost/";
            if (method == HttpMethod.Get && objectParameter != null)
            {
                apiEndPoint = AppendQueryString(apiEndPoint, objectParameter);
            }

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(serviceUrl + apiEndPoint),
                Method = method
            };

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                var json = JsonConvert.SerializeObject(objectParameter);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var token = GetAuthenticationToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = _testServer.HttpClient.SendAsync(request, CancellationToken.None).Result;

            return response;
        }

        private string AppendQueryString(string apiEndPoint, object objectParameter)
        {
            var endpointWithQueryString = new StringBuilder(apiEndPoint);
            if (!apiEndPoint.Contains("?"))
            {
                endpointWithQueryString.Append("?");
            }
            else if (!apiEndPoint.EndsWith("?"))
            {
                endpointWithQueryString.Append("&");
            }

            var properties = objectParameter.GetType().GetProperties();

            foreach (var prop in properties)
            {
                string value;
                if (prop.PropertyType == typeof(DateTime))
                {
                    value = ((DateTime)prop.GetValue(objectParameter)).ToString("o");
                }
                else if (prop.PropertyType == typeof(DateTime?))
                {
                    value = ((DateTime?)prop.GetValue(objectParameter))?.ToString("o");
                }
                else
                {
                    value = prop.GetValue(objectParameter)?.ToString();
                }

                value = value ?? string.Empty;

                endpointWithQueryString.Append(prop.Name);
                endpointWithQueryString.Append('=');
                endpointWithQueryString.Append(value);
                if (prop != properties.Last())
                {
                    endpointWithQueryString.Append("&");
                }
            }

            return endpointWithQueryString.ToString();
        }

        public string GetAuthenticationToken()
        {
            var serviceUrl = "http://localhost/api/security/token";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(serviceUrl),
                Method = HttpMethod.Post
            };

            var parameters = $"grant_type=password&username=Admin&password=admin";
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            request.Content = new StringContent(parameters);

            var response = _testServer.HttpClient.SendAsync(request, CancellationToken.None).Result;
            if (response.Content == null)
            {
                return null;
            }

            var content = response.Content.ReadAsAsync<TokenModel>().Result;
            var token = content.access_token;
            return string.IsNullOrEmpty(token) ? null : token;
        }
    }
}
