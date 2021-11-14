using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Songs.Queries.GetFullPathesSong;
namespace Web.WebUI.Controllers
{
    public class PlaySongController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> PlaySong([FromQuery] GetFullPathesSongQuery query)
        {
            string fullPath = await Mediator.Send(query);
            return File(System.IO.File.ReadAllBytes(fullPath), "audio/mp3",enableRangeProcessing: true);
        }
    }
}