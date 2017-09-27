using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class AditionalAccountOperations
    {
        public async Task<bool> IsEnable(String userName)
        {
            try
            {
                DataBaseSettings db = new DataBaseSettings();
                String query = String.Format(@"SELECT * FROM AspNetUsers WHERE UserName = '{0}' and Enabled = 1", userName);
                DataTable aux = await db.GetDataTable(query);

                return aux.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateStatus(IDUser user)
        {
            try
            {
                String query = String.Format("UPDATE AspNetUsers SET Enabled = {0} WHERE ID = '{1}'", user.status == true ? 1 : 0, user.idUser);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IDUser>> GetUsers(int fkCustomer)
        {
            try
            {
                String query = String.Format(@"SELECT U.Id, U.FullName, C.NombreEmpresa, U.Position, U.Email, U.Enabled
                                            FROM AspNetUsers U
                                            INNER JOIN Clientes C ON U.fkCliente = C.IDCliente
                                            WHERE U.fkCliente = {0}", fkCustomer);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<IDUser> data = aux.AsEnumerable().Select(m => new IDUser()
                {
                    idUser = m.Field<String>("Id"),
                    enterprise = m.Field<String>("NombreEmpresa"),
                    status = m.Field<bool>("Enabled"),
                    fullName = m.Field<String>("FullName"),
                    position = m.Field<String>("Position"),
                    email = m.Field<String>("Email")
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(String idUser)
        {
            try
            {
                String query = String.Format("DELETE FROM AspNetUsers WHERE Id = '{0}'", idUser);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}