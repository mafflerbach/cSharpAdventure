using Api.gw2;
using Api.Item;

namespace Api.Transactions.history {
  class Buys : Service {
    public dynamic Request(string querry) {
      var url = requestBaseUrl() + "commerce/transactions/history/buys";
      var request = base.Request(url);
      var ids = this.seperateIds(request);
      var request2 = this.getItemData(ids);
      return request2;
    }
  }
}
