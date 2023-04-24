using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
// using System.Text.Json;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/new_session")]
    public class SessionController : ControllerBase{
        private SessionModel m_sessionModel = new SessionModel();

        [HttpGet]
        public async Task<ActionResult<bool>> createSession(int id) {

            bool response = await m_sessionModel.createSession();

            if(response != false) {
                return Ok(response);
            }
            string error = ("Error when deleting all the datas");
            return Ok(error);
        }
    }
}