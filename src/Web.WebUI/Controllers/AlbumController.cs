using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Application.Albums.Commands.CreateAlbum;
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
    }
}