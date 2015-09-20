using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Gw;

namespace Api
{
    abstract class Service : WpfApplication1.Gw.Api
    {
        public string version = "v0.9";
        public string type = "json";
        public string baseUrl = "http://www.gw2spidy.com/api";

        private dynamic data ;

        public string requestBaseUrl()
        {
            return this.baseUrl + "/" + this.version + "/" + this.type + "/";
        }

        public string Response()
        {
            return this.data;
        }


        public dynamic Request(string url)
        {
            WebClient client = new WebClient();
            string s = client.DownloadString(url);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(s);
            return data;
        }


        protected static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }

            if ((response.StatusCode == HttpStatusCode.OK ||
                 response.StatusCode == HttpStatusCode.Moved ||
                 response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
