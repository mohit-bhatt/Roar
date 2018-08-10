using Roar.Api.Models;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api
{
    [RoutePrefix("api/v1/Enrollment")]
    public class EnrollmentController : ApiController
    {
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage CreateEnrollment(EnrollmentModel enrollmentModel)
        {
            var voiceItWrapper = new VoiceItWrapper();
            if (enrollmentModel.UserDetails == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User details not supplied");

            var result = voiceItWrapper.createEnrollmentByByteData(enrollmentModel.UserDetails.UserId,
                                                                   enrollmentModel.UserDetails.Password, enrollmentModel.VoiceData,
                                                                   enrollmentModel.ContentLanguage);
            
            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The server did not respond back with a result");


            return Request.CreateResponse(HttpStatusCode.OK, result);
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
