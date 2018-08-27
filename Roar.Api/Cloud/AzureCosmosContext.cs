using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Roar.Api.Cloud
{
    public interface IAzureCosmosContext
    {
        string GetCosmosCollectionId();
        DocumentClient GetCosmosDocumentClient();
    }
    public class AzureCosmosContext
    {
        private readonly string _databaseId = string.Empty;
        private readonly string _collectionId = string.Empty;
        private DocumentClient _cosmosClient;
        public AzureCosmosContext()
        {
            _collectionId = ConfigurationManager.AppSettings["cosmosCollection"];
            Uri endpointUri = new Uri(ConfigurationManager.AppSettings["roarcosmosdbEndpoint"]);
            _cosmosClient = new DocumentClient(endpointUri, ConfigurationManager.AppSettings["roarcosmosdbAuthKey"]);
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