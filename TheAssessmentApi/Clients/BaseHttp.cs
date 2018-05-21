using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TheAssessmentApi.Models;

namespace TheAssessmentApi.Clients
{
    public abstract class BaseHttp: IDisposable
    {
        private const string BaseAddress = "https://mobilewebserver9-pokertest8ext.installprogram.eu/TestApi/";
        
        private HttpClient client;

        public BaseHttp(string name, string password)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            InitBearerToken(name, password);
        }

        public void InitBearerToken(string name, string password)
        {
            var body = new Dictionary<string, string>();
            body.Add("username", name);
            body.Add("password", password);
            body.Add("grant_type", "password");

            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress + "/token");
            request.Content = new FormUrlEncodedContent(body);
            var tokenResult = SendRequest(request);
            var tokenData = tokenResult.Content.ReadAsStringAsync().Result;

            var token = JsonHelper.Deserialize<TokenResponse>(tokenData);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }

        public void Dispose()
        {
            if(client != null)
            {
                client.Dispose();
            }
        }

        protected bool Create(string resource, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/automation/{resource}");
            request.Content = new StringContent($"{{\"Name\":\"{name}\"}}");

            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        protected List<T> GetAll<T>(string resource)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/automation/{resource}");

            var response = SendRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            return JsonHelper.Deserialize<List<T>>(response.Content.ReadAsStringAsync().Result);
        }

        protected T GetById<T>(string resource, int id) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/automation/{resource}/id/{id}");

            var response = SendRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            return JsonHelper.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
        }

        protected bool DeleteById(string resource, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/automation/{resource}/id/{id}");
            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }

        private HttpResponseMessage SendRequest(HttpRequestMessage message)
        {
            return client.SendAsync(message).Result;
        }
    }
}
