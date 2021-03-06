using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CELA_Knowledge_Management_Data_Services.DataUtilities
{
    public class AzureTableDataUtilities
    {
        private static AzureTableDataUtilities instance = null;
        private static CloudStorageAccount storageAccount = null;
        private static CloudTable tagTable = null;


        private AzureTableDataUtilities()
        {
            //string storageAccountString = ConfigurationManager.AppSettings["TagStorageConnectionString"];
            // TODO change the constructor to take all necessary inputs
            string storageAccountString = "DefaultEndpointsProtocol=https;AccountName=[TABLE_NAME];AccountKey=[TABLE_KEY];EndpointSuffix=core.windows.net";
            if (storageAccountString != null)
            {
                storageAccount = CloudStorageAccount.Parse(storageAccountString);
                if (storageAccount != null)
                {
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    //var tableName = ConfigurationManager.AppSettings["TagStorageTableName"];
                    var tableName = "[TABLE_NAME]";
                    if (tableName != null)
                    {
                        tagTable = tableClient.GetTableReference(tableName);
                    }
                    else
                    {
                        throw new Exception("TagStorageTableName not found in AppSettings.");
                    }
                }
            }
            else
            {
                throw new Exception("TagStorageConnectionString not found in AppSettings.");
            }
        }

        public static AzureTableDataUtilities GetInstance()
        {
            if (instance == null)
            {
                instance = new AzureTableDataUtilities();
            }

            return instance;
        }

        public static CloudStorageAccount GetStorageAccount()
        {
            GetInstance();
            return storageAccount;
        }

        public static CloudTable GetTagTable()
        {
            GetInstance();
            return tagTable;
        }
    }
}
