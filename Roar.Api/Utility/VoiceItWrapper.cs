using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace Roar.Api.Utility
{
    public class VoiceItWrapper
    {
        string developerId;

        public VoiceItWrapper(string devId)
        {
            this.developerId = devId;
        }

        public VoiceItWrapper()
        {
            this.developerId = ConfigurationManager.AppSettings["VoiceItDevId"];
        }

        //Function to convert password string into a SHA256 Hexadecimal Hash
        public static string GetSha256FromString(string strData)
        {
            var message = Encoding.ASCII.GetBytes(strData);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        //Function to create a User
        public string createUser(string userId, string passwd)
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/users");
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of createUser Method

        //Function to delete the user
        public string deleteUser(string userId, string passwd)
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/users");
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "DELETE";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of deleteUser Method

        //Function to retrieve User Details
        public string getUser(string userId, string passwd)
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/users");
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "GET";

            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();


                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of getUser Method

        //Function to create a new Enrollment
        public string createEnrollment(string userId, string passwd, string pathToEnrollmentWav, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);

            byte[] wavData = System.IO.File.ReadAllBytes(pathToEnrollmentWav);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments");
            request.ContentType = "audio/wav";
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            request.ContentLength = wavData.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(wavData, 0, wavData.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of createEnrollment Method

        //Function to create a new Enrollment
        public string createEnrollmentByByteData(string userId, string passwd, byte[] wavData, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);

            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments");
            request.ContentType = "audio/wav";
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            request.ContentLength = wavData.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(wavData, 0, wavData.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of createEnrollment Method

        //Function to create a new Enrollment by Wav URL
        public string createEnrollmentByWavURL(string userId, string passwd, string urlToEnrollmentWav, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments/bywavurl");
            request.ContentType = "audio/wav";
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["VsitwavURL"] = urlToEnrollmentWav;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of createEnrollmentByWavURL Method

        //Function to delete a specific enrollment
        public string deleteEnrollment(string userId, string passwd, string enrollmentId)
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments" + "/" + enrollmentId);
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "DELETE";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of deleteEnrollment Method

        //Function to retrieve array of User Enrollments
        public string getEnrollments(string userId, string passwd)
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments");
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "GET";

            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();


                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of getEnrollments Method

        //Function to retrieve the count of enrollments for a specific phrase for a specificUser
        public string getEnrollmentsCount(string userId, string passwd, string vppText, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);

            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/enrollments/count");
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VppText"] = vppText;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["PlatformID"] = "4";
            request.Method = "GET";

            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();


                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of getEnrollmentsCount Method

        //Function to authenticate your Voice Print
        public string authentication(string userId, string passwd, string pathToAuthenticationWav, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            byte[] wavData = System.IO.File.ReadAllBytes(pathToAuthenticationWav);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/authentications");
            request.ContentType = "audio/wav";
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            request.ContentLength = wavData.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(wavData, 0, wavData.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of authentication Method

        //Function to authenticate your Voice Print
        public string authenticationByByteData(string userId, string passwd, byte[] wavData, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);

            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/authentications");
            request.ContentType = "audio/wav";
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            request.ContentLength = wavData.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(wavData, 0, wavData.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of authentication Method

        //Function to authenticate your Voice Print
        public string authenticationByWavURL(string userId, string passwd, string urlToAuthenticationWav, string contentLanguage = "")
        {

            var password = GetSha256FromString(passwd);
            //password = GetSha256FromString(password);
            // Create a request for the URL.
            WebRequest request = WebRequest.Create("https://siv.voiceprintportal.com/sivservice/api/authentications/bywavurl");
            request.Headers["VsitwavURL"] = urlToAuthenticationWav;
            request.Headers["UserId"] = userId;
            request.Headers["VsitPassword"] = password;
            request.Headers["VsitDeveloperId"] = this.developerId;
            request.Headers["ContentLanguage"] = contentLanguage;
            request.Headers["PlatformID"] = "4";
            request.Method = "POST";
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            try
            {
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine (((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine (responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
                // Clean up the streams.

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var response = (HttpWebResponse)ex.Response)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"))) { return reader.ReadToEnd(); }
                        }
                    }
                }
            }
            return "";
        }//End of authenticationByWavURL Method

    }//End of Class Declaration
}