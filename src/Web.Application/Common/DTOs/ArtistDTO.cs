using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Mappings;
using Web.Domain.Entities;

namespace Web.Application.Common.DTOs;
public class ArtistDTO : IMapFrom<Artist>
{
    public int Id { get; set; }
    public ProfileDTO Profile { get; set; }
}