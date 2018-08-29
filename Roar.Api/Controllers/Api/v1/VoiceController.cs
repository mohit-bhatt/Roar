using Roar.Api.Manager;
using Roar.Api.Models;
using Roar.Api.Utility;
using System;
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
        /// API to save voice data to Azure Blob 
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage SaveVoiceData([FromBody]byte[] voiceData)
        {
            //var wrapper = new VoiceItWrapper();
            //var result1 = wrapper.createEnrollmentByByteData("mohit", "mohit", voiceData);
            //byte[] voiceData = System.Text.Encoding.UTF8.GetBytes(voiceDataString);
            if (voiceData.Length <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Voice data found");
            }
            try
            {
                var fileName = "Test_" + DateTime.Now.Ticks.ToString() + ".wav";
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
        public HttpResponseMessage SaveUserVoiceData(EmployeeEnrollment employeeEnrollment)
        {
            try
            {
                UserVoiceManager manager = new UserVoiceManager();
                string result = string.Empty;
                result = manager.SaveUserVoiceData(employeeEnrollment);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("UserVoiceGet")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeByEnrollmentId(string enrollmentId)
        {
            try
            {
                UserVoiceManager manager = new UserVoiceManager();
                var result = manager.GetEmployeeEnrollment(enrollmentId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}