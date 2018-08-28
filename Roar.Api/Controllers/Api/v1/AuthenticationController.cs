using Roar.Api.Models;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Roar.Api.Models.ApiResponse;

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
                }

                if (command.ToLower().Contains("punch out"))
                {
                    DoPunchOut(authResponse.EnrollmentID);
                }

                if (command.ToLower().Contains("authenticate"))
                {
                    DoAuthenticate(authResponse.EnrollmentID);
                }
            }

            return result;
        }

        private void DoPunchIn(string enrollmentId)
        {

        }

        private void DoPunchOut(string enrollmentId)
        {

        }

        private void DoAuthenticate(string enrollmentId)
        {

        }
    }
}
