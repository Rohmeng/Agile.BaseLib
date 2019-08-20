using Microsoft.AspNetCore.Mvc.Filters;
using Agile.BaseLib.Helpers;

namespace Agile.BaseLib.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //var type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Logs.WriteError(filterContext.Exception);

            filterContext.ExceptionHandled = true;
        }
    }
}
