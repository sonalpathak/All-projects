using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Neudesic.Schoolistics.Framework.API_Models;
using Neudesic.Schoolistics.Framework.Models;
using Neudesic.Schoolistics.Framework.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public SchoolRepository()
        {
            if (RoleEnvironment.IsAvailable)
                storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            else
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            table = tableClient.GetTableReference(TableNames.School);
            table.CreateIfNotExists();
        }

        public List<School> GetSchoolsByName(string schoolName)
        {
            return table.CreateQuery<School>().Where(p => p.SchoolName == schoolName).ToList();
        }
        public List<Alumni> GetAlumniBySchoolId(string schoolId)
        {
            return table.CreateQuery<Alumni>().Where(p => p.SchoolId == schoolId).ToList();
        }
        public List<SchoolManagement> GetSchoolManagement(string schoolId)
        {
            return table.CreateQuery<SchoolManagement>().Where(p => p.SchoolId == schoolId).ToList();
        }

        public School GetSchoolByID(string id)
        {
            return table.CreateQuery<School>().Where(p => p.RowKey == id).FirstOrDefault();
        }

        public School GetSchoolByIDAndCity(string city, string schoolID)
        {
            return table.CreateQuery<School>().Where(p => p.PartitionKey == city && p.RowKey == schoolID).FirstOrDefault();
        }

        public List<School> GetFeaturedSchools()
        {
            return table.CreateQuery<School>().Where(p => p.IsFeatured == true).ToList();
        }

        public List<School> GetAllSchools()
        {
            return table.CreateQuery<School>().ToList().OrderBy(p=>p.SchoolName).ToList();
        }

        public List<School> SearchSchoolsByName(string keyword)
        {
            return table.CreateQuery<School>().Where(p=>p.SchoolName.Contains(keyword)).ToList().OrderBy(p=>p.SchoolName).ToList();
        }

        public School RateSchool(UserSchoolRating rating)
        {
            var school = table.CreateQuery<School>().Where(p => p.PartitionKey == rating.City && p.RowKey == rating.SchoolId).FirstOrDefault();
            
            var newRating = (school.Rating * school.RatingsCount + rating.Rating)/(school.RatingsCount + 1);

            school.RatingsCount = school.RatingsCount + 1;
            school.Rating = Math.Round(newRating * 2, MidpointRounding.AwayFromZero) / 2;

            TableOperation updateOperation = TableOperation.Replace(school);

            table.Execute(updateOperation);

            return school;
        }

        public  List<School> SearchSchools(SearchDetails search)
        {
            IQueryable<School> finalQuery = table.CreateQuery<School>();

            if (search.Accreditation != null)
            {
                string accrediction = search.Accreditation.ToLower();

                finalQuery = finalQuery.Where(p => p.Accreditation == accrediction);
            }

            if (search.AdmissionsInProgress != null)
            {
                //  var innerQuery = table.CreateQuery<School>().Where(p => p.AdmissionsInProgress == search.AdmissionsInProgress);
                finalQuery = finalQuery.Where(p => p.AdmissionsInProgress == search.AdmissionsInProgress);
            }

            if (search.City != null)
            {
                search.City = search.City.ToLower();
              //  var innerQuery = table.CreateQuery<School>().Where(p => p.City == search.City);
                finalQuery = finalQuery.Where(p => p.City == search.City);
            }

            if (search.Class != null)
            {
                int searchClass = Convert.ToInt32(search.Class);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.LowerClassRange >= search.Class && p.UpperClassRange <= search.Class);
                finalQuery = finalQuery.Where(p => p.LowerClassRange <= searchClass && p.UpperClassRange >= searchClass);
            }

            if (search.DayBoarding != null)
            {
                bool dayBoarding = Convert.ToBoolean(search.DayBoarding);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.DayBoarding == search.DayBoarding);
                finalQuery = finalQuery.Where(p => p.DayBoarding == dayBoarding);
            }

            if (search.DigitalClassroom != null)
            {
                bool digitalClassroom = Convert.ToBoolean(search.DigitalClassroom);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.DigitalClassroom == search.DigitalClassroom);
                finalQuery = finalQuery.Where(p => p.DigitalClassroom == digitalClassroom);
            }

            if (search.Keyword != null)
            {
                string sample = search.Keyword.ToLower();
                //   var innerQuery = table.CreateQuery<School>().Where(p => p.SchoolName.Contains(search.Keyword) || p.Address.Contains(search.Keyword));
                finalQuery = finalQuery.Where(p => p.SchoolName.Equals(sample));
            }

            if (search.MidDayMeals != null)
            {
                bool midDayMeals = Convert.ToBoolean(search.MidDayMeals);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.MidDayMeals == search.MidDayMeals);
                finalQuery = finalQuery.Where(p => p.MidDayMeals == midDayMeals);
            }

            if (search.Playground != null)
            {
                bool playGround = Convert.ToBoolean(search.Playground);
                //  var innerQuery = table.CreateQuery<School>().Where(p => p.Playground == search.Playground);
                finalQuery = finalQuery.Where(p => p.Playground == playGround);
            }

            if (search.Rating != null && search.Rating != 0)
            {
                double rating = Convert.ToDouble(search.Rating);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.Rating == search.Rating);
                finalQuery = finalQuery.Where(p => p.Rating == rating);
            }

            //if (search.SchoolBus != null)
            //{
            //    bool schoolBus = Convert.ToBoolean(search.SchoolBus);
            //   // var innerQuery = table.CreateQuery<School>().Where(p => p.SchoolBus == search.SchoolBus);
            //    finalQuery = finalQuery.Where(p => p.SchoolBus == schoolBus);
            //}

            if (search.Transportation != null)
            {
                bool transportation = Convert.ToBoolean(search.Transportation);
                // var innerQuery = table.CreateQuery<School>().Where(p => p.Transportation == search.Transportation);
                finalQuery = finalQuery.Where(p => p.Transportation == transportation);
            }


            //List<School> schools = new List<School>();

            //var sampleSchools = finalQuery.ToList();
            //var temp = table.CreateQuery<School>();
            //var temporary = temp.ToList();
            //foreach (var school in sampleSchools)
            //{
                
            //    string searchClass = search.Class == null ? "Any Class" : search.Class == 0 ? "KG" : search.Class.ToString();
            //    var filter = temporary.Where(x => x.PartitionKey == searchClass && x.RowKey == school.SchoolId && x.MinimumBudget <= search.MinimumBudget && x.MaximumBudget >= search.MaximumBudget).First();
            //    School filteredSchool = filter;
            //    if (filteredSchool != null)
            //    {
            //        schools.Add(filteredSchool);
            //        sampleSchools.Remove(school);
            //        temporary.Remove(school);
            //    }
            //    break;
            //}

            // Pending budget Filter
            return finalQuery.ToList();
        }

        public async Task<bool> UpdateSchoolLatitudeAndLongitude()
        {
                var schoolDetails = table.CreateQuery<School>().ToList();
                TableOperation updateOperation;

                foreach (var school in schoolDetails)
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await client.GetAsync("http://maps.googleapis.com/maps/api/geocode/json?address=" + school.Address + "&sensor=false");
                        var jsonContent = await response.Content.ReadAsStringAsync();
                        //var json = (JObject)JsonConvert.DeserializeObject(jsonContent.ToString());
                        //var location = json["results"].ToString();
                        RootObjectForLocation schoolLocation = JsonConvert.DeserializeObject<RootObjectForLocation>(jsonContent.ToString());
                        if (schoolLocation.results.Count > 0)
                        {
                            school.Latitude = schoolLocation.results[0].geometry.location.lat;
                            school.Longitude = schoolLocation.results[0].geometry.location.lng;

                            updateOperation = TableOperation.Replace(school);

                            table.Execute(updateOperation);
                        }
                    }
                    catch (Exception ex)
                    {
                        string a = ex.Message;
                    }
                }
                return true;
            
        }
    }
}
