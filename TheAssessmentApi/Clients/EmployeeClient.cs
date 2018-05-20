using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TheAssessmentApi.Models;

namespace TheAssessmentApi.Clients
{
    class EmployeeClient : BaseHttp
    {
        public HttpResponseMessage CreateEmployee(string employeeName)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/employees");
            request.Content = new StringContent($"{{\"Name\":{employeeName}}}");

            return SendRequest(request);
        }

        public List<Employee> GetAllEmployees()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/employees");

            var response = SendRequest(request);
            return JsonHelper.Deserialize<List<Employee>>(response.Content.ReadAsStringAsync().Result);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/employees/id/{employeeId}");

            var response = SendRequest(request);
            return JsonHelper.Deserialize<Employee>(response.Content.ReadAsStringAsync().Result);
        }

        public HttpResponseMessage DeleteCompanyById(int employeeId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/employees/id/{employeeId}");

            return SendRequest(request);
        }
    }
}
