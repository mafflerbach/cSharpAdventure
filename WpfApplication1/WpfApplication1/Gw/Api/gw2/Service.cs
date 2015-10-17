using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Api.Item;
using Newtonsoft.Json;

namespace Api.gw2 {
  internal abstract class Service : WpfApplication1.Gw.Api {
    public string baseUrl = "https://api.guildwars2.com";
    
    private dynamic data;
    public string type = "json";
    public string version = "v2";

    public string Response() {
      return data;
    }


    public dynamic Request(string url) {

      var client = new WebClient();
      client.Headers.Add("Accept", "*/*");
      client.Headers.Add("Accept-Language", "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
      client.Headers.Add("Authorization", "Bearer "+ WpfApplication1.Properties.Settings.Default.API_KEY);

      var s = client.DownloadString(url);
      var data = JsonConvert.DeserializeObject(s);
      return data;
    }

    public dynamic getItemData(ArrayList ids) {
      var itemList = new List();

      var request2 = itemList.Request(String.Join(",", ids.ToArray()));

      return request2;
    }

    public ArrayList seperateIds(dynamic request) {
      ArrayList ids = new ArrayList();

      foreach (var obj in request) {
        ids.Add(obj.item_id);
      }

      return ids;
    }


    public string requestBaseUrl() {
      return baseUrl + "/" + version + "/";
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