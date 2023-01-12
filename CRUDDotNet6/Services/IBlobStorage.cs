using System;
namespace CRUDDotNet6.Services
{
	public interface IBlobStorage
	{
        
        Task UploadDocument(string connectionString, string containerName, string fileName, Stream fileContent);
        Task<Stream> GetDocument(string connectionString, string containerName, string fileName);
        
    }
}

