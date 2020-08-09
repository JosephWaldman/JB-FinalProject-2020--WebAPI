using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Authorization = Final_Project___Joseph_Waldman.Filters.Authorization;

namespace Final_Project___Joseph_Waldman.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]  // אטריביוט כדי שמת'ודס יהיו מוכרים באנגולר פריימוורק
    public class AdminController : ApiController
    {
        dbEntities db = new dbEntities(); // אתחול מאגר המידע
        // GET: 
        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarRentalField> GetCarRentalFields() // גט שמחזיר את כל ההשכרות של החברה
        {
            using (dbEntities db = new dbEntities())
            {
                return db.CarRentalFields.ToArray();
            }
        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarField> GetCarFields() // גט שמחזיר רשימה של כל המכוניות
        {
            using (dbEntities db = new dbEntities()) 
            {
                return db.CarFields.ToArray();
            }
        }
        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<CarType> GetCarTypes() // גט שמחזיר רשימת עם סוגי הרכבים
        {
            using (dbEntities db = new dbEntities())
            {
                return db.CarTypes.ToArray();
            }
        }

        [HttpGet] // אטריביוט עבור ראוטינג
        public IEnumerable<UserField> GetUserFields() // גט שמחזיר רשימת משתמשים
        {
            using (dbEntities db = new dbEntities())
            {
                return db.UserFields.ToArray();
            }
        }
        // POST: 

        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PostCarRentalFields([FromBody]CarRentalField NewCarRental) // פוסט מת'וד, מקבלת מגוף הבקשה אובייקט השכרה חדשה ומוסיפה לרשימת ההשכרות
        {
            using (dbEntities db = new dbEntities())
            {
                db.CarRentalFields.Add(NewCarRental); // הוספת האובייקט לרשומה חדשה
                db.SaveChanges(); // שמירת הנתונים במאגר
                return Request.CreateResponse<CarRentalField>(HttpStatusCode.OK, NewCarRental);
            }
        }

        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PostCarFields([FromBody]CarField NewCar) // פוסט מת'וד, מקבלת מגוף הבקשה אובייקט מכונית חדשה ומוסיפה לרשימת המכוניות
        {
            using (dbEntities db = new dbEntities())
            {
                db.CarFields.Add(NewCar); // הוספת האובייקט לרשומה חדשה
                db.SaveChanges(); // שמירת הנתונים במאגר
                return Request.CreateResponse<CarField>(HttpStatusCode.OK, NewCar);
            }
        }

        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PostCarTypes([FromBody]CarType NewCar) // פוסט מת'וד, מקבלת מגוף הבקשה אובייקט סוג מכונית חדש ומוסיפה לרשימת סוגי המכוניות
        {
            using (dbEntities db = new dbEntities())
            {
                db.CarTypes.Add(NewCar); // הוספת האובייקט לרשומה חדשה
                db.SaveChanges(); // שמירת הנתונים במאגר
                return Request.CreateResponse<CarType>(HttpStatusCode.OK, NewCar);
            }
        }

        [HttpPost] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PostUserFields([FromBody]UserField NewUser) // פוסט מת'וד, מקבלת מגוף הבקשה אובייקט משתמש חדש ומוסיפה לרשימת המשתמשים באתר
        {
            using (dbEntities db = new dbEntities())
            {
                db.UserFields.Add(NewUser); // הוספת האובייקט לרשומה חדשה
                db.SaveChanges(); // שמירת הנתונים במאגר
                return Request.CreateResponse<UserField>(HttpStatusCode.OK, NewUser);
            }
        }

        [HttpPut] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PutCarRentalFields([FromBody]CarRentalField UpdateCarRental) // פוט מת'וד, מקבלת מגוף הבקשה אובייקט השכרת רכב 
        {
            using (dbEntities db = new dbEntities())
            {
                CarRentalField CarToUpdate = db.CarRentalFields.FirstOrDefault(c => c.ID == UpdateCarRental.ID); // בבדיקה, אם נמצא שההשכרה קיימת במאגר לפי מספר זיהוי איי די

                if (CarToUpdate != null) // שייכנס להתניה ויעדכן את המאגר עם פרטי הרשומה שמצא
                {
                    CarToUpdate.UserID = UpdateCarRental.UserID;
                    CarToUpdate.LicensePlateNumber = UpdateCarRental.LicensePlateNumber;
                    CarToUpdate.RentalStartDate = UpdateCarRental.RentalStartDate;
                    CarToUpdate.ReturnCarDate = UpdateCarRental.ReturnCarDate;
                    CarToUpdate.ReturnCarDateApproval = UpdateCarRental.ReturnCarDateApproval;

                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<CarRentalField>(HttpStatusCode.OK, UpdateCarRental);
                }
                else
                    return Request.CreateResponse<CarRentalField>(HttpStatusCode.BadRequest, UpdateCarRental);
            }
        }

        [HttpPut] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PutCarFields([FromBody]CarField UpdateCarField) // פוט מת'וד, מקבלת מגוף הבקשה אובייקט רכב חדש
        {
            using (dbEntities db = new dbEntities())
            {
                CarField CarFieldToUpdate = db.CarFields.FirstOrDefault(c => c.LicensePlateNumber == UpdateCarField.LicensePlateNumber); // בבדיקה, אם נמצא שהרכב קיים במאגר לפי מספר לוחית רישוי

                if (CarFieldToUpdate != null) // שייכנס להתניה ויעדכן את המאגר עם פרטי הרשומה שמצא
                {
                    CarFieldToUpdate.CarTypeID = UpdateCarField.CarTypeID;
                    CarFieldToUpdate.CurrentKilometrage = UpdateCarField.CurrentKilometrage;
                    CarFieldToUpdate.Picture = UpdateCarField.Picture;
                    CarFieldToUpdate.IsRepaired = UpdateCarField.IsRepaired;
                    CarFieldToUpdate.IsAvailable = UpdateCarField.IsAvailable;
                    CarFieldToUpdate.Branch = UpdateCarField.Branch;

                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<CarField>(HttpStatusCode.OK, UpdateCarField);
                }
                else
                    return Request.CreateResponse<CarField>(HttpStatusCode.BadRequest, UpdateCarField);
            }
        }

        [HttpPut] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PutCarTypes([FromBody]CarType UpdateCarType) // פוט מת'וד, מקבלת מגוף הבקשה אובייקט סוג רכב חדש
        {
            using (dbEntities db = new dbEntities())
            {
                CarType CarTypeToUpdate = db.CarTypes.FirstOrDefault(c => c.CarTypeID == UpdateCarType.CarTypeID); // בבדיקה, אם נמצא שסוג הרכב קיים במאגר לפי מספר מזהה ייחודי

                if (CarTypeToUpdate != null) // שייכנס להתניה ויעדכן את המאגר עם פרטי הרשומה שמצא
                {
                    CarTypeToUpdate.ManufacturerName = UpdateCarType.ManufacturerName;
                    CarTypeToUpdate.Model = UpdateCarType.Model;
                    CarTypeToUpdate.DailyCost = UpdateCarType.DailyCost;
                    CarTypeToUpdate.DailyMonetaryFine = UpdateCarType.DailyMonetaryFine;
                    CarTypeToUpdate.ManufactureYear = UpdateCarType.ManufactureYear;
                    CarTypeToUpdate.GearBox = UpdateCarType.GearBox;

                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<CarType>(HttpStatusCode.OK, UpdateCarType);
                }
                else
                    return Request.CreateResponse<CarType>(HttpStatusCode.BadRequest, UpdateCarType);
            }
        }

        [HttpPut] // אטריביוט עבור ראוטינג
        public HttpResponseMessage PutUserFields([FromBody]UserField UpdateUserField) // פוט מת'וד, מקבלת מגוף הבקשה אובייקט עם משתמש חדש
        {
            using (dbEntities db = new dbEntities())
            {
                UserField UserFieldToUpdate = db.UserFields.FirstOrDefault(c => c.ID == UpdateUserField.ID); // בבדיקה, אם נמצא שפרטי המשתמש קיימים במאגר לפי מספר מזהה ייחודי

                if (UserFieldToUpdate != null) // שייכנס להתניה ויעדכן את המאגר עם פרטי הרשומה שמצא
                {
                    UserFieldToUpdate.DriverLicense = UpdateUserField.DriverLicense;
                    UserFieldToUpdate.Username = UpdateUserField.Username;
                    UserFieldToUpdate.Password = UpdateUserField.Password;
                    UserFieldToUpdate.FullName = UpdateUserField.FullName;
                    UserFieldToUpdate.Birthdate = UpdateUserField.Birthdate;
                    UserFieldToUpdate.Gender = UpdateUserField.Gender;
                    UserFieldToUpdate.Email = UpdateUserField.Email;
                    UserFieldToUpdate.Thumbnail = UpdateUserField.Thumbnail;
                    UserFieldToUpdate.Rank = UpdateUserField.Rank;

                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<UserField>(HttpStatusCode.OK, UserFieldToUpdate);
                }
                else
                    return Request.CreateResponse<UserField>(HttpStatusCode.BadRequest, null);
            }
        }
        [HttpDelete] // אטריביוט עבור ראוטינג
        public HttpResponseMessage DeleteCarRentalFields(int CarRentalID) // דליט מת'וד עבור ההשכרה, מקבלת מזהה ייחודי אינט
        {
            using (dbEntities db = new dbEntities())
            {
                CarRentalField Car = db.CarRentalFields.FirstOrDefault(c => c.ID  == CarRentalID); // בבדיקה אם נמצא מזהה ייחודי
                if (Car != null) // שייכנס להתנייה 
                {
                    db.CarRentalFields.Remove(Car); // וימחק האובייקט מהמאגר
                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<int>(HttpStatusCode.OK, CarRentalID);
                }
                else
                return Request.CreateResponse<int>(HttpStatusCode.BadRequest, CarRentalID);
            }
        }

        [HttpDelete] // אטריביוט עבור ראוטינג
        public HttpResponseMessage DeleteCarFields(int CarID)// דליט מת'וד עבור רכב, מקבלת מזהה ייחודי אינט
        {
            using (dbEntities db = new dbEntities())
            {
                CarField Car = db.CarFields.FirstOrDefault(c => c.LicensePlateNumber == CarID);// בבדיקה אם נמצא מזהה ייחודי
                if (Car != null) // שייכנס להתנייה 
                {
                    db.CarFields.Remove(Car); // וימחק האובייקט מהמאגר
                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<int>(HttpStatusCode.OK, CarID);
                }
                else
                    return Request.CreateResponse<int>(HttpStatusCode.BadRequest, CarID);
            }
        }

        [HttpDelete] // אטריביוט עבור ראוטינג
        public HttpResponseMessage DeleteCarTypes(int CarTypeID)// דליט מת'וד עבור סוג רכב, מקבלת מזהה ייחודי אינט
        {
            using (dbEntities db = new dbEntities())
            {
                CarType Car = db.CarTypes.FirstOrDefault(c => c.CarTypeID == CarTypeID);// בבדיקה אם נמצא מזהה ייחודי
                if (Car != null) // שייכנס להתנייה 
                {
                    db.CarTypes.Remove(Car); // וימחק האובייקט מהמאגר
                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<int>(HttpStatusCode.OK, CarTypeID);
                }
                else
                    return Request.CreateResponse<int>(HttpStatusCode.BadRequest, CarTypeID);
            }
        }

        [HttpDelete] // אטריביוט עבור ראוטינג
        public HttpResponseMessage DeleteUserFields(int UserID)// דליט מת'וד עבור משתמש, מקבלת מזהה ייחודי אינט
        {
            using (dbEntities db = new dbEntities())
            {
                UserField Car = db.UserFields.FirstOrDefault(c => c.ID == UserID);// בבדיקה אם נמצא מזהה ייחודי
                if (Car != null) // שייכנס להתנייה 
                {
                    db.UserFields.Remove(Car); // וימחק האובייקט מהמאגר
                    db.SaveChanges(); // שמירת הנתונים במאגר
                    return Request.CreateResponse<int>(HttpStatusCode.OK, UserID);
                }
                else
                    return Request.CreateResponse<int>(HttpStatusCode.BadRequest, UserID);
            }
        }
    }
}
