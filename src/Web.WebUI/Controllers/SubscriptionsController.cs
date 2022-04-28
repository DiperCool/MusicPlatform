using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Subscribers.Commands.SubscribeArtist;
using Web.Application.Subscribers.Commands.UnsubscribeArtist;
using Web.Application.Subscribers.Queries.GetSubscriptions;

namespace Web.WebUI.Controllers
{
    public class SubscriptionsController:ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody]SubscribeArtistCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> Unsubscribe([FromBody]UnsubscribeArtistCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetSubscriptions([FromQuery]GetSubscriptionsQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}