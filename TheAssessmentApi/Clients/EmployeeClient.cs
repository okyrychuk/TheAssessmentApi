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
        private const string Employees = "employees";
        public EmployeeClient(string name, string password) : base(name, password) { }

        public bool CreateEmployee(string employeeName)
        {
            return Create(Employees, employeeName);
        }

        public List<Employee> GetAllEmployees()
        {
            return GetAll<Employee>(Employees);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return GetById<Employee>(Employees, employeeId);
        }

        public bool DeleteEmployeeById(int employeeId)
        {
            return DeleteById(Employees, employeeId);
        }
    }
}
