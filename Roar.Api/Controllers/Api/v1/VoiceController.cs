using Roar.Api.Manager;
using Roar.Api.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api.v1
{
    [RoutePrefix("api/v1/Voice")]
    public class VoiceController : ApiController
    {
        /// <summary>
        /// API to save voice data to Azure Blob asn return url
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage SaveVoiceData(byte[] voiceData, string fileName)
        {            
            try
            {
                UserVoiceManager manager = new UserVoiceManager();
                string result = string.Empty;
                result = manager.SaveVoiceData(voiceData, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// API to save user voice json to Cosmos Db 
        /// </summary>
        /// <returns></returns>
        [Route("UserVoiceCreate")]
        [HttpPost]
        public HttpResponseMessage SaveUserVoiceData(EnrollmentModel item)
        {
            try
            {
                UserVoiceManager manager = new UserVoiceManager();
                string result = string.Empty;
                result = manager.SaveUserVoiceData(item);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}