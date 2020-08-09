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
    public class GuestController : ApiController
    {
        dbEntities db = new dbEntities(); // אתחול מאגר המידע

        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarField> Ava2ilableCarFields()
        {                                    // שאילתא עם לינק
            var Cars = from i in db.CarFields // והשמתה למשתנה קארס. שירוץ על טבלת הרכבים
                       where i.IsAvailable == true // אם ערכו של מאפיין 'האם זמין' מוגדר לאמת
                       select i; // תוסיף את אותו אובייקט אליו שייך המאפיין למשתנה קארס
            return Cars; // החזר את כל הרשומות שנמצאו
        }
        [HttpGet] // אטריביוט עבור ראוטינג

        public IEnumerable<object> AvailableCars()  // גט שמחזיר את כל הרכבים הפנויים , מוצג בחלון החיפוש באתר
        {
            // שאילתא עם לינק
            var Cars = (from Car in db.CarFields // שירוץ על טבלת רכבים
                        join CarType in db.CarTypes on Car.CarTypeID equals CarType.CarTypeID // וכמו כן על סוגי רכבים שהם בעלי מזהה משותף
                        where Car.IsAvailable == true // וכל עוד הרכב הינו פנוי
                        orderby CarType.CarTypeID // תחזיר ליסט של אובייקטים בסדר עולה לפי מזהה ייחודי
                        select new // איחדתי בין שתי הטבלאות עי יצירת עמודות חדשות
                        {
                            CarType.ManufacturerName,
                            CarType.ManufactureYear,
                            CarType.Model,
                            Car.CurrentKilometrage,
                            CarType.GearBox,
                            Car.IsRepaired,
                            CarType.DailyCost,
                            CarType.DailyMonetaryFine,
                            Car.IsAvailable,
                            Car.LicensePlateNumber,
                            Car.Branch,
                            Car.CarTypeID,
                            Car.Picture
                        }).ToList();

            return Cars; // החזרת הליסט
        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public string UniqueUsername(string Username) // גט מת'וד שמחזיר אם שם המשתמש ייחודי או לא, מקבל שם משתמש כסטרינג. הקריאה לפונקציה בעת ההרשמה לאתר
        {
            UserField UserFieldCheckUp = db.UserFields.FirstOrDefault( // בדיקה אם יש נמצא כבר שם משתמש זהה במאגר הנתונים
                    c => c.Username == Username);

            if (UserFieldCheckUp == null) // במידה ולא נמצא, בערך החוזר שהוא נאל
                return "Accepted"; // החזר הודעת מאושר
            return "false"; // אחרת החזר הודעה אחרת

        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public UserField Login(string UserDetails) // גט מת'וד עבור כניסת משתמשים לאתר, מקבלת את פרטי המשתמש בסטרינג אחד
        {
            string[] UsernameAndPassword = UserDetails.Split(':'); // פיצול הסטרינג למערך היכן שיש נקודותיים
            string Username = UsernameAndPassword[0]; // השמת ערכי האינדקסים מהמערך ל2 סטרינגים
            string Password = UsernameAndPassword[1];

            UserField UserFieldCheckUp = db.UserFields.FirstOrDefault( // בדיקה אם נמצא ששם המשתמש והסיסמא קיימים ברשומות במאגר הנתונים
                c => c.Username == Username && c.Password == Password);

            if (UserFieldCheckUp != null) // היכנס אם בבדיקה נמצאה רשומה זהה לנתוני המשתמש
                return UserFieldCheckUp; // והחזר את אותו אובייקט
            return null; // אחרת, החזר נאל

        }

        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage SignUp([FromBody]UserField NewUser) // פונקצית פוסט מת'וד שמקבלת מגוף הבקשה את ערכי טופס (ריאקטיב פורמס)
        {
            if (NewUser != null) // במידה הפונקציה מקבלת ערך ששונה מנאל 
            {
                db.UserFields.Add(NewUser); // הוסף את האובייקט לטבלת משתמשים במאגר הנתונים
                db.SaveChanges(); // ושמור שינויים באנטיטי פריים וורק
                return Request.CreateResponse<UserField>(HttpStatusCode.OK, NewUser);
            }
            else
            {
                return Request.CreateResponse<UserField>(HttpStatusCode.BadRequest, null);
            }
        }
    }
}