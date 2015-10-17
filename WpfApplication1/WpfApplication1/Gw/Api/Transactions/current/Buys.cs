using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.gw2;
using Api.Item;
using WpfApplication1.Gw;

namespace Api.Transactions.current {
  class Buys : Service {
    
    public dynamic Request() {
      var url = requestBaseUrl() + "commerce/transactions/current/buys";

      var request = base.Request(url);
      var ids = this.seperateIds(request);
      var request2 = this.getItemData(ids);

      return request2;
    }
  }
}
