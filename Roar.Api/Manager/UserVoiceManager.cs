using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Roar.Api.Cloud;
using Roar.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roar.Api.Manager
{
    public class UserVoiceManager: IUserVoiceManager
    {
        public string SaveVoiceData(byte[] voicedata, string fileName)
        {            
            CloudBlobContainer container = getContainerRefernce();
            container.CreateIfNotExistsAsync();
            var fileBlob = container.GetBlockBlobReference(fileName);
            fileBlob.UploadFromByteArrayAsync(voicedata, 0, voicedata.Length);            
            return fileBlob.Uri.ToString();
        }

        public string SaveUserVoiceData(EnrollmentModel item)
        {
            AzureStorageContext azureStorageContext = new AzureStorageContext();
            DocumentClient client = azureStorageContext.GetCosmosDocumentClient();
            string cosmosCollectionId = azureStorageContext.GetCosmosCollectionId();
            //client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, cosmosCollectionId));
            //client.CreateDocumentAsync(cosmosCollectionId, item);
            var result = client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("roardb", cosmosCollectionId), item);
            return "ok";
            
        }

        private CloudBlobContainer getContainerRefernce()
        {
            AzureStorageContext azureStorageContext = new AzureStorageContext();
            CloudStorageAccount storageAccount = azureStorageContext.GetCloudStorageAccount();
            CloudBlobClient blobClient = azureStorageContext.GetCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("roaraudio");            
            return container;
        }
    }
}