using System;
using System.Collections.Generic;

namespace CDPW.BLL
{
    public class CurrentUser
    {
        public Int64 WAppUserId { get; set; }
        public Int64 WPersonId { get; set; }
        public string WAppUserName { get; set; }
        public string WAppUserPwd { get; set; }
        public string WAppUserAEmail { get; set; }
        public string WAppUserEmail { get; set; }
        public bool NoSignUp { get; set; }
        public bool NotKeepData { get; set; }
        public string WPFirstName { get; set; }
        public string WPMiddleName { get; set; }
        public string WPLastName { get; set; }
        public Int64[] PIds_CAN { get; set; }
        public Int64 PId_USA { get; set; }
        public Boolean SavedDUSA { get; set; }
        public Boolean SavedDCAN { get; set; }

        public List<UserActiveEnrollments> ActiveEnrollments { get; set; }
    }
}
