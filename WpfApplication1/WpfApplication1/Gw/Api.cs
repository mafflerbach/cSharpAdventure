using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Gw
{
    interface Api
    {
        string Response();
        dynamic Request(string url);
    }
        
}
