using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Final_Project___Joseph_Waldman.Filters
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {                                            // בדיקת ולידציה 
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //כשמתבצעות פניות מלקוחות, לסנן אותן לפי דרישות מסויימות לפי התניית אינווליד, 

            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                   //נחזיר ריספונט של בד ריקווסט וגם את הנתונים של השגיאה
                   System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}