using System;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace CRUDDotNet6.Services
{
	public class BlobStorage: IBlobStorage
	{
		
        public async Task<Stream> GetDocument(string connectionString, string containerName, string fileName)
        {
            try
            {
                var container = BlobExtensions.GetContainer(connectionString, containerName);
                if (await container.ExistsAsync())
                {
                    var blobClient = container.GetBlobClient(fileName);
                    if (blobClient.Exists())
                    {
                        var content = await blobClient.DownloadStreamingAsync();
                        return content.Value.Content;
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task UploadDocument(string connectionString, string containerName, string fileName, Stream fileContent)
        {
            try {
                var container = BlobExtensions.GetContainer(connectionString, containerName);
                if (!await container.ExistsAsync())
                {
                    BlobServiceClient blobServiceClient = new(connectionString);
                    await blobServiceClient.CreateBlobContainerAsync(containerName);
                    container = blobServiceClient.GetBlobContainerClient(containerName);
                }

                var blobclient = container.GetBlobClient(fileName);
                if (!blobclient.Exists())
                {
                    fileContent.Position = 0;
                    await container.UploadBlobAsync(fileName, fileContent);
                }
                else
                {
                    fileContent.Position = 0;
                    await blobclient.UploadAsync(fileContent, overwrite: true);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }

    public static class BlobExtensions
    {
        public static BlobContainerClient GetContainer(string connectionString, string containerName)
        {
            BlobServiceClient blobServiceClient = new(connectionString);
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}

