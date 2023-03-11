using Application.Abstracts.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {

        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName{ get => _storage.GetType().Name; }

        public List<string> GetAllFiles(string path)
        {
            var files = _storage.GetAllFiles(path);
            return files;
        }

        public bool IsExists(string path, string fileName)
        {
            return _storage.IsExists(path, fileName);
        }

        public async Task RemoveAsync(string path, string fileName)
        {
            await _storage.RemoveAsync(path, fileName);
        }

        public Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
           var datas = _storage.UploadAsync(path, files);
            return datas;
        }
    }
}
