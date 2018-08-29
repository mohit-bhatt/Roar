using System;
using Roar.Api.Models;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Roar.Api.Models.ApiResponse;
using Roar.Api.Manager;

namespace Roar.Api.Controllers.Api
{
    [Route("api/v1/Authentication")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("authandparse/{userid}/{pw}")]
        public HttpResponseMessage AuthenticateAndParse(string userId, string pw, byte[] voiceData)
        {

            VoiceItWrapper wrapper = new VoiceItWrapper();

            var result = wrapper.authenticationByByteData(userId, pw, voiceData);

            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The voice auth server did not respond back with a result");

            //now parse
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(result);

            if(authResponse == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while deserializing response from voice auth server");
            }

            bool r = ParseAuthResponse(authResponse);

            if(r == false)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while parsing and executing the voice command");
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        private bool ParseAuthResponse(AuthResponse authResponse)
        {
            bool result = false;
            if (authResponse.Result.Contains("Authentication successful"))
            {
                var command = authResponse.DetectedVoiceprintText;
                if(command.ToLower().Contains("punch in"))
                {
                    DoPunchIn(authResponse.EnrollmentID);
                    result = true;
                }

                if (command.ToLower().Contains("punch out"))
                {
                    DoPunchOut(authResponse.EnrollmentID);
                    result = true;
                }

                if (command.ToLower().Contains("authenticate"))
                {
                    DoAuthenticate(authResponse.EnrollmentID);
                    result = true;
                }
            }

            return result;
        }

        private void DoPunchIn(string enrollmentId)
        {
            PunchManager punchManager = new PunchManager();
            UserVoiceManager mgr = new UserVoiceManager();

            EmployeeEnrollment enrollment = mgr.GetEmployeeEnrollment(enrollmentId);

            if (enrollment != null)
            {
                Punch p = new Punch
                {
                    ClientId = enrollment.ClientId,
                    EmployeeUid = enrollment.EmployeeUid,
                    DepartmentUid = enrollment.DepartmentUid,
                    PunchDateTime = DateTime.Now,
                    PunchSourceTypeId = (byte)1, //manual
                    PunchActivityTypeId = (byte)1, //work
                    PunchStatusTypeId = (byte)1, //auto
                    IsActive = true,
                    CanBeProcessed = true,
                    UserKey = Guid.Parse("C00E2729-9FFA-E511-8893-005056BD7869")
                };

                punchManager.InsertPunch(p);
            }
            else
            {
                throw new Exception("Enrollment record could not be read from cosmosdb");
            }


        }

        private void DoPunchOut(string enrollmentId)
        {

        }

        private void DoAuthenticate(string enrollmentId)
        {

        }
    }
}
