using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Mappings;
using Web.Domain.Entities;
using Web.Domain.Interfaces;

namespace Web.Application.Common.DTOs;

public class LikeDTO:IMapFrom<Like>,IPaginated
{
    public int Id { get; set; }
    public SongDTO Song { get; set; }
    public DateTime LikedAt { get; set; }
}