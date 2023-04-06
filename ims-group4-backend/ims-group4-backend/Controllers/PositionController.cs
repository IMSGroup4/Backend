
using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/position")]
    public class ApiController : ControllerBase{

        private readonly PositionModel pm = new PositionModel();

        [HttpGet("{id}")]

        public ActionResult<Position> Get_position(int id){
            Position? position = pm.get_by_id(id);

            if(position != null){

                var jsonObj = JsonSerializer.Serialize(position);
                Console.WriteLine(jsonObj);
                return Ok(jsonObj);
            }

            string error = ("Position with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet]
        public ActionResult<List<Position>> Get_all_positions(){

            List<Position> positions = pm.get_all_position();
            var jsonObj = JsonSerializer.Serialize(positions);
            return Ok(jsonObj);

        }

        [HttpPost]
        public ActionResult<Position> Set_position(){
            Console.WriteLine("Posting request");
            float x = 1; 
            float y = 2; 
            string time = "11:33";

            Position position = new Position();
            position.m_x_position = x;
            position.m_y_position = y;
            position.m_time_stamp = time;

            var jsonObj = JsonSerializer.Serialize(position);

            var actionName = "position";
            return CreatedAtAction(actionName, jsonObj);

        }

    }
}