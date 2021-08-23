using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace donationApi.Common
{
    public  class  globalsDonation
    {
        private const string DATA_BASE = "donation";


        public string DATA_BASe { get { return DATA_BASE; } }
    }
    public static class globalParm
    {
        public const string cntDataSet = "klali";
        public const string cntRcTable = "TableRC";
        public const string cntDataTable = "TableData";
        public const string cntRc = "rc";
        public const string cntRcMsg = "rcMsg";
        public const string cntRcMsgExp = "rcMsgExp";
    }
   
}