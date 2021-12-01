using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Interfaces;

namespace Web.Domain.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public PathToFile File { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}