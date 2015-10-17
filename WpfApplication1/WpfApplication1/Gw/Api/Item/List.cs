using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.gw2spidy;

namespace Api.Item {
  class List: Service {

    public int page { get; set; }

    public dynamic Request(string querry) {
      var url = requestBaseUrl() + "items/all?filter_ids=" + querry + "/" + page;

      return base.Request(url);
    }


  }
}
