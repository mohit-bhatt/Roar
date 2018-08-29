using Roar.Api.Models;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Roar.Api.Controllers.Api
{
    [RoutePrefix("api/v1/Enrollment")]
    public class EnrollmentController : ApiController
    {
        [Route("Create/{userId}/{pw}")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateEnrollment(string userId, string pw, [FromBody] byte[] voiceData)
        {
            userId = "mohit";
            pw = "mohit";
            var voiceItWrapper = new VoiceItWrapper();
           // if (enrollmentModel.UserDetails == null)
           //     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User details not supplied");

            string result = string.Empty;

            //if (!string.IsNullOrEmpty(enrollmentModel.VoiceDataUrl))
            //{
            //    result = voiceItWrapper.createEnrollmentByWavURL(enrollmentModel.UserDetails.UserId,
            //                                           enrollmentModel.UserDetails.Password, enrollmentModel.VoiceDataUrl,
            //                                           enrollmentModel.ContentLanguage);
            //}
            //else
            //{
                //var data = await Request.Content.ReadAsByteArrayAsync();
                //var data = await Request.Content.ReadAsStringAsync();
                result = voiceItWrapper.createEnrollmentByByteData(userId,
                                                                       pw, voiceData);
            //}
            
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
