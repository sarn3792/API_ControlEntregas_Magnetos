using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class OrdenEntregaModel
    {
        public async Task<List<OrdenEntrega>> Get(Int64? idCliente)
        {
            try
            {
                String query = String.Format("SELECT * FROM OrdenesEntrega WHERE IDCliente = {0}", idCliente);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<OrdenEntrega> data = aux.AsEnumerable().Select(m => new OrdenEntrega()
                {
                    idOrdenEntrega = m.Field<Int64>("IDOrdenEntrega"),
                    idCliente = m.Field<Int64>("IDCliente"),
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

        public async Task<List<GetDetailsOrdenEntrega>> GetSpecific(Int64? idOrdenEntrega)
        {
            try
            {
                String query = String.Format(@"SELECT PO.Descripcion, POC.Cantidad 
                                            FROM OrdenesEntrega OC
                                            INNER JOIN Productos_OrdenesEntrega POC ON OC.IDOrdenEntrega = POC.IDOrdenEntrega
                                            INNER JOIN Productos PO ON PO.IDProducto = POC.IDProducto
                                            WHERE OC.IDOrdenEntrega = {0}", idOrdenEntrega);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<GetDetailsOrdenEntrega> data = aux.AsEnumerable().Select(m => new GetDetailsOrdenEntrega()
                {
                    descripcionProducto = m.Field<String>("Descripcion"),
                    cantidad = m.Field<int>("Cantidad")
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(OrdenEntrega data)
        {
            try
            {
                String query = String.Format(@"INSERT INTO OrdenesEntrega 
                                               (IDCliente, Descripcion)
                                                VALUES
                                                ({0}, '{1}')", data.idCliente, data.descripcion);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertDetails(List<InsertDetailsOrdenEntrega> data)
        {
            try
            {
                foreach(InsertDetailsOrdenEntrega singleObject in data)
                {
                    await this.InsertDetails(singleObject);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task InsertDetails(InsertDetailsOrdenEntrega data)
        {
            try
            {
                String query = String.Format(@"INSERT INTO Productos_OrdenesEntrega
                                              (IDProducto, IDOrdenEntrega, Cantidad)
                                               VALUES
                                                ({0}, {1}, {2})", data.idProducto, data.idOrdenEntrega, data.cantidad);
                DataBaseSettings db = new DataBaseSettings();
                await db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class OrdenEntrega
    {
        public Int64 ?idOrdenEntrega;
        public Int64 ?idCliente;
        public String descripcion;
        public DateTime creadoEn;

        public OrdenEntrega()
        {

        }
    }

    public class InsertDetailsOrdenEntrega
    {
        public Int64? idProducto;
        public Int64? idOrdenEntrega;
        public int cantidad;
    }

    public class GetDetailsOrdenEntrega
    {
        public String descripcionProducto;
        public int cantidad;
    }
}