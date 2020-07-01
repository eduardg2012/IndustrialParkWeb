using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }
}