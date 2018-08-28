using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models.ApiResponse
{
    public class EnrollmentResponse
    {
        public string Result { get; set; }
        public string EnrollmentID { get; set; }
        public string DetectedVoiceprintText { get; set; }
        public string DetectedTextConfidence { get; set; }
        public string ResponseCode { get; set; }
        public string TimeTaken { get; set; }
    }
}