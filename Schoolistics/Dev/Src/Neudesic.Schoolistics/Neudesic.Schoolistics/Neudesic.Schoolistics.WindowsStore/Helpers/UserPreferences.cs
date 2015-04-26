using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Neudesic.Schoolistics.WindowsStore.Helpers
{
    public class UserPreferences
    {
        
        public static string Username
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("Username", out retValue))
                {
                    return retValue.ToString();
                }
                return null;
            }
            set { ApplicationData.Current.LocalSettings.Values["Username"] = value; }
        }

        public static string RememberUsername
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("RememberUsername", out retValue))
                {
                    return retValue.ToString();
                }
                return null;
            }
            set { ApplicationData.Current.LocalSettings.Values["RememberUsername"] = value; }
        }

        public static string RememberPassword
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("RememberPassword", out retValue))
                {
                    return retValue.ToString();
                }
                return null;
            }
            set { ApplicationData.Current.LocalSettings.Values["RememberPassword"] = value; }
        }

        public static string AuthToken
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("authtoken", out retValue))
                {
                    return retValue.ToString();
                }
                return null;
            }
            set { ApplicationData.Current.LocalSettings.Values["authtoken"] = value; }
        }

        public static double Latitude
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("Latitude", out retValue))
                {
                    return Convert.ToDouble(retValue);
                }
                return 0;
            }
            set { ApplicationData.Current.LocalSettings.Values["Latitude"] = value; }
        }

        public static double Longitude
        {
            get
            {
                object retValue;
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("Longitude", out retValue))
                {
                    return Convert.ToDouble(retValue);
                }
                return 0;
            }
            set { ApplicationData.Current.LocalSettings.Values["Longitude"] = value; }
        }
    }
}
