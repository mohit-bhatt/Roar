﻿using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Roar.Api.Cloud;
using Roar.Api.Models;
using System.Collections.Generic;
using System.Linq;
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
            fileBlob.UploadFromByteArray(voicedata, 0, voicedata.Length);            
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

        public string SaveUserVoiceData(EmployeeEnrollment employeeEnrollment)
        {
            AzureCosmosContext azureCosmosContext = new AzureCosmosContext();
            DocumentClient client = azureCosmosContext.GetCosmosDocumentClient();
            string cosmosCollectionId = azureCosmosContext.GetCosmosCollectionId();
            var result = client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("roardb", cosmosCollectionId), employeeEnrollment).Result;
            return result.ActivityId;
        }

        public EmployeeEnrollment GetEmployeeEnrollment(string enrollmentId)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            AzureCosmosContext azureCosmosContext = new AzureCosmosContext();
            DocumentClient client = azureCosmosContext.GetCosmosDocumentClient();
            string cosmosCollectionId = azureCosmosContext.GetCosmosCollectionId();

            List<EmployeeEnrollment> familyQuery = client.CreateDocumentQuery<EmployeeEnrollment>(
                UriFactory.CreateDocumentCollectionUri("roardb", cosmosCollectionId), queryOptions)
                .Where(f => f.EnrollmentId == enrollmentId).ToList();

            return familyQuery.FirstOrDefault();
        }
    }
}