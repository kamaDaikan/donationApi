using donationApi.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace donationApi.DAL
{
    public class sqlDal
    {
        private static string us;
        private static string ps;
        cSQL oSQL = null;
        //int  rc = 0;
        //string rcMsg;
        //string rcExepMsg;
        public DataTable oDataTable;
        public DataSet oDataSet;
        public HttpContext context = HttpContext.Current;


        public sqlDal()
        {

            oDataSet = createDs1();
            oSQL = new cSQL(ref oDataSet);

        }

        // בניית JSON להחזרה
        public string DataTableToJSONWithJSONNet(DataTable table)
        {

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);

            return JSONString;
        }


        public DataSet ResponseDs(DataTable rTabel, int rc, string RCMSG, string rcExepMsg)
        {
            //if (rTabel != null) { if (rTabel.Rows.Count > 0) {if(rTabel.Columns.Contains(RcColumn)) {} } }
            DataSet set = createDs(rc.ToString(), RCMSG, rcExepMsg);
            if (rTabel != null)
            {
                if (rTabel.Rows.Count > 0)
                {
                    rTabel.TableName = globalParm.cntDataTable;
                    set.Tables.Add(rTabel);
                }
            }
            return set;
        }



        public HttpStatusCode ResponseHttpStatusCode(DataSet oDataSet1)
        {

            if (oDataSet1.Tables[globalParm.cntRcTable].Select(globalParm.cntRc + "<>0").Count() > 0)
            {
                return HttpStatusCode.InternalServerError;
            }
            return HttpStatusCode.OK;

        }





        public DataTable createDt(string rc, string RCMSG)
        {

            DataTable table = new DataTable();
            table.Columns.Add(globalParm.cntRc, typeof(string));
            table.Columns.Add(globalParm.cntRcMsg, typeof(string));
            table.Rows.Add(rc, RCMSG);
            table.TableName = "d";
            return table;



        }

        public DataSet createDs(string rc, string RCMSG, string rcExepMsg)
        {

            DataTable table = new DataTable();
            table.Columns.Add(globalParm.cntRc, typeof(string));
            table.Columns.Add(globalParm.cntRcMsg, typeof(string));
            table.Columns.Add(globalParm.cntRcMsgExp, typeof(string));
            table.Rows.Add(rc, RCMSG, rcExepMsg);
            table.TableName = globalParm.cntRcTable;
            DataSet set = new DataSet(globalParm.cntDataSet);
            set.Tables.Add(table);
            return set;


        }

        public DataSet createDs1()
        {

            DataTable table = new DataTable();
            table.Columns.Add(globalParm.cntRc, typeof(string));
            table.Columns.Add(globalParm.cntRcMsg, typeof(string));
            table.Columns.Add(globalParm.cntRcMsgExp, typeof(string));

            table.TableName = globalParm.cntRcTable;
            DataSet set = new DataSet(globalParm.cntDataSet);
            set.Tables.Add(table);
            return set;


        }



        // שליפת יוזר וסיסמא מוצפנים
        public void getUserDet()
        {
            us = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["User"]);
            ps = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Pass"]);
            
        }

        // פנייה לשרת ושליפת נתונים

        public DataSet runSql(string spName, Dictionary<string, string> Searchparam)
        {


            getUserDet();  // שליפת יוזר וסיסמא מוצפנים
            try
            {
                    DataSet odataset = new DataSet();
                    oSQL.OpenSQLConnection();


                    odataset = oSQL.ExecuteStoredProcedure(spName, false, Searchparam);
                    if (odataset != null)
                    {
                        if (odataset.Tables.Count > 0)
                        {
                            foreach (DataTable table in odataset.Tables)
                            {
                                oDataTable = table.Copy();
                                oDataSet.Tables.Add(oDataTable);
                            }

                        }
                    oDataSet.Tables[globalParm.cntRcTable].Rows.Add(0, "", "");
                    oSQL.CloseSQLConnection();  // סגירת חיבור ל sql

                }

            }
            catch (Exception ex)
            {

                oDataSet.Tables[globalParm.cntRcTable].Rows.Add(8, "  שגיאה ב impersonate  ", ex.ToString());

                throw ex;

            }

            return oDataSet;

        }

        // פנייה לשרת ושליפת נתונים

        public DataSet runSqlLst(string spName, List<SqlParameter> paramsList)
        {


            getUserDet();  // שליפת יוזר וסיסמא מוצפנים
            try
            {
                    DataSet odataset = new DataSet();
                    oSQL.OpenSQLConnection();
                    oSQL.ExecuteStoredProcedureLst(spName, true, paramsList);
                    if (odataset != null)
                    {
                        if (odataset.Tables.Count > 0)
                        {
                            foreach (DataTable table in odataset.Tables)
                            {
                                oDataTable = table.Copy();
                                oDataSet.Tables.Add(oDataTable);
                            }

                        }
                    oDataSet.Tables[globalParm.cntRcTable].Rows.Add(0, "", "");
                    oSQL.CloseSQLConnection();  // סגירת חיבור ל sql

                }

            }
            catch (Exception ex)
            {

                oDataSet.Tables[globalParm.cntRcTable].Rows.Add(8, "  שגיאה ב impersonate  ", ex.ToString());

                throw ex;

            }

            return oDataSet;

        }

        //public int GetIntRunSql(string spName, Dictionary<string, string> Searchparam, SqlParameter returnValue)
        //{
        //    int result = 0;
        //    getUserDet();  // שליפת יוזר וסיסמא מוצפנים
        //    try
        //    {
        //        {
        //            oSQL.OpenSQLConnection();
        //            result = oSQL.GetIntValExecuteStoredProcedure(spName, false, Searchparam, returnValue);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        oSQL.CloseSQLConnection();  // סגירת חיבור ל sql

        //    }

        //    return result;

        //}
     

       
        public DataTable buildSucess()
        {
            oDataTable.Clear();
            oDataTable.Columns.Add(globalParm.cntRc, typeof(int));
            oDataTable.Rows.Add(new Object[] { 0 });
            return oDataTable;
        }


    }
    public class cSQL
    {


        //This class main issue is to deal with data base opretion.


        // buildXmlData bXmlData = new buildXmlData();
        private System.Data.SqlClient.SqlConnection oConn;
        string[] searchstrings = new string[101];
        // string parmeterstrings = "";
        // int kResponseData;
        // int i;
        string[] tmpPropert = new string[2];
        // SqlDataReader tmpReader;
        // StringBuilder xmlstring;

        public SqlCommand objCmd;
        public SqlDataAdapter da;
        public DataTable oDataTable;
        public DataSet oDataSet;
        public SqlException oex;
        Common.globalsDonation cn = new Common.globalsDonation(); 

            

        public cSQL(ref  DataSet MoDataSet)
        {
            oDataSet = MoDataSet;
        }

        public cSQL()
        {

        }



        public void OpenSQLConnection()
        {

            try
            {
                oConn = new System.Data.SqlClient.SqlConnection("Data Source=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DATA_SOURCE"]) + ";Initial Catalog=" + cn.DATA_BASe + ";Integrated Security=True");
                oConn.Open();

            }
            catch (Exception ex)
            {
                exitService();
                throw new Exception(ex.Message);

            }
        }

        public void CloseSQLConnection()
        {
            try
            {

                oConn.Close();
                oConn = null;



            }
            catch (Exception ex)
            {
                oDataSet.Tables[globalParm.cntRcTable].Rows.Add(5, "   נכשלה פעולת סגירת חיבור ל - sqlServer ", ex.ToString());
                oConn = null;
            }


        }

        public void exitService()
        {
            // kResponseData = 0;
            tmpPropert[0] = " ";
            CloseSQLConnection();
            // סגירת חיבור ל sql
        }

        // פונקציה כללית לקריאה ל SQL 
        public DataSet ExecuteStoredProcedure(string SpName, bool byStoredProce, Dictionary<string, string> Searchparam)
        {
            //    public bool ExecuteStoredProcedure(string SpName, ref   System.Data.SqlClient.SqlParameter[] param, bool byStoredProce = true)

            //  oDataSet = null;
            oex = null;
            DataSet oDataSetData;

            try
            {

                oDataSetData = new DataSet(Convert.ToString(globalParm.cntDataTable));
                objCmd = new SqlCommand(SpName.ToString(), oConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                if (!byStoredProce)
                {
                    objCmd.CommandType = CommandType.Text;
                }

                using (SqlCommand cmd = new SqlCommand(SpName.ToString(), oConn)) // 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    fillData(SpName, ref  Searchparam, cmd);
                    da = new SqlDataAdapter(cmd);


                    da.Fill(oDataSetData);
                }



            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            return oDataSetData;

        }


        // פונקציה כללית לקריאה ל SQL 
        public DataSet ExecuteStoredProcedureLst(string SpName, bool byStoredProce, List<SqlParameter> paramsList)
        {

            oex = null;
            DataSet oDataSetData;

            try
            {

                oDataSetData = new DataSet(Convert.ToString(globalParm.cntDataTable));
                objCmd = new SqlCommand(SpName.ToString(), oConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                if (!byStoredProce)
                {
                    objCmd.CommandType = CommandType.Text;
                }

                using (SqlCommand cmd = new SqlCommand(SpName.ToString(), oConn)) // 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(paramsList.ToArray());
                    da = new SqlDataAdapter(cmd);
                    da.Fill(oDataSetData);
                }



            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);

            }
            return oDataSetData;

        }



        public int GetIntValExecuteStoredProcedure(string SpName, bool byStoredProce, Dictionary<string, string> Searchparam, SqlParameter returnValue)
        {
            oex = null;
            DataSet oDataSetData;
            try
            {
                oDataSetData = new DataSet(Convert.ToString(globalParm.cntDataTable));
                objCmd = new SqlCommand(SpName.ToString(), oConn);
                objCmd.CommandType = CommandType.StoredProcedure;
                if (!byStoredProce)
                {
                    objCmd.CommandType = CommandType.Text;
                }

                using (SqlCommand cmd = new SqlCommand(SpName.ToString(), oConn)) // 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    fillData(SpName, ref  Searchparam, cmd);
                    da = new SqlDataAdapter(cmd);
                    cmd.ExecuteScalar();
                    if (returnValue.Value is DBNull)
                    {
                        objCmd.Parameters.Clear();
                        return -1;
                    }
                    objCmd.Parameters.Clear();
                    return Convert.ToInt32(returnValue.Value);
                }

            }
            catch (SqlException ex)
            {
                string str = ex.ToString();
                return -1;


            }
        }

        public void fillData(string SpName, ref Dictionary<string, string> Searchparam, SqlCommand cmd)
        {
            foreach (KeyValuePair<string, string> item in Searchparam)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }





        }

    }
}