using donationApi.DAL;
using donationApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace donationApi.Common.BL
{
    public class dbBl
    {
        DataSet oDataSet;
        sqlDal dSql = new DAL.sqlDal();
        public void spInserOrUpdateAllDataRequest(clsRequest request)
        {
            //DataSet oRequest = oDonationDal.spInserOrUpdateAllDataRequest(data);


            try
            {
                checkData checkD = new checkData();

                List<SqlParameter> lstParams = new List<SqlParameter>();

                SqlParameter sqlZehutID = new SqlParameter("@zehutID", SqlDbType.BigInt);
                sqlZehutID.Value = request.zehutID.ToString();
                lstParams.Add(sqlZehutID);


                SqlParameter sqldonationList = new SqlParameter("@donationList", SqlDbType.Structured);
                sqldonationList.Value = checkD.ToDataTable(request.donations);
                lstParams.Add(sqldonationList);

                oDataSet = dSql.runSqlLst("spInserOrUpdateAllDataRequest", lstParams);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}