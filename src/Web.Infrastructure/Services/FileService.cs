using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Models;
using Web.Domain.Entities;

namespace Web.Infrastructure.Services;
public class FileService : IFileService
{
    IWebHostEnvironment _host;

    public FileService(IWebHostEnvironment host)
    {
        _host = host;
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }

    public string GetWebRootPath() => _host.WebRootPath;

    public PathToFile SaveFile(FileModel model)
    {
        string guid = Guid.NewGuid().ToString();
        string extensionsFile = Path.GetExtension(model.nameFile);
        string shortPath = Path.Combine(Path.Combine("api","Files"), guid+extensionsFile);
        string fullPath = Path.Combine(GetWebRootPath(), shortPath);
        File.WriteAllBytes(fullPath,model.bytes);
        return new PathToFile {
            ShortPath = shortPath,
            FullPath = fullPath
        };
    }
}