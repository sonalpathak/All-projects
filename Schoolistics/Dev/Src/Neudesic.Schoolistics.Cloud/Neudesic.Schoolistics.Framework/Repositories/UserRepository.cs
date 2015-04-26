using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public class UserRepository : IUserRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public UserRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.User);
            table.CreateIfNotExists();
        }

        public bool CreateUser(UserInfo user)
        {
            user.Created = DateTime.UtcNow;
            user.UserId = Guid.NewGuid().ToString();
            if (user.Password != null)
            {
                user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, user.Salt);
            }
            TableOperation insertOperation = TableOperation.Insert(user);
            
            table.Execute(insertOperation);

            return true;
        }

        public bool UpdateUser(UserProfileData user)
        {
            string oldPassword = null;

            TableOperation updateOperation;

            var userSearched = table.CreateQuery<UserInfo>().Where(p => p.PartitionKey == user.Username).FirstOrDefault();

            if (user.Password != null && user.OldPassword != null)
            {
                oldPassword = BCrypt.Net.BCrypt.HashPassword(user.OldPassword, userSearched.Salt);

                if (userSearched.Password == oldPassword)
                {
                    //if (user.FullName != null)
                    //    userSearched.FullName = user.FullName;
                    //else
                    //    userSearched.FullName = String.Empty;                    

                    if (user.DateOfBirth != null)
                        userSearched.DateOfBirth = user.DateOfBirth;
                    else
                        userSearched.DateOfBirth = String.Empty;

                    if (user.Image != null)
                        userSearched.Image = user.Image;
                    else
                        userSearched.Image = String.Empty;

                    //if (user.Education != null)
                    //    userSearched.Education = user.Education;
                    //else
                    //    userSearched.Education = String.Empty;

                    if (user.Email != null)
                        userSearched.Email = user.Email;
                    else
                        userSearched.Email = String.Empty;

                    if (user.Gender != null)
                        userSearched.Gender = user.Gender;
                    else
                        userSearched.Gender = String.Empty;

                    if (user.Occupation != null)
                        userSearched.Occupation = user.Occupation;
                    else
                        userSearched.Occupation = String.Empty;

                    if (user.PhoneNumber != null)
                        userSearched.PhoneNumber = user.PhoneNumber;
                    else
                        userSearched.PhoneNumber = String.Empty;

                    //if (user.UserType != null)
                    //    userSearched.UserType = user.UserType;
                    //else
                    //    userSearched.UserType = String.Empty;

                    //if (user.Address != null)
                    //    userSearched.Address = user.Address;
                    //else
                    //    userSearched.Address = String.Empty;


                    userSearched.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, userSearched.Salt);

                    updateOperation = TableOperation.Replace(userSearched);

                    table.Execute(updateOperation);

                    return true;
                }
                else
                    return false;
            }


            //if (user.FullName != null)
            //    userSearched.FullName = user.FullName;
            //else
            //    userSearched.FullName = String.Empty;

            if (user.DateOfBirth != null)
                userSearched.DateOfBirth = user.DateOfBirth;
            else
                userSearched.DateOfBirth = String.Empty;

            if (user.Image != null)
                userSearched.Image = user.Image;
            else
                userSearched.Image = String.Empty;

            //if (user.Education != null)
            //    userSearched.Education = user.Education;
            //else
            //    userSearched.Education = String.Empty;

            if (user.Email != null)
                userSearched.Email = user.Email;
            else
                userSearched.Email = String.Empty;

            if (user.Gender != null)
                userSearched.Gender = user.Gender;
            else
                userSearched.Gender = String.Empty;

            if (user.Occupation != null)
                userSearched.Occupation = user.Occupation;
            else
                userSearched.Occupation = String.Empty;

            if (user.PhoneNumber != null)
                userSearched.PhoneNumber = user.PhoneNumber;
            else
                userSearched.PhoneNumber = String.Empty;

            //if (user.UserType != null)
            //    userSearched.UserType = user.UserType;
            //else
            //    userSearched.UserType = String.Empty;

            //if (user.Address != null)
            //    userSearched.Address = user.Address;
            //else
            //    userSearched.Address = String.Empty;

            updateOperation = TableOperation.Replace(userSearched);

            table.Execute(updateOperation);
           

            return true;

        }

        public UserInfo FindByUsername(string username)
        {
            var user = table.CreateQuery<UserInfo>().Where(p => p.PartitionKey == username).FirstOrDefault();

            return user;
        }

        public UserInfo FindByEmail(string emailId)
        {
            var user = table.CreateQuery<UserInfo>().Where(p => p.Email == emailId).FirstOrDefault();

            return user;
        }

        public bool ValidateAccount(string username, string password)
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

        public string UpdatePassword(UserInfo user)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var newPassword = new string(
                            Enumerable.Repeat(chars, 8)
                            .Select(s => s[random.Next(s.Length)])
                                .ToArray());

            user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, user.Salt);

            TableOperation insertOperation = TableOperation.Replace(user);

            // Execute the insert operation.
            table.Execute(insertOperation);

            return newPassword;
        }
    }
}
