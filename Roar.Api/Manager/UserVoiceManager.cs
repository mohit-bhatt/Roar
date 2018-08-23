using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Roar.Api.Cloud;

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