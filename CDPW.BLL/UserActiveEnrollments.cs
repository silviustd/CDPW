using System;

namespace CDPW.BLL
{
    public class UserActiveEnrollments
    {
        public Int64 EnrollmentId { get; set; }
        public string PLicenseType { get; set; }
        public Int16  PLicenseTypeId { get; set; }
        public Nullable<DateTime> EnrollmentBeginDate { get; set; }
        public Nullable<DateTime> EnrollmentEndDate { get; set; }
    }
}
