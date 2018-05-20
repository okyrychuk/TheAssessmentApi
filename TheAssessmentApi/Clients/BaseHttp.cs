using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheAssessmentApi.Models;

namespace TheAssessmentApi
{
    public abstract class BaseHttp
    {
        private const string BaseAddress = "https://mobilewebserver9-pokertest8ext.installprogram.eu/TestApi";

        private TokenResponse token;

        public BaseHttp()
        {
            token = GetToken();
        }

        public HttpResponseMessage SendRequest(HttpRequestMessage message)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", token.access_token));

                response = client.SendAsync(message).Result;
            }
            return response;
        }

        public TokenResponse GetToken()
        {
            var body = new Dictionary<string, string>();
            body.Add("username", "newTestName");
            body.Add("password", "password");
            body.Add("grant_type", "password");

            var request = new HttpRequestMessage(HttpMethod.Post, "/token");
            request.Content = new FormUrlEncodedContent(body);

            var tokenResult = SendRequest(request);
            var tokenData = tokenResult.Content.ReadAsStringAsync().Result;

            return JsonHelper.Deserialize<TokenResponse>(tokenData);
        }
    }
}
