using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web.Application.Common.Mappings;

namespace Web.Application.Common.DTOs;
public class ProfileDTO: IMapFrom<Web.Domain.Entities.Profile>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}