
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
    [Route("api/position")]
    public class ApiController : ControllerBase{

        private PositionModel pm = new PositionModel();
        private FirebaseModel firebaseModel = new FirebaseModel();

        [HttpGet("{id}")]

        public async Task<ActionResult<Position>> Get_position(int id){

            Position position = await firebaseModel.getPosition(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Position with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Position>>> Get_all_positions(){

            var positions = await firebaseModel.getAllPositions();
            return Ok(positions);

        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Position>> Set_position(Position new_position, int id){

            Position position = await firebaseModel.setPosition(new_position, id);

            return Created(nameof(Get_position), position);

        }

        [HttpPost]
        public async Task<ActionResult<PositionData>> Push_position(PositionData new_position)
        {
            Console.WriteLine(new_position.position);
            try{
                Position position = await firebaseModel.pushPosition(new_position.position);
                return Created(nameof(Get_position), position);
            }catch{
                return BadRequest("Bad request");
            }
            
        }


        

    }
}