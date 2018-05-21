using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAssessmentApi.Clients;

namespace TheAssessmentApi.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        public const string Name = "olgaK";
        public const string Password = "password";
        private static EmployeeClient client;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            client = new EmployeeClient(Name, Password);
        }

        [TestInitialize]
        public void TestSetup()
        {
            var employees = client.GetAllEmployees();
            foreach (var employee in employees)
            {
                client.DeleteEmployeeById(employee.id);
            }
        }

        [TestMethod]
        public void CreateEmployeePositiveTest()
        {
            var newEmployee = "TestEmployee";
            var result = client.CreateEmployee(newEmployee);

            var employees = client.GetAllEmployees();
            Assert.IsTrue(result, "Employee was not created.");
            Assert.IsTrue(employees.Any(c => c.Name.Equals(newEmployee)), $"Employee name differs from expected");
        }

        [TestMethod]
        public void CreateEmployeeWithSameNameTest()
        {
            List<string> employeeNames = new List<string>() { "TestEmployee1", "TestEmployee1" };
            foreach (var employeeName in employeeNames)
            {
                client.CreateEmployee(employeeName);
            }
            var employees = client.GetAllEmployees();

            Assert.IsTrue(employees.Count == 1, "Cannot add same employee twice");
        }

        [TestMethod]
        public void CreateEmployeeNegativeTest()
        {
            string newEmployee = null;
            var result = client.CreateEmployee(newEmployee);
            Assert.IsFalse(result, "Employee should not be created");
        }

        [TestMethod]
        public void GetByIdEmployeePositiveTest()
        {
            List<string> employeeNames = new List<string>() { "TestEmployee1", "TestEmployee2" };
            foreach (var employeeName in employeeNames)
            {
                client.CreateEmployee(employeeName);
            }
            var employees = client.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var actualemployee = client.GetEmployeeById(employee.id);

            Assert.IsTrue(actualemployee.Name == employee.Name, $"Employee id differs from expected");
        }

        [TestMethod]
        public void GetByIdEmployeeNegativeTest()
        {
            var response = client.GetEmployeeById(-1);

            Assert.IsNull(response, "Response should be null for invalid index");
        }

        [TestMethod]
        public void GetAllEmployeesPositiveTest()
        {
            List<string> employeeNames = new List<string>() { "TestEmployee1", "TestEmployee2" };
            foreach (var employeeName in employeeNames)
            {
                client.CreateEmployee(employeeName);
            }
            var employees = client.GetAllEmployees();

            Assert.IsTrue(employees.Count == 2, "Response should return 2 employees");
        }

        [TestMethod]
        public void GetAllEmployeesIfEmptyTest()
        {
            var employees = client.GetAllEmployees();

            Assert.IsTrue(employees.Count == 0, "Response list should be empty");
        }

        [TestMethod]
        public void DeleteByIdEmployeePositiveTest()
        {
            List<string> employeeNames = new List<string>() { "TestEmployee1", "TestEmployee2" };
            foreach (var employeeName in employeeNames)
            {
                client.CreateEmployee(employeeName);
            }
            var employees = client.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var employeeDeleted = client.DeleteEmployeeById(employee.id);
            var employeesAfterDelete = client.GetAllEmployees();

            Assert.IsTrue(employeeDeleted, $"Employee {employee.id} should be deleted");
            Assert.IsTrue(employeesAfterDelete.Count == 1, $"One employee should be left after delete");
        }

        [TestMethod]
        public void DeleteByIdEmployeeNegativeTest()
        {
            var response = client.DeleteEmployeeById(-1);

            Assert.IsFalse(response, "Response shuld be false for invalid index");
        }
    }
}
