using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Project___Joseph_Waldman.Models
{
    [MetadataType(typeof(dbEntities))]
    public partial class UserFields // קלאס שממשיך את הקלאס באנטיטי פריימוורק, זאת על מנת שלא נאבד מידע בשמירה
    {
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int UserID { get; set; }
        [Required, Range(9,9, ErrorMessage = "Driver Liecense is out of range")] // ולידציה - טווח מספרים
        public int DriverLicense { get; set; }
        [Required,StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Username length is out of range")] // ולידציה - טווח מספרים
        public string Username { get; set; }
        [Required, StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Password length is out of range")] // ולידציה - טווח מספרים
        public string Password { get; set; }
        [Required, StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "FullName length is out of range")] // ולידציה - טווח מספרים
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string Gender { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string Email { get; set; }
        public string Thumbnail { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string Rank { get; set; }
    }

    [MetadataType(typeof(dbEntities))]
    public partial class CarRentalFields
    {
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int UserID { get; set; }
        [Required, Range(6, 8, ErrorMessage = "Driver Liecense is out of range")] // ולידציה - טווח מספרים
        public int LicensePlateNumber { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string RentalStartDate { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string ReturnCarDate { get; set; }
        public string ReturnCarDateApproval { get; set; }
    }

    [MetadataType(typeof(dbEntities))]
    public partial class CarTypes
    {
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int CarTypeID { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string ManufacturerName { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string Model { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int DailyCost { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int DailyMonetaryFine { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int ManufactureYear { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public string GearBox { get; set; }
    }

    [MetadataType(typeof(dbEntities))]
    public partial class CarFields 
    {
        [Required, Range(6, 8, ErrorMessage = "Driver Liecense is out of range")] // ולידציה - טווח מספרים
        public int LicensePlateNumber { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int CarTypeID { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public int CurrentKilometrage { get; set; }
        public string Picture { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public bool IsRepaired { get; set; }
        [Required] // ולידציה- השדה חייב להכיל ערך
        public bool IsAvailable { get; set; }
        public string Branch { get; set; }
    }
}