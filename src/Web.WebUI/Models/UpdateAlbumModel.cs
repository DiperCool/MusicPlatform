using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Application.Common.Models;

namespace Web.WebUI.Models
{
    public class UpdateAlbumModel
    {
        public int AlbumId{ get ;set; }
        public string Titile { get; set;}
        public IFormFile File { get; set; }
    }
}