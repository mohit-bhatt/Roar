using Roar.Api.Models;
using Roar.Api.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api
{
    [Route("api/v1/Authentication")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("authandparse")]
        public HttpResponseMessage AuthenticateAndParse(EnrollmentModel enrollmentModel)
        {
            if((enrollmentModel == null) || (enrollmentModel.UserDetails == null))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid enrollment model provided");
            }

            VoiceItWrapper wrapper = new VoiceItWrapper();

            var result = wrapper.authenticationByWavURL(enrollmentModel.UserDetails.UserId, enrollmentModel.UserDetails.Password, enrollmentModel.VoiceDataUrl);

            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The voice auth server did not respond back with a result");


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
