using System.Web;
using System.Web.Mvc;

namespace Barangay_Health_Record
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
