using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace API_ControlEntregas.Models
{
    public class DataBaseSettings
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader reader;
        private String connectionString;
        private DataTable data = new DataTable();

        public DataBaseSettings()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Testing"].ConnectionString;
                conn = new SqlConnection(connectionString);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DataTable> GetDataTable(String query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = 0;
                conn.Open();
                reader = await cmd.ExecuteReaderAsync();
                data.Load(reader);
                return data;
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public async Task ExecuteQuery(String query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandTimeout = 0;
                conn.Open();
                cmd.CommandType = CommandType.Text;
                await cmd.ExecuteNonQueryAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}