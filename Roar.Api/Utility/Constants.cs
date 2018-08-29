using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Utility
{
    public class Constants
    {
        public const string EnrollmentSuccessResponseCode = "SUC";
        public static Dictionary<string, string> EnrollmentErrorResponseCodes = new Dictionary<string, string>
        {
            { "VPND", "Voiceprint not detected. Please speak naturally and try again." },
            { "NAVP", "No Approved Voiceprint Phrases for the Content Language en-US" },
            { "ENF", "Enrollment Failed. Cannot Use Same Recording More Than Once For Enrollment." },
            { "STTF", "Enrollment Failed. Failed Speech-To-Text Confidence <31.0%" },
            { "IFW", "Incorrect formatted wav data" },
            { "WNS", "Wave data not sent" },
            { "UNF", "User not found" },
            { "INP", "Incorrect password" },
            { "IUID", "UserId does not meet requirements, please ensure it is an alphanumeric string between 5 - 36 characters" },
            { "IFP", "Incorrect formatted password" },
            { "DIR", "DeveloperId required" },
            { "DINV", "DeveloperId is not valid" },
            { "DID", "DeveloperId is disabled" },
            { "SRNR", "Sound Recording Does Not Meet Requirements - Recording Length < 1300ms (1.3s)." },
            { "SSTQ", "Sound Recording Does Not Meet Requirements - Recording Length > 5000ms (5s)." },
            { "SSTL", "Sound Recording Does Not Meet Requirements - Speaker Speaking Too Quiet." },
            { "CLNE", "Enrollment failed. Only en-US is enabled for Free Tier." },
            { "ISE", "Internal Server Error" }
        };
    }
}