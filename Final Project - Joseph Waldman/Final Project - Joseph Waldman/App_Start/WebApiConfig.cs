using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Final_Project___Joseph_Waldman
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(); // לאפשר לווב איי פי איי לעבוד עם אנגולר ללא שגיאות
            // Web API routes
            config.MapHttpAttributeRoutes();

            // ('דף ראוטינג עבור אורח,משתמש רגיל, עובד ומנהל. הכתובות מתחילות בשם הבקשה, ורק לאחר מכן סוג המשתמש (עובד, מנהל וכו

            // Guest Routing //
            #region 
            //http://localhost:61955/Get/Guest/AvailableCarFields 
            config.Routes.MapHttpRoute(
            name: "Guest-AvailableCarFields",
            routeTemplate: "Get/{controller}/{action}",
            defaults:
                new { controller = "Guest", action = "AvailableCarFields" }
            );

            //http://localhost:61955/Get/Guest/AvailableCarTypes
            config.Routes.MapHttpRoute(
            name: "Guest-AvailableCarTypes",
            routeTemplate: "Get/{controller}/{action}",
            defaults:
                new { controller = "Guest", action = "AvailableCarTypes" }
            );

            //http://localhost:61955/Get/Guest/UniqueUsername?Username=123
            config.Routes.MapHttpRoute(
            name: "Guest-UniqueUsername",
            routeTemplate: "Get/{controller}/{action}",
            defaults:
                new { controller = "Guest", action = "UniqueUsername" }
            );

            //http://localhost:61955/Get/Guest/Login?UserDetails=Admin:1234
            config.Routes.MapHttpRoute(
            name: "Guest-Login",
            routeTemplate: "Get/{controller}/{action}",
            defaults:
                new { controller = "Guest", action = "Login" }
            );

            //http://localhost:61955/Post/Guest/SignUp/
            config.Routes.MapHttpRoute(
            name: "Guest-SignUp",
            routeTemplate: "Post/{controller}/{action}/{NewUser}",
            defaults:
                new { controller = "Guest", action = "SignUp" }
            );

            #endregion

            // User Routing //
            #region
            //http://localhost:61955/Post/User/NewCarRental/
            config.Routes.MapHttpRoute(
            name: "User-NewCarRental",
            routeTemplate: "Post/{controller}/{action}/",
            defaults:
                new { controller = "User", action = "NewCarRental" }
            );

            //http://localhost:61955/Get/User/UserRentalsHistory
            config.Routes.MapHttpRoute(
            name: "User-UserRentalsHistory",
            routeTemplate: "Get/{controller}/{action}/{id}",
            defaults:
                new { controller = "User", action = "UserRentalsHistory" }
            );
            #endregion

            // Employee Routing //
            #region
            //http://localhost:61955/Get/Employee/ReturnCarCosts?LicensePlateNumber=1155554
            config.Routes.MapHttpRoute(
            name: "Employee-ReturnCarCosts",
            routeTemplate: "Get/{controller}/{action}/",
            defaults:
                new { controller = "Employee", action = "ReturnCarCosts" }
            );

            //http://localhost:61955/Get/Employee/CarRentalDates/1177711/2020-05-01
            config.Routes.MapHttpRoute(
            name: "Employee-CarRentalDates",
            routeTemplate: "Get/{controller}/{action}/{LicensePlateNumber}/{date}/",
            defaults:
                new { controller = "Employee", action = "CarRentalDates" }
            );

            //http://localhost:61955/Get/Employee/GetCarRentalFields/
            config.Routes.MapHttpRoute(
            name: "Employee-GetCarRentalFields",
                routeTemplate: "Get/{controller}/{action}/",
                defaults:
                    new { controller = "Employee", action = "GetCarRentalFields" }
                );

            //http://localhost:61955/Put/Employee/CarReturn/?LicensePlateNumber=4981889&Date=09-05-2020
            config.Routes.MapHttpRoute(
            name: "Employee-CarReturn",
            routeTemplate: "Put/{controller}/{action}/{LicensePlateNumber}/{date}/",
            defaults:
                new { controller = "Employee", action = "CarReturn" }
            );
            #endregion

            //Admin Routing //
            #region
            //Admin Get Methods
            #region
            //http://localhost:61955/Get/Admin/GetCarRentalFields/
            config.Routes.MapHttpRoute(
            name: "Admin-GetCarRentalFields",
            routeTemplate: "Get/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "GetCarRentalFields" }
            );

            //http://localhost:61955/Get/Admin/GetCarFields/
            config.Routes.MapHttpRoute(
            name: "Admin-GetCarFields",
            routeTemplate: "Get/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "GetCarFields" }
            );

            //http://localhost:61955/Get/Admin/GetCarTypes/
            config.Routes.MapHttpRoute(
            name: "Admin-GetCarTypes",
            routeTemplate: "Get/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "GetCarTypes" }
            );

            //http://localhost:61955/Get/Admin/GetUserFields/
            config.Routes.MapHttpRoute(
            name: "Admin-GetUserFields",
            routeTemplate: "Get/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "GetUserFields" }
            );
            #endregion

            //Admin Post Methods
            #region
            //http://localhost:61955/Post/Admin/PostCarRentalFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PostCarRentalFields",
            routeTemplate: "Post/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PostCarRentalFields" }
            );

            //http://localhost:61955/Post/Admin/PostCarFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PostCarFields",
            routeTemplate: "Post/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PostCarFields" }
            );

            //http://localhost:61955/Post/Admin/PostCarTypes/
            config.Routes.MapHttpRoute(
            name: "Admin-PostCarTypes",
            routeTemplate: "Post/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PostCarTypes" }
            );

            //http://localhost:61955/Post/Admin/PostUserFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PostUserFields",
            routeTemplate: "Post/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PostUserFields" }
            );
            #endregion

            //Admin Put Methods
            #region
            //http://localhost:61955/Put/Admin/PutCarRentalFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PutCarRentalFields",
            routeTemplate: "Put/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PutCarRentalFields" }
            );

            //http://localhost:61955/Put/Admin/PutCarFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PutCarFields",
            routeTemplate: "Put/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PutCarFields" }
            );

            //http://localhost:61955/Put/Admin/PutCarTypes/
            config.Routes.MapHttpRoute(
            name: "Admin-PutCarTypes",
            routeTemplate: "Put/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PutCarTypes" }
            );

            //http://localhost:61955/Put/Admin/PutUserFields/
            config.Routes.MapHttpRoute(
            name: "Admin-PutUserFields",
            routeTemplate: "Put/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "PutUserFields" }
            );
            #endregion

            //Admin Delete Methods
            #region
            //http://localhost:61955/Delete/Admin/DeleteCarRentalFields/
            config.Routes.MapHttpRoute(
            name: "Admin-DeleteCarRentalFields",
            routeTemplate: "Delete/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "DeleteCarRentalFields" }
            );

            //http://localhost:61955/Delete/Admin/DeleteCarFields/
            config.Routes.MapHttpRoute(
            name: "Admin-DeleteCarFields",
            routeTemplate: "Delete/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "DeleteCarFields" }
            );

            //http://localhost:61955/Delete/Admin/DeleteCarTypes/
            config.Routes.MapHttpRoute(
            name: "Admin-DeleteCarTypes",
            routeTemplate: "Delete/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "DeleteCarTypes" }
            );

            //http://localhost:61955/Delete/Admin/DeleteUserFields/
            config.Routes.MapHttpRoute(
            name: "Admin-DeleteUserFields",
            routeTemplate: "Delete/{controller}/{action}/",
            defaults:
                new { controller = "Admin", action = "DeleteUserFields" }
            );
            #endregion

            #endregion
        }
    }
}
