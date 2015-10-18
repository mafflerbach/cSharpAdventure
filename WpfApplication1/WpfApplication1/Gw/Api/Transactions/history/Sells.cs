using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.gw2;
using Api.Item;
using WpfApplication1.Gw;
namespace Api.Transactions.history {
  class Sells : Service {
    public dynamic Request() {
      var url = requestBaseUrl() + "commerce/transactions/history/sells";
      var request = base.Request(url);
      Console.WriteLine(request);

      var ids = this.seperateIds(request);

      var request2 = this.getItemData(ids);

      return request2;
    }
  }
}
