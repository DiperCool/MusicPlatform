using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Songs.Commands.CreateSong;
using Web.WebUI.ExtensionsMethods;
using Web.WebUI.Models;

namespace Web.WebUI.Controllers
{
    public class SongController: ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromForm] CreateSongModel model )
        {
            CreateSongCommand command = new CreateSongCommand{ Title = model.Title, AlbumId = model.AlbumId,File = await model.File.ConvertToFileModelAsync()};
            return Ok(await Mediator.Send(command));
        }
    }
}