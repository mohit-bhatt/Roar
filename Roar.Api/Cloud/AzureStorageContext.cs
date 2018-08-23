using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace Roar.Api.Cloud
{
    public interface IAzureStorageContext
    {
        CloudStorageAccount GetCloudStorageAccount();
        CloudBlobClient GetCloudBlobClient();
    }
    public class AzureStorageContext: IAzureStorageContext
    {
        private CloudStorageAccount _cloudStorageAccount;
        private CloudBlobClient _cloudBlobClient;

        public AzureStorageContext()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["roartable_AzureStorageConnectionString"]);
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();

        }
        public CloudStorageAccount GetCloudStorageAccount()
        {
            return _cloudStorageAccount;
        }

        public CloudBlobClient GetCloudBlobClient()
        {
            return _cloudBlobClient;
        }
    }
}