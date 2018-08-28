using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models
{
    public class EmployeeEnrollment
    {
        public string EnrollmentId { get; set; }
        public long EmployeeUid { get; set; }
        public int ClientId { get; set; }
    }
}