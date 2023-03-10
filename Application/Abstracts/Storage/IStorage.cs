using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);

        Task RemoveAsync(string path,string fileName );

        List<string> GetAllFiles(string path);

        bool IsExists(string path,string fileName);
    }
}
