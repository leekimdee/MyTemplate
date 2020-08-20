using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTemplate.Codes
{
    public class MyConnection: IDisposable
    {
        private bool disposed = false;
        private SqlConnection Cnn = null;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                // giai phong tai da su dung dang su dung
                // Free any other managed objects here.
                Cnn.Dispose();
            // Free any unmanaged objects here.
            // giai phong tai nguyen khong duoc quan ly lun; o cung, ban phim....
            disposed = true;
        }

        ~MyConnection()
        {
            Dispose(false);
        }

        public MyConnection()
        {
            string connectionString = "QtDbConStr";
            string strSQL = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            try
            {
                if (strSQL != "")
                {
                    Cnn = new SqlConnection(strSQL);
                    Cnn.Open();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public SqlDataReader GetDataReader(string strSql)
        {
            try
            {
                SqlDataReader dr;
                SqlCommand cm = new SqlCommand();
                cm.CommandText = strSql;
                cm.CommandType = CommandType.Text;
                cm.Connection = Cnn;
                dr = cm.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("Lỗi: " + ex.Message);
                return null;
            }
        }
    }
}