using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class ProductoModel
    {
        public async Task<List<Producto>> Get(Int64? idCliente)
        {
            try
            {
                String query = String.Format("SELECT * FROM Productos WHERE IDCliente = {0}", idCliente);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<Producto> data = aux.AsEnumerable().Select(m => new Producto()
                {
                    idProducto = m.Field<Int64>("IDProducto"),
                    idCliente = m.Field<Int64>("IDCliente"),
                    idParaEmpresa = m.Field<String>("IDParaEmpresa"),
                    descripcion = m.Field<String>("Descripcion"),
                    creadoEn = m.Field<DateTime>("CreadoEn")
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Producto data)
        {
            try
            {
                String query = String.Format(@"INSERT INTO Productos 
                                               (IDCliente, IDParaEmpresa, Descripcion)
                                                VALUES
                                                ({0}, '{1}', '{2}')", data.idCliente, data.idParaEmpresa, data.descripcion);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Producto
    {
        public Int64 idProducto;
        public Int64 ?idCliente;
        public String idParaEmpresa;
        public String descripcion;
        public DateTime creadoEn;

        public Producto()
        {

        }
    }
}