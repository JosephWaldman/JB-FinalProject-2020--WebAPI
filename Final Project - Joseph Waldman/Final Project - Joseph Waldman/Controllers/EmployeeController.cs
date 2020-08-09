using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using Authorization = Final_Project___Joseph_Waldman.Filters.Authorization;

namespace Final_Project___Joseph_Waldman.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]  // אטריביוט כדי שמת'ודס יהיו מוכרים באנגולר פריימוורק
    [Authorization] // תגית המייצגת הרשאת גישה לעובד חברה
    public class EmployeeController : ApiController
    {
        // GET: api/Employee
        dbEntities db = new dbEntities();

        [HttpGet] // אטריביוט עבור ראוטינג
        public CarType ReturnCarCosts(int LicensePlateNumber) // גט מת'וד שמקבלת מזהה ייחודי, הגט מחזיר אובייקט סוג רכב, מטרתה להחזיר פרטי עלויות עבור הרכב הספציפי
        {
            CarField CarField = db.CarFields.FirstOrDefault( //  בודק אם הרכב קיים במאגר לפי מזהה ייחודי
                c => c.LicensePlateNumber == LicensePlateNumber);

            CarType CarType = db.CarTypes.FirstOrDefault( // בודק את סוג הרכב
                c => c.CarTypeID == CarField.CarTypeID);

            return CarType; // החזרה של רשומה של סוג רכב, שם כתובות העלויות
        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public CarRentalField CarRentalDates(int LicensePlateNumber, string date) // גט מת'וד שמקבלת מזהה ייחודי ותאריך של היום,
                                                                //הגט מחזיר אובייקט השכרת רכב, מטרתה היא להחזיר את תאריכי ההשכרה של רכב ספציפי

        {
            Thread.Sleep(500); // עוד שיטה לקבלת אובסרוובל, ולמעשה את התוצאה באתר, תוך כדי שהפונקציה באנגולר מתקדמת
            var CarRentalDates = from i in db.CarRentalFields // לינק קוורי, משווה את הרשומות מטבלת השכרות
                                 where i.LicensePlateNumber == LicensePlateNumber // התנית וור, מזהה ייחודי
                                 && i.ReturnCarDateApproval == date // התניית וור לתאריך החזרה בפועל והתאריך של היום
                                 select i; // יחזיר אובייקט

            return CarRentalDates.FirstOrDefault(); // החזרת רשומה של השכרת רכב
        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarRentalField> GetCarRentalFields()// גט שמחזיר רשימת עם כל השכרות החברה
        {
            using (dbEntities db = new dbEntities())
            {
                return db.CarRentalFields.ToArray();
            }
        }

        // PUT: 
        [HttpPut] // אטריביוט עבור ראוטינג
        public HttpResponseMessage CarReturn(int LicensePlateNumber, string Date) // פוט מת'וד עבור החזרת רכב חברה, מקבלת פרמטר ראשון -מס לוחית רישוי, פרמטר שני התאריך של ההחזרה, שהוא היום
        {
            CarRentalField MakeCarDateApproval = db.CarRentalFields.FirstOrDefault( // בדיקה אם נמצא שההשכרה קיימת במאגר
            c => c.LicensePlateNumber == LicensePlateNumber && c.ReturnCarDateApproval == null);

            CarField MakeCarAvailable = db.CarFields.FirstOrDefault( // בדיקה שניה עבור החזרת רכב, ואם נמצא שהרכב קיים במאגר
                   c => c.LicensePlateNumber == LicensePlateNumber);

            if (MakeCarDateApproval != null && MakeCarAvailable != null) // היכנס להתניה, כל עוד לא קיבלנו נאל- לא קיבלנו תוצאה בבדיקות
            {
                MakeCarDateApproval.ReturnCarDateApproval = Date; // הכנס את התאריך של היום לערך תאריך סיום השכרה בפועל באובייקט מהבדיקה הראשונה
                MakeCarAvailable.IsAvailable = true; // ומהבדיקה השניה, הפוך את הרכב לזמין באתר
                db.SaveChanges(); // שמירת הנתונים במאגר מידע
                return Request.CreateResponse<CarField>(HttpStatusCode.OK, MakeCarAvailable);
            }
            else
                return Request.CreateResponse<CarField>(HttpStatusCode.BadRequest, null);
        }
    }
}
