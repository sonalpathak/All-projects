using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace Neudesic.Schoolistics.WebRole.Filters
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private CloudStorageAccount cloudStorageAccount;

        private static CloudTableClient tableClient;

        private static CloudTable table;

        private const string Realm = "127.0.0.1:81";

        public void Init(HttpApplication context)
        {
            if (RoleEnvironment.IsAvailable)
                cloudStorageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = cloudStorageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.User);
            table.CreateIfNotExists();

            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        // TODO: Here is where you would validate the username and password.
        private static bool CheckPassword(string username, string password)
        {
            if (table.CreateQuery<UserInfo>().Where(p => p.PartitionKey.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
            {
                var user = table.CreateQuery<UserInfo>().Where(p => p.PartitionKey.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (!user.IsSocialUser)
                {
                    var tempPassword = BCrypt.Net.BCrypt.HashPassword(password, user.Salt);

                    if (user.Password == tempPassword)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return false;
        }

        private static bool AuthenticateUser(string credentials)
        {
            bool validated = false;
            try
            {
                credentials = Crypto.DecryptStringAES(credentials);

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                validated = CheckPassword(name, password);
                if (validated)
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                validated = false;

            }
            return validated;
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        // If the request was unauthorized, add the WWW-Authenticate header 
        // to the response.
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("WWW-Authenticate",

                    string.Format("xBasic realm=\"{0}\"", Realm));
            }
        }

        public void Dispose()
        {
        }
    }
}