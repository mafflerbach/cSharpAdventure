using System;
using Api.gw2spidy;

namespace Api.Item {
  internal class Search : Service {
    private string endpoint = "item-search";
    public int page { get; set; }

    public dynamic Request(string querry) {
      var url = requestBaseUrl() + "item-search/" + querry + "/" + page;

      return base.Request(url);
    }
  }
}