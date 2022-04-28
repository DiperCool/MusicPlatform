using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }

        public Listener Listener { get; set; }
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}