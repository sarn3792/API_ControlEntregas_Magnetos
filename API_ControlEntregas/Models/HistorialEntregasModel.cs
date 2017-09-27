using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace API_ControlEntregas.Models
{
    public class HistorialEntregasModel
    {
        public async Task<Int64?> Insert(HistorialEntregas data)
        {
            try
            {
                String query = String.Format(@"INSERT INTO HistorialEntregas
                                                (IDOrdenEntrega, IDUsuario, Latitud, Longitud)
                                                OUTPUT Inserted.IDHistorialEntrega
                                                VALUES
                                                ({0}, '{1}', '{2}', '{3}')",
                                                data.idOrdenEntrega, data.idUsuario, data.latitud, data.longitud);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                if(aux.Rows.Count > 0)
                {
                    return Convert.ToInt64(aux.Rows[0]["IDHistorialEntrega"].ToString());
                }

                throw new Exception("Ha ocurrido un error al guardar la entrega");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GuardarFotos(Images data)
        {
            try
            {
                foreach(byte[] array in data.images)
                {
                    String query = String.Format(@"INSERT INTO Fotos
                                                   (IDHistorialEntrega, Foto)
                                                    VALUES
                                                    ({0}, CONVERT(VARBINARY(MAX), '{1}{2}', 1))", data.idHistorialEntrega, "0x", new StringBuilder(BitConverter.ToString(array).ToLower()).Replace("-", string.Empty));
                    DataBaseSettings db = new DataBaseSettings();
                    await db.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GuardarFirmas(Images data)
        {
            try
            {
                foreach (byte[] array in data.images)
                {
                    String query = String.Format(@"INSERT INTO Firmas
                                                   (IDHistorialEntrega, Firma)
                                                    VALUES
                                                    ({0}, CONVERT(VARBINARY(MAX), '{1}{2}', 1))", data.idHistorialEntrega, "0x", new StringBuilder(BitConverter.ToString(array).ToLower()).Replace("-", string.Empty));
                    DataBaseSettings db = new DataBaseSettings();
                    await db.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HistorialEntregas> GetSpecific(Int64? id)
        {
            try
            {
                String query = String.Format(@"SELECT HE.IDHistorialEntrega, HE.IDOrdenEntrega, USR.FullName 'Usuario', HE.Latitud, HE.Longitud, HE.FechaEntrega
                                                FROM HistorialEntregas HE
                                                INNER JOIN AspNetUsers USR ON HE.IDUsuario = USR.Id
                                                WHERE IDHistorialEntrega = {0}", id);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                List<HistorialEntregas> data = aux.AsEnumerable().Select(m => new HistorialEntregas()
                {
                    idHistorialEntrega = m.Field<Int64>("IDHistorialEntrega"),
                    idOrdenEntrega = m.Field<Int64>("IDOrdenEntrega"),
                    idUsuario = m.Field<String>("Usuario"),
                    latitud = m.Field<String>("Latitud"),
                    longitud = m.Field<String>("Longitud"),
                    fechaEntrega = m.Field<DateTime>("FechaEntrega")
                }).ToList();

                HistorialEntregas result = data.First();
                result.fotos = (await this.GetSpecificFotos(id)).images;
                result.firmas = (await this.GetSpecificFirmas(id)).images;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Images> GetSpecificFotos(Int64? idHistorialEntregas)
        {
            try
            {
                String query = String.Format("SELECT * FROM Fotos WHERE IDHistorialEntrega = {0}", idHistorialEntregas);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                Images data = new Images();
                foreach (DataRow row in aux.Rows)
                {
                    data.images.Add((byte[])row["Foto"]);
                }

                data.idHistorialEntrega = idHistorialEntregas;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Images> GetSpecificFirmas(Int64? idHistorialEntregas)
        {
            try
            {
                String query = String.Format("SELECT * FROM Firmas WHERE IDHistorialEntrega = {0}", idHistorialEntregas);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                Images data = new Images();
                foreach (DataRow row in aux.Rows)
                {
                    data.images.Add((byte[])row["Firma"]);
                }

                data.idHistorialEntrega = idHistorialEntregas;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class HistorialEntregas
    {
        public Int64? idHistorialEntrega;
        public Int64? idOrdenEntrega;
        public String idUsuario;
        public String latitud;
        public String longitud;
        public DateTime fechaEntrega;
        public List<byte[]> firmas;
        public List<byte[]> fotos;

        public HistorialEntregas()
        {

        }
    }

    public class Images
    {
        public Int64? idHistorialEntrega;
        public List<byte[]> images;

        public Images()
        {
            images = new List<byte[]>();
        }

        public Images(Int64? idHistorialEntrega, List<byte[]> images)
        {
            this.idHistorialEntrega = idHistorialEntrega;
            this.images = images;
        }
    }
}