
using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/position")]
    public class ApiController : ControllerBase{

        private readonly PositionModel pm = new PositionModel();

        [HttpGet("{id}")]

        public ActionResult<Position> Get_position(int id){
            Position? position = pm.get_by_id(id);
            return Ok(position);

        }

        [HttpGet]
        public ActionResult<List<Position>> Get_all_positions(){
            List<Position> positions = pm.get_all_position();
            return Ok(positions);
        }

    }
}