using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Mappings;
using Web.Domain.Entities;
using Web.Domain.Interfaces;

namespace Web.Application.Common.DTOs
{
    public class SubscriberDTO: IMapFrom<Subscriber>, IPaginated
    {
        public int Id { get; set; }
        public ArtistDTO Artist { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}