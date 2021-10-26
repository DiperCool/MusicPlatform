using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.Common.Interfaces
{
    public interface ITestService
    {
        string GetTest();
        string SetTest(string test);
    }
}