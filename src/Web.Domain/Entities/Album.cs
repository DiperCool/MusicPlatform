using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Domain.Interfaces;

namespace Web.Domain.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Song> Songs { get; set; }= new List<Song>();
        public DateTime CreatedAt { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public PathToFile Picture { get; set; }
    }
}