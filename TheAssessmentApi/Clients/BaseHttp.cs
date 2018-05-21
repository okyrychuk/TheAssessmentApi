using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public HttpResponseMessage SendRequest(HttpRequestMessage message)
        {
            return client.SendAsync(message).Result;
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
    }
}
