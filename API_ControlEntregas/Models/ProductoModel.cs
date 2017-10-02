﻿using System;
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

        public async Task<Int64> Insert(Producto data)
        {
            try
            {
                String query = String.Format(@"EXEC xsp_InsertProducts
                                                {0}, '{1}', '{2}'", data.idCliente, data.idParaEmpresa, data.descripcion);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = await db.GetDataTable(query);

                if(aux.Rows.Count > 0)
                {
                    return Convert.ToInt64(aux.Rows[0]["IDProducto"].ToString());
                }

                return 0;

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