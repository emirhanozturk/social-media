using Application.Abstracts.Storage.LocalStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Storages.LocalStorage
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<string> GetAllFiles(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            var files = directoryInfo.GetFiles().Select(p => p.Name);
            return files.ToList();
        }

        public bool IsExists(string path, string fileName)
        {
            var isExists = File.Exists($"{path}\\{fileName}");
            return isExists;
        }

        public async Task RemoveAsync(string path, string fileName)
        {
            File.Delete($"{path}\\{fileName}");
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                bool result = await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
                datas.Add((file.Name, $"{path}\\{file.Name}"));
            }



            return datas;
        }

         async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

    }
}
