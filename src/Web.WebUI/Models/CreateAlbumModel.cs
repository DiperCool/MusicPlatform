using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.WebUI.Models;
public class CreateAlbumModel
{
    public string Title { get; set; }
    public IFormFile File { get; set; }
}