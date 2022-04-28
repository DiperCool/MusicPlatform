using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set;}
        public int ListenerId { get; set; }
        public Listener Listener { get; set; }
        public DateTime LikedAt { get; set; }
    }
}