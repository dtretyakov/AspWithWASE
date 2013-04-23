using System.Web.Http;
using AspWithAzureExtensions.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace AspWithAzureExtensions.App_Start
{
    public static class TablesInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            var tableClient = (CloudTableClient) config.DependencyResolver.GetService(typeof (CloudTableClient));

            // Initialize required tables
            tableClient.GetTableReference(typeof (Issue).Name).CreateIfNotExists();
        }
    }
}