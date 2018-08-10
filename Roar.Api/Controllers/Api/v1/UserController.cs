using Roar.Api.Models;
using Roar.Api.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api
{
    [RoutePrefix("api/v1/User")]
    public class UserController : ApiController
    {
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(UserModel userModel)
        {
            var voiceItWrapper = new VoiceItWrapper();
            var result = voiceItWrapper.createUser(userModel.UserId, userModel.Password);

            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The server did not respond back with a result");


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(UserModel userModel)
        {
            var voiceItWrapper = new VoiceItWrapper();
            var result = voiceItWrapper.deleteUser(userModel.UserId, userModel.Password);

            if (string.IsNullOrEmpty(result))
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The server did not respond back with a result");


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
