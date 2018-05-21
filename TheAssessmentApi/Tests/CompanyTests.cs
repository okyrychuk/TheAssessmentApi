using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TheAssessmentApi.Clients;

namespace TheAssessmentApi.Tests
{
    [TestClass]
    public class CompanyTests
    {
        public const string Name = "olgaK";
        public const string Password = "password";
        private static CompanyClient client;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            client = new CompanyClient(Name, Password);
        }

        [TestInitialize]
        public void TestSetup()
        {
            var companies = client.GetAllCompanies();
            foreach(var company in companies)
            {
                client.DeleteCompanyById(company.id);
            }
        }

        [TestMethod]
        public void CreateCompanyPositiveTest()
        {
            var newCompany = "TestCompany";
            var result = client.CreateCompany(newCompany);

            var companies = client.GetAllCompanies();
            Assert.IsTrue(result, "Company was not created.");
            Assert.IsTrue(companies.Any(c => c.Name.Equals(newCompany)), $"Company name differs from expected");
        }

        [TestMethod]
        public void CreateCompanyWithSameNameTest()
        {
            List<string> companyNames = new List<string>() { "TestCompany1", "TestCompany1" };
            foreach (var companyName in companyNames)
            {
                client.CreateCompany(companyName);
            }
            var companies = client.GetAllCompanies();

            Assert.IsTrue(companies.Count == 1, "Cannot add same company twice");
        }

        [TestMethod]
        public void CreateCompanyNegativeTest()
        {
            string newCompany = null;
            var result = client.CreateCompany(newCompany);
            Assert.IsFalse(result, "Company should not be created");
        }

        [TestMethod]
        public void GetByIdCompanyPositiveTest()
        {
            List<string> companyNames = new List<string>() { "TestCompany1", "TestCompany2" };
            foreach (var companyName in companyNames)
            {
                client.CreateCompany(companyName);
            }
            var companies = client.GetAllCompanies();
            var company = companies.FirstOrDefault();
            var actualcompany = client.GetCompanyById(company.id);

            Assert.IsTrue(actualcompany.Name == company.Name, $"Company id differs from expected");
        }

        [TestMethod]
        public void GetByIdCompanyNegativeTest()
        {
            var response = client.GetCompanyById(-1);

            Assert.IsNull(response, "Response should be null for invalid index");
        }

        [TestMethod]
        public void GetAllCompaniesPositiveTest()
        {
            List<string> companyNames = new List<string>() { "TestCompany1", "TestCompany2" };
            foreach (var companyName in companyNames)
            {
                client.CreateCompany(companyName);
            }
            var companies = client.GetAllCompanies();

            Assert.IsTrue(companies.Count == 2, "Response should return 2 companies");
        }

        [TestMethod]
        public void GetAllCompaniesIfEmptyTest()
        {
            var companies = client.GetAllCompanies();

            Assert.IsTrue(companies.Count == 0, "Response list should be empty");
        }

        [TestMethod]
        public void DeleteByIdCompanyPositiveTest()
        {
            List<string> companyNames = new List<string>() { "TestCompany1", "TestCompany2" };
            foreach (var companyName in companyNames)
            {
                client.CreateCompany(companyName);
            }
            var companies = client.GetAllCompanies();
            var company = companies.FirstOrDefault();
            var companyDeleted = client.DeleteCompanyById(company.id);
            var companiesAfterDelete = client.GetAllCompanies();

            Assert.IsTrue(companyDeleted, $"Company {company.id} should be deleted");
            Assert.IsTrue(companiesAfterDelete.Count == 1, $"One company should be left after delete");
        }

        [TestMethod]
        public void DeleteByIdCompanyNegativeTest()
        {
            var response = client.DeleteCompanyById(-1);

            Assert.IsFalse(response, "Response shuld be false for invalid index");
        }
    }
}
