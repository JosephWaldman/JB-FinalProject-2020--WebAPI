using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Final_Project___Joseph_Waldman.Filters
{
    public class Authorization : AuthorizationFilterAttribute // הרשאת גישה עבור עובד חברה
    {
        public override void OnAuthorization(HttpActionContext actionContext) 
        {
            if (actionContext.Request.Headers.Authorization == null) // במידה ואין מידע בהדרס יחזיר ריספונס שאין הרשאת גישה למת'ודס של העובד
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                System.Net.HttpStatusCode.Unauthorized);
            }

            else // במידה ויש מידע בהדרס, המשך לפעולות הבאות
            {
                string UserAuthorization = actionContext.Request.Headers.Authorization.Parameter; // השמה לסטרינג, מה ששלחנו בתור אמצעי זיהוי
                string[] UsernameAndPassword = UserAuthorization.Split(':'); // נבצע ספליט לסטרינג בסימן סוגריים, כך נקבל שם משתמש וסיסמא בתאים נפרדים במערך
                string Username = UsernameAndPassword[0]; // (השמת תא במערך למשתנה (שם משתמש
                string Password = UsernameAndPassword[1]; // (השמת תא במערך למשתנה (סיסמא

                using (dbEntities db = new dbEntities())
                {
                    UserField CheckList = db.UserFields.FirstOrDefault(p => p.Username == Username && p.Password == Password); // בודק אם נמצאים פרטי הגולש במאגר נתונים

                    if (CheckList == null) 
                    {
                        actionContext.Response = actionContext.Request.CreateResponse( // צור הודעת אישור
                        System.Net.HttpStatusCode.Accepted);
                    }
                }
            }
        }
    }
}