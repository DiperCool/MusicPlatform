using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Songs.Commands.CreateSong;
using Web.Application.Songs.Commands.DeleteSong;
using Web.Application.Songs.Commands.UpdateSong;
using Web.Application.Songs.Queries.GetSongs;
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

        [HttpPut]
        public async Task<IActionResult> UpdateSong([FromForm] UpdateSongModel model)
        {
            UpdateSongCommand command = new UpdateSongCommand{ Title = model.Title, SongId = model.SongId,File = await model.File.ConvertToFileModelAsync()};
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSong([FromBody] DeleteSongCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs([FromQuery] GetSongsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}