using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entities
{
    public class Artist : Account
    {
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}