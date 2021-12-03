using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Application.Albums.Commands.CreateAlbum;
using Web.Application.Albums.Commands.DeleteAlbum;
using Web.Application.Albums.Commands.UpdatePicturesAlbum;
using Web.Application.Albums.Queries.GetAlbum;
using Web.Application.Albums.Queries.GetAlbums;
using Web.Application.Albums.Queries.HasAlbumArtist;
using Web.Application.Common.Models;
using Web.WebUI.ExtensionsMethods;
using Web.WebUI.Models;

namespace Web.WebUI.Controllers
{
    
    public class AlbumController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromForm] CreateAlbumModel model)
        {

            

            CreateAlbumCommand command = new CreateAlbumCommand{ Title = model.Title, File = await model.File.ConvertToFileModelAsync()};
            return Ok(await Mediator.Send(command));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlbum([FromForm] UpdateAlbumModel model )
        {
            UpdateAlbumCommand command = new UpdateAlbumCommand(){AlbumId = model.AlbumId, File = await model.File.ConvertToFileModelAsync(), Title = model.Title};
            return Ok(await Mediator.Send(command));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAlbum([FromBody] DeleteAlbumCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums([FromQuery] GetAlbumsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet("{AlbumId}")]
        public async Task<IActionResult> GetAlbum(int AlbumId)
        {
            return Ok(await Mediator.Send(new GetAlbumQuery{ AlbumId = AlbumId}));
        }
        [HttpGet("HasAlbumArtist/{AlbumId}")]
        public async Task<IActionResult> HasAlbumArtist(int AlbumId)
        {
            return Ok(await Mediator.Send(new HasAlbumArtistQuery{ AlbumId = AlbumId}));
        }

    }
}