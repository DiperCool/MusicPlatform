using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Models;
using Web.Domain.Entities;

namespace Web.Application.Common.Interfaces
{
    public interface IFileService
    {
        PathToFile SaveFile(FileModel model);
        string GetWebRootPath();
    }
}