using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using API_ControlEntregas.Models;
using System.Threading.Tasks;

namespace API_ControlEntregas.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("api/Clientes/{idCliente}/Productos")]
        public async Task <HttpResponseMessage> Get(Int64? idCliente)
        {
            try
            {
                if (idCliente != null)
                {
                    ProductoModel model = new ProductoModel();
                    List<Producto> data = await model.Get(idCliente);
                    if (data.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fueron encontrados productos");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/Clientes/{idCliente}/Productos")]
        public async Task <HttpResponseMessage> Post([FromBody] Producto data, [FromUri] Int64? idCliente)
        {
            try
            {
                if (data == null || idCliente == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
                else
                {
                    ProductoModel model = new ProductoModel();
                    data.idCliente = idCliente;
                    await model.Insert(data);
                    return Request.CreateResponse(HttpStatusCode.Created, data);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
