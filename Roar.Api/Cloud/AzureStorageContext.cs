using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
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
        private readonly string _databaseId = string.Empty;
        private readonly string _collectionId = string.Empty;
        private DocumentClient _cosmosClient;        

        public AzureStorageContext()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["roartable_AzureStorageConnectionString"]);
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            //_databaseId = ConfigurationManager.AppSettings["roarcosmosdbEndpoint"];
            _collectionId = "enrollment";
            Uri endpointUri = new Uri(ConfigurationManager.AppSettings["roarcosmosdbEndpoint"]);

            _cosmosClient = new DocumentClient(endpointUri, ConfigurationManager.AppSettings["roarcosmosdbAuthKey"]);
        }
        public CloudStorageAccount GetCloudStorageAccount()
        {
            return _cloudStorageAccount;
        }

        public CloudBlobClient GetCloudBlobClient()
        {
            return _cloudBlobClient;
        }
        public string GetCosmosCollectionId()
        {
            return _collectionId;
        }
        public DocumentClient GetCosmosDocumentClient()
        {
            return _cosmosClient;
        }
    }
}