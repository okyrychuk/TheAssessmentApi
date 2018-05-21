using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TheAssessmentApi.Models;

namespace TheAssessmentApi.Clients
{
    public class CompanyClient : BaseHttp
    {
        public CompanyClient(string name, string password) : base(name, password) { }

        public bool CreateCompany(string companyName)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/automation/companies");
            request.Content = new StringContent($"{{\"Name\":\"{companyName}\"}}");

            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        public List<Company> GetAllCompanies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/automation/companies");

            var response = SendRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            return JsonHelper.Deserialize<List<Company>>(response.Content.ReadAsStringAsync().Result);
        }

        public Company GetCompanyById(int companyId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/automation/companies/id/{companyId}");

            var response = SendRequest(request);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            return JsonHelper.Deserialize<Company>(response.Content.ReadAsStringAsync().Result);
        }

        public bool DeleteCompanyById(int companyId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/automation/companies/id/{companyId}");
            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
    }
}
