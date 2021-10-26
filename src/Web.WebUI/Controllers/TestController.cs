using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Security;
using Web.Application.Test.Commands.SetTest;
using Web.Application.Test.Queries.GetTest;

namespace Web.WebUI.Controllers
{
    public class TestController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetTest()
        {
            return await Mediator.Send(new GetTestQuery()); 
        }
        [HttpPost]
        public async Task<ActionResult<string>> SetTest([FromBody] SetTestCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}