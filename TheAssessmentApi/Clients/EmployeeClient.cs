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
    class EmployeeClient : BaseHttp
    {
        public EmployeeClient(string name, string password) : base(name, password) { }

        public bool CreateEmployee(string employeeName)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "employees");
            request.Content = new StringContent($"{{\"Name\":\"{employeeName}\"}}");
            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        public List<Employee> GetAllEmployees()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "employees");

            var response = SendRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            return JsonHelper.Deserialize<List<Employee>>(response.Content.ReadAsStringAsync().Result);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"employees/id/{employeeId}");

            var response = SendRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            return JsonHelper.Deserialize<Employee>(response.Content.ReadAsStringAsync().Result);
        }

        public bool DeleteCompanyById(int employeeId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"employees/id/{employeeId}");
            var response = SendRequest(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
    }
}
