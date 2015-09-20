using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfApplication1.Gw;

namespace Api.Item
{
    class Search : Service
    {
        private string endpoint = "item-search";
        public int page { get; set; }
        
        public dynamic Request(string querry)
        {
            var url = this.requestBaseUrl() + "item-search/" + querry + "/"+this.page.ToString();

            Console.WriteLine(url);
            return base.Request(url);
            
        }


    }
}
