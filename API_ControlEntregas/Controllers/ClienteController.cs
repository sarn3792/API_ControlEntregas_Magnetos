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
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("api/Clientes")]
        public async Task <HttpResponseMessage> Get()
        {
            try
            {
                ClienteModel model = new ClienteModel();
                List<Cliente> data = await model.Get();
                if(data.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No fueron encontrados clientes");
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/Clientes")]
        public async Task<HttpResponseMessage> Post([FromBody] Cliente data)
        {
            try
            {
                if(data == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
                else
                {
                    ClienteModel model = new ClienteModel();
                    await model.Insert(data);
                    return Request.CreateResponse(HttpStatusCode.Created, data);
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/Clientes/Estatus")]
        public async Task <HttpResponseMessage> Put([FromBody] Cliente data)
        {
            try
            {
                if (data == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parámetro nulo");
                }
                else
                {
                    ClienteModel model = new ClienteModel();
                    await model.UpdateStatus(data);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
