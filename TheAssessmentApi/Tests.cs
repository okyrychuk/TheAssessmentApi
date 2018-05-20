using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TheAssessmentApi.Models;

namespace TheAssessmentApi
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetCompanyTest()
        {
            var response = new Company
            {
                id = 1,
                Name = "TestEmployee",
            };
            string serialized = JsonHelper.Serialize(response);

            Company resp = JsonHelper.DeserializeToModel<Company>(serialized);

        }
    }
}
