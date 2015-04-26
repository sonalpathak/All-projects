using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Utils
{
    public class URLModifier
    {
        public static string VoteOnSurvey(string surveyId, string optionId)
        {
            var url = Constants.VoteforSurveys.Replace("##SURVEYID##", surveyId);

            url = url.Replace("##OPTIONID##", optionId);

            return url;
        }
        public static string CheckUserVotes(string userName)
        {
            var url=Constants.CheckUserVotes.Replace("##USERNAME##", userName);
            return url;
        }
        public static string GetAllSchoolComments(string schoolId)
        {
            var url = Constants.GetAllSchoolComments.Replace("##SCHOOLID##", schoolId);
            return url;
        }
    }
}
