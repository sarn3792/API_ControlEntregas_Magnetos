using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class ClienteModel
    {
        public async Task <List<Cliente>> Get()
        {
            try
            {
                String query = String.Format("SELECT * FROM Clientes");
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<Cliente> data = aux.AsEnumerable().Select(m => new Cliente()
                {
                    idCliente = m.Field<int>("IDCliente"),
                    nombreEmpresa = m.Field<String>("NombreEmpresa"),
                    contactoSistemas = m.Field<String>("ContactoSistemas"),
                    telefono = m.Field<String>("Telefono"),
                    email = m.Field<String>("Email"),
                    grupo = m.Field<String>("Grupo"),
                    activo = m.Field<bool>("Estatus")
                }).ToList();

                return data;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Cliente data)
        {
            try
            {
                String query = String.Format(@"INSERT INTO [dbo].[Clientes]
                                               ([NombreEmpresa]
                                               ,[ContactoSistemas]
                                               ,[Telefono]
                                               ,[Email]
                                               ,[Grupo]
                                               ,[Estatus])
                                         VALUES
                                               ('{0}'
                                               ,'{1}'
                                               ,'{2}'
                                               ,'{3}'
                                               ,'{4}'
                                               ,{5})", data.nombreEmpresa, data.contactoSistemas, data.telefono, data.email, data.grupo, data.activo == true ? 1 : 0);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateStatus(Cliente data)
        {
            try
            {
                String query = String.Format("UPDATE Clientes SET Estatus = {0} WHERE IDCliente = {1}", data.activo == true ? 1 : 0, data.idCliente);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Cliente
    {
        public int idCliente;
        public String nombreEmpresa;
        public String contactoSistemas;
        public String telefono;
        public String email;
        public String grupo;
        public bool activo;

        public Cliente()
        {
            
        }
    }
}