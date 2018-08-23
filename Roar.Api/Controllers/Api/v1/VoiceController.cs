using Roar.Api.Manager;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roar.Api.Controllers.Api.v1
{
    public class VoiceController : ApiController
    {
        private readonly IUserVoiceManager _userVoiceManager;
        //public VoiceController(IUserVoiceManager userVoiceManager)
        //{
        //    _userVoiceManager = userVoiceManager;
        //}
        [HttpPost]
        public HttpResponseMessage SaveVoiceData()
        {
            UserVoiceManager manager = new UserVoiceManager();
            var fileName = "Anupam_test.wav";
            try
            {
                byte[] bytes = File.ReadAllBytes(@"C:\Users\achopra\Desktop\Anupam_test.wav");
                if (bytes.Length <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Voice data found");
                }
                string result = string.Empty;
                result = manager.SaveVoiceData(bytes, fileName);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}