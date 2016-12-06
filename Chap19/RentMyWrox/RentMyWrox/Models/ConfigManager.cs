using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Configuration;

namespace RentMyWrox.Models
{
    public class ConfigManager
    {
        public static int AdminItemListPageSize
        {
            get
            {
                int answer = 5;
                string results = WebConfigurationManager.AppSettings.Get("AdminItemListPageSize");
                if (!string.IsNullOrWhiteSpace(results))
                {
                    int.TryParse(results, out answer);
                }
                return answer;
            }
        }

        public static int StoreOpenTime
        {
            get
            {
                int answer = 9;
                string results = WebConfigurationManager.AppSettings.Get("StoreOpenTime");
                if (!string.IsNullOrWhiteSpace(results))
                {
                    int.TryParse(results, out answer);
                }
                return answer;
            }
        }

        public static int StoreCloseTime
        {
            get
            {
                int answer = 18;
                string results = WebConfigurationManager.AppSettings.Get("StoreCloseTime");
                if (!string.IsNullOrWhiteSpace(results))
                {
                    int.TryParse(results, out answer);
                }
                return answer;
            }
        }

        public static string StoreOpenStringValue
        {
            get
            {
                string results = WebConfigurationManager.AppSettings.Get("StoreOpenStringValue");
                if (string.IsNullOrWhiteSpace(results))
                {
                    results = "9:00 am";
                }
                return results;
            }
        }

        public static bool ViewNotifications
        {
            get
            {
                bool answer = false;
                string results = WebConfigurationManager.AppSettings.Get("ViewNotifications");
                if (!string.IsNullOrWhiteSpace(results))
                {
                    bool.TryParse(results, out answer);
                }
                return answer;
            }
        }
    }
}