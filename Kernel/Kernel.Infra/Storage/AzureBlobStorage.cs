using Kernel.Domain.Model.Settings;
using Kernel.Domain.Model.Validation;
using Kernel.Domain.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kernel.Infra.Storage
{
    public class AzureBlobStorage : IBlobStorage
    {
        private readonly AppSettings _appSettings;
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorage(AppSettings appSettings)
        {
            if (string.IsNullOrWhiteSpace(appSettings?.StorageSettings?.ConnectionString))
                throw new ValidatorException("Não existe nenhum Azure Storage configurado");

            _appSettings = appSettings;
            _blobServiceClient = new BlobServiceClient(_appSettings.StorageSettings.ConnectionString);
        }

        public async Task CreateContainer(string containerName) => await CreateContainerIfNotExists(containerName);

        public async Task DeleteContainer(string containerName)
        {
            var containerClient = GetBlobContainerClient(containerName);
            await containerClient.DeleteIfExistsAsync();
        }

        private BlobContainerClient GetBlobContainerClient(string containerName) => 
            _blobServiceClient.GetBlobContainerClient(containerName);

        private async Task<BlobContainerClient> CreateContainerIfNotExists(string containerName)
        {
            var containerClient = GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        public async Task Upload(
            string containerName, 
            string fileName,
            Stream uploadFileStream,
            bool overwrite = true)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(uploadFileStream, overwrite);
        }

        public string GetBlobUrl(string containerName, string fileName)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var expiration = DateTimeOffset.Now.AddHours(_appSettings.StorageSettings.SasUriExpirantionHours);

            var uri = blobClient.GenerateSasUri(BlobSasPermissions.Read, expiration);

            return uri.ToString();
        }

        public async Task DeleteBlob(string containerName, string fileName)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
