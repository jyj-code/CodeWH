using Log4net;
using System.Web;
using System.Web.Mvc;

namespace ILove
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new StatisticsTrackerAttribute());
        }
    }
}
