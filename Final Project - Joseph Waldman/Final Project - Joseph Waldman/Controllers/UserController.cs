using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Final_Project___Joseph_Waldman.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]  // אטריביוט כדי שמת'ודס יהיו מוכרים באנגולר פריימוורק
    public class UserController : ApiController
    {
        dbEntities db = new dbEntities(); // אתחול מאגר המידע

        // GET: 

        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarRentalField> UserRentalsHistory(int id) // גט מת'וד המחזירה את כל הסטורית ההשכרות למשתמש שנכנס לאתר. מקבלת מספר מזהה של משתמש רשום באתר
        {                                                // שאילתא עם לינק
            var UserRentalHistory = from i in db.CarRentalFields // שירוץ על טבלת השכרת רכבים
                                    where i.UserID == id // וכל עוד שלרשומה יש מזהה ייחודי זהה למזהה של המשתמש
                                    select i; // הוסף את אותו אובייקט לרשימה
            return UserRentalHistory; // בסיום החזר את כל הרשימות שנמצאו
        }

        // POST: 
        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage NewCarRental([FromBody]CarRentalField NewCarRentalObject) // פונקצית פוסט מת'וד, מקבלת אובייקט מסוג השכרת רכב
        {
                                                        // שאילתא, לינק
            var CarRentals = from i in db.CarRentalFields // שירוץ על כל הרשומות מטבלת השכרת רכב
                                                          // בבדיקה, אם נמצא שלוחית הרישוי מטבלת רכבים במאגר הנתונים זהה למספר לוחית רישוי של המכונית אותה המשתמש בחר להזמין באתר
                                                          // ואם בתאריך החזרה בפועל אין ערך במכונית מהמאגר נתונים
                             where i.LicensePlateNumber == NewCarRentalObject.LicensePlateNumber 
                             select i; // אז הכנס את הרשומה לרשימת אובייקטים

            if (CarRentals == null) // במידה ובבדיקה לא נמצא הרכב, החזר שגיאה.
            {
                return Request.CreateResponse<CarRentalField>(HttpStatusCode.BadRequest, null);
            }
            else // אחרת, המשך לפעולות הבאות
            {
                CarField Car = db.CarFields.FirstOrDefault( // בדוק אם קיימת המכונית בטבלת רכבים במאגר לפי מס' לוחית רישוי
                c => c.LicensePlateNumber == NewCarRentalObject.LicensePlateNumber);

                Car.IsAvailable = false; // הוצא את המכונית מזמינות באתר

                db.CarRentalFields.Add(NewCarRentalObject); // הוסף השכרה חדשה למאגר

                db.SaveChanges(); // שמור את השינויים במאגר נתונים.

                return Request.CreateResponse<CarRentalField>(HttpStatusCode.OK, NewCarRentalObject);
            }
        }
    }
}