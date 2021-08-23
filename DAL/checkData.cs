using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace donationApi.DAL
{
    public class checkData
    {
        
        int n;
        bool resp;

        public bool chechDate(ref string fDate)
        {
            resp = int.TryParse(fDate, out n); // בדיקה אם שדה נומרי
            if (fDate.ToString().Length == 8 && resp)
            {
                fDate = fDate.Substring(0, 4) + "-" + fDate.Substring(4, 2) + "-" + fDate.Substring(6, 2);
                return true;
            }

            return false;
        }

        public bool chechTime(ref string fTime)
        {
            resp = int.TryParse(fTime, out n); // בדיקה אם שדה נומרי
            if (fTime.ToString().Length == 9 && resp)
            {
                fTime = fTime.Substring(0, 2) + ":" + fTime.Substring(2, 2) + ":" + fTime.Substring(4, 2) + "." + fTime.Substring(6, 3);
                return true;
            }
            return false;

        }

        // Convert generic List to DataTable?
        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    string propertyType = props[i].PropertyType.Name.ToUpper();
                                        
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}