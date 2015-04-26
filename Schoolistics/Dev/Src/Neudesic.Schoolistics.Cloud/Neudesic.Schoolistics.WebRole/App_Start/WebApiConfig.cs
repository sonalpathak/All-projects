using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.Repositories;
using Neudesic.Schoolistics.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Neudesic.Schoolistics.WebRole.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterRepositories(HttpConfiguration config)
        {
            //Dependency Injection

            var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISchoolRepository, SchoolRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookmarkedSchoolsRepository, BookmarkedSchoolsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISchoolsComparisonRepository, SchoolsComparisonRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISearchRepository, SearchRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserComparisonsRepository, UserComparisonsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserSchoolRatingRepository, UserSchoolRatingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserSchoolCommentRepository, UserSchoolCommentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISurveyRepository, SurveyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserSurveyDetailsRepository, UserSurveyDetailsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<INewsArticlesRepository, NewsArticlesRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }

        public static void CreateTables()
        {
            CloudStorageAccount storageAccount;
            CloudTableClient tableClient;
            CloudTable table;


            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            //table = tableClient.GetTableReference(TableNames.UserSchoolsComparisons);
            //table.CreateIfNotExists();

            //table = tableClient.GetTableReference(TableNames.BookmarkedSchools);
            //table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.School);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.SchoolsComparison);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.Search);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.User);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.NewsArticle);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.Survey);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.UserSchoolComment);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.UserSchoolRating);
            table.CreateIfNotExists();

            table = tableClient.GetTableReference(TableNames.UserSurveyDetails);
            table.CreateIfNotExists();

        }
    }
}