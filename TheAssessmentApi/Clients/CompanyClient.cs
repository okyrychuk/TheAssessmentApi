using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TheAssessmentApi.Models;

namespace TheAssessmentApi
{
    class CompanyClient : BaseHttp
    {
        public HttpResponseMessage CreateCompany(string companyName)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/companies");
            request.Content = new StringContent($"{{\"Name\":{companyName}}}");

            return SendRequest(request);
        }

        public List<Company> GetAllCompanies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/companies");

            var response = SendRequest(request);
            return JsonHelper.Deserialize<List<Company>>(response.Content.ReadAsStringAsync().Result);
        }

        public Company GetCompanyById(int companyId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/companies/id/{companyId}");

            var response = SendRequest(request);
            return JsonHelper.Deserialize<Company>(response.Content.ReadAsStringAsync().Result);
        }

        public HttpResponseMessage DeleteCompanyById(int companyId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/companies/id/{companyId}");

            return SendRequest(request);
        }
    }
}
