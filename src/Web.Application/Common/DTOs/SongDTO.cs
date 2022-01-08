using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Mappings;
using Web.Domain.Entities;
using Web.Domain.Interfaces;

namespace Web.Application.Common.DTOs;
public class SongDTO: IMapFrom<Song>, IPaginated
{
    public int Id { get; set; }
    public string Title { get; set; }
    public AlbumDTO Album { get; set; }
}