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
        private const string Companies = "companies";
        public CompanyClient(string name, string password) : base(name, password) { }

        public bool CreateCompany(string companyName)
        {
            return Create(Companies, companyName);
        }

        public List<Company> GetAllCompanies()
        {
            return GetAll<Company>(Companies);
        }

        public Company GetCompanyById(int companyId)
        {
            return GetById<Company>(Companies, companyId);
        }

        public bool DeleteCompanyById(int companyId)
        {
            return DeleteById(Companies, companyId);
        }
    }
}
