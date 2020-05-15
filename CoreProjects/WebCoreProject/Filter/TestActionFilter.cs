using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Filter
{
    public class TestActionFilter : ActionFilterAttribute
    {
        private Logger logger = LogManager.GetLogger("Filter");
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            logger.Info("执行方法后");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logger.Info("执行方法前");
        }
    }
}
