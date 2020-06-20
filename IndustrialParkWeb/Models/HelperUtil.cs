using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IndustrialParkWeb.Models
{
    public class HelperUtil
    {
        public static string ObtenerValorConfig(string nombreConfig)
        {
            string valor = "";
            try
            {
                valor = (string)ConfigurationManager.AppSettings.Get(nombreConfig);
            }
            catch (Exception)
            {
                throw;
            }
            return valor;
        }
    }
}