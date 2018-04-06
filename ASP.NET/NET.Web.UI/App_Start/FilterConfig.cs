using Log4net;
using NET.Web.UI.App_Start;
using System.Web;
using System.Web.Mvc;

namespace NET.Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CompressAttribute());
            filters.Add(new StatisticsTrackerAttribute());
        }
    }
}
