using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entities
{
    public class PathToFile
    {
        public int Id { get; set; }
        public string FullPath { get; set; }
        public string ShortPath { get; set; }
    }
}