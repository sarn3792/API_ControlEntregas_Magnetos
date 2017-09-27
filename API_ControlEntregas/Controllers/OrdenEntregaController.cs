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
    public class OrdenEntregaController : ApiController
    {
        [HttpGet]
        [Route("api/Clientes/{idCliente}/OrdenesEntrega")]
        public async Task<HttpResponseMessage> Get(Int64? idCliente)
        {
            try
            {
                if(idCliente != null)
                {
                    OrdenEntregaModel model = new OrdenEntregaModel();
                    List<OrdenEntrega> data = await model.Get(idCliente);
                    if(data.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fueron encontradas órdenes de compra");
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

        [HttpGet]
        [Route("api/Clientes/{idCliente}/OrdenesEntrega/{idOrdenEntrega}")]
        public async Task<HttpResponseMessage> GetSpecific(Int64? idOrdenEntrega)
        {
            try
            {
                if(idOrdenEntrega != null)
                {
                    OrdenEntregaModel model = new OrdenEntregaModel();
                    List<GetDetailsOrdenEntrega> data = await model.GetSpecific(idOrdenEntrega);
                    if(data.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fueron encontradas órdenes de compra");
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
        [Route("api/Clientes/{idCliente}/OrdenesEntrega")]
        public async Task<HttpResponseMessage> Post([FromBody] OrdenEntrega data, [FromUri] Int64? idCliente)
        {
            try
            {
                if(data == null || idCliente == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
                else
                {
                    OrdenEntregaModel model = new OrdenEntregaModel();
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

        [HttpPost]
        [Route("api/Clientes/{idCliente}/OrdenesEntrega/Detalle")]
        public async Task<HttpResponseMessage> PostDetails([FromBody] List<InsertDetailsOrdenEntrega> data, [FromUri] Int64? idCliente)
        {
            try
            {
                if(idCliente == null || data == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
                else
                {
                    OrdenEntregaModel model = new OrdenEntregaModel();
                    await model.InsertDetails(data);
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
