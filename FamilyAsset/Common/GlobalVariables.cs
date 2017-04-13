using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GlobalVariables
    {
        public static string iconPath = System.AppDomain.CurrentDomain.BaseDirectory  +
            ConfigurationManager.AppSettings["ItemSourcePath"];

        public static string backgroundPath = System.AppDomain.CurrentDomain.BaseDirectory +
            ConfigurationManager.AppSettings["BackgroundPath"];

        public static string systemIconPath = System.AppDomain.CurrentDomain.BaseDirectory +
            ConfigurationManager.AppSettings["SystemIconPath"];
    }
}
