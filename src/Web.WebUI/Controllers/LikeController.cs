using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Likes.Commands.LikeSong;
using Web.Application.Likes.Commands.RemoveLikeSong;
using Web.Application.Likes.Queries.GetLikes;

namespace Web.WebUI.Controllers
{
    public class LikeController: ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Like([FromBody]LikeSongCommand command)
        {
            
            return Ok(await Mediator.Send(command));

        }
        [HttpGet]
        public async Task<IActionResult> GetLikes([FromQuery]GetLikesQuery command)
        {

            return Ok(await Mediator.Send(command));

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLike([FromBody] RemoveLikeSongCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}