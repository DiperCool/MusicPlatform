using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Application.Common.Models;

namespace Web.WebUI.ExtensionsMethods;
public static class ConvertatorFormFile
{
    public static async Task<FileModel> ConvertToFileModelAsync(this IFormFile file)
    {
        if(file==null) return null;
        var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);
        FileModel fileModel = new FileModel{ nameFile = file.FileName, bytes= memoryStream.ToArray() };
        return fileModel;
    }
}
