using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Mappings;
using Web.Domain.Entities;
using Web.Domain.Interfaces;

namespace Web.Application.Common.DTOs
{
    public class AlbumDTO: IMapFrom<Album>, IPaginated
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public ArtistDTO Artist { get; set; }
        public string Picture { get; set; }
        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Album, AlbumDTO>()
                .ForMember(dto=>dto.Picture, opt=>opt.MapFrom(s=>s.Picture.ShortPath));
        }
    }
}