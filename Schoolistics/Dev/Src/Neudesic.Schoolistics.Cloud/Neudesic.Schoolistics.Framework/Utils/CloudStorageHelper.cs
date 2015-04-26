using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Utils
{
    public static class CloudStorageHelper
    {
        public static CloudBlobContainer GetBlobContainer(string containerName)
        {
            CloudStorageAccount cloudStorageAccount;
            // Retrieve storage account from connection-string
            if (RoleEnvironment.IsAvailable)
                cloudStorageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client 
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            
            // Retrieve a reference to a container 

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist
            container.CreateIfNotExists();

            return container;
        }

        public static bool Exists(this ICloudBlob blob)
        {
            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
