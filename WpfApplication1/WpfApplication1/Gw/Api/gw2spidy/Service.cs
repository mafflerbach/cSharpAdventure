using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Api.gw2spidy {
  internal abstract class Service : WpfApplication1.Gw.Api {
    public string baseUrl = "http://www.gw2spidy.com/api";

    private dynamic data;
    public string type = "json";
    public string version = "v0.9";

    public string Response() {
      return data;
    }


    public dynamic Request(string url) {
      var client = new WebClient();
      var s = client.DownloadString(url);
      var data = JsonConvert.DeserializeObject(s);
      return data;
    }

    public string requestBaseUrl() {
      return baseUrl + "/" + version + "/" + type + "/";
    }


    protected static bool DownloadRemoteImageFile(string uri, string fileName) {
      var request = (HttpWebRequest) WebRequest.Create(uri);
      HttpWebResponse response;
      try {
        response = (HttpWebResponse) request.GetResponse();
      }
      catch (Exception) {
        return false;
      }

      if ((response.StatusCode == HttpStatusCode.OK ||
           response.StatusCode == HttpStatusCode.Moved ||
           response.StatusCode == HttpStatusCode.Redirect) &&
          response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase)) {
        using (var inputStream = response.GetResponseStream()) {
          using (Stream outputStream = File.OpenWrite(fileName)) {
            var buffer = new byte[4096];
            int bytesRead;
            do {
              bytesRead = inputStream.Read(buffer, 0, buffer.Length);
              outputStream.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
          }
        }
        return true;
      }
      return false;
    }
  }
}