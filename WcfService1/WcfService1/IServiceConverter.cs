﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    [ServiceContract]
    public interface IServiceConverter
    {

        [OperationContract]
        string ConvertCurrencyToWords(decimal currency);
    }
}
