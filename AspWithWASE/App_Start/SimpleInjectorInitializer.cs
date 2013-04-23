using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using AspWithAzureExtensions.App_Start;
using AspWithAzureExtensions.Infrastructure;
using AspWithAzureExtensions.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using WebActivator;
using WindowsAzure.Table;

[assembly: PreApplicationStartMethod(typeof (SimpleInjectorInitializer), "Initialize")]

namespace AspWithAzureExtensions.App_Start
{
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
            var container = new Container();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterMvcAttributeFilterProvider();

            container.Verify();

            // MVC Resolver
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // Web API Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            // Azure storage account
            container.Register(() =>
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString;
                    return CloudStorageAccount.Parse(connectionString);
                });

            // Azure table client
            container.Register(() => container.GetInstance<CloudStorageAccount>().CreateCloudTableClient());

            // TableSet for comments
            container.Register<ITableSet<Issue>>(() => new TableSet<Issue>(container.GetInstance<CloudTableClient>()));
        }
    }
}