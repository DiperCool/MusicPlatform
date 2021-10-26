using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;

namespace Web.Infrastructure.Services
{
    public class TestService : ITestService
    {
        public string GetTest()
        {
            return "test";
        }

        public string SetTest(string test)
        {
            return $"set {test}";
        }
    }
}