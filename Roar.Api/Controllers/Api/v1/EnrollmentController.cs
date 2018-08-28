using Newtonsoft.Json;
using Roar.Api.Manager;
using Roar.Api.Models;
using Roar.Api.Models.ApiResponse;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api
{
    [RoutePrefix("api/v1/Enrollment")]
    public class EnrollmentController : ApiController
    {
        [Route("Create/{userId}/{pw}/{clientId}/{employeeUid}")]
        [HttpPost]
        public HttpResponseMessage CreateEnrollment(string userId, string pw, int clientId, long employeeUid, [FromBody] byte[] voiceData)
        {
            var voiceItWrapper = new VoiceItWrapper();

            string result = string.Empty;

            result = voiceItWrapper.createEnrollmentByByteData(userId,
                                                                    pw, voiceData);
            
            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The server did not respond back with a result");

            var enrollmentResponse = JsonConvert.DeserializeObject<EnrollmentResponse>(result);

            if (enrollmentResponse == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while deserializing response from voice auth server");
            }

            if(enrollmentResponse.Result.ToLower().Contains("Success"))
            {
                var userVoiceManager = new UserVoiceManager();
                userVoiceManager.SaveUserVoiceData(new EnrollmentModel { ClientId = clientId, EnrollmentId = enrollmentResponse.EnrollmentID, EmployeeUid = employeeUid});
            }

            return Request.CreateResponse(HttpStatusCode.OK, enrollmentResponse);
        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteEnrollment(EnrollmentModel enrollmentModel)
        {
            var voiceItWrapper = new VoiceItWrapper();
            if (enrollmentModel.UserDetails == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User details not supplied");

            var result = voiceItWrapper.deleteEnrollment(enrollmentModel.UserDetails.UserId,
                                                                   enrollmentModel.UserDetails.Password, enrollmentModel.EnrollmentId);

            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The server did not respond back with a result");


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
