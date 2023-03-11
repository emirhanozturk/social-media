using Application.Abstracts.Storage.AzureStorage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Storage.AzureStorage
{
    public class AzureStorage :Storage, IAzureStorage
    {
        BlobContainerClient _blobContainerClient;
        readonly BlobServiceClient _blobServiceClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public List<string> GetAllFiles(string container)
        {
           _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            var files = _blobContainerClient.GetBlobs().Select(p=>p.Name).ToList();
            return files;
        }

        public bool IsExists(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            var file = _blobContainerClient.GetBlobs().Any(p => p.Name == fileName);
            return file;
        }

        public async Task RemoveAsync(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();

        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string container, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            var _ =await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string path)> values = new();
            foreach (var file in files)
            {
                var newFileName = await FileRenameAsync(container, file.Name, IsExists);

                var blobClient = _blobContainerClient.GetBlobClient(newFileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                values.Add((newFileName, $"{container}/{newFileName}"));
            }
            return values;
        }
    }
}
