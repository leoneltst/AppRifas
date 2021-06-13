using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pensiones.Tools
{
    public static class Propiedades
    {



        public static string GetString(string name)
        {
            
            try
            {
                string setting = ConfigurationManager.AppSettings[name];
                return setting;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
