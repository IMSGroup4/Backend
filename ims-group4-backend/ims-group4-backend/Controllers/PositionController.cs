
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
    [Route("api/positions")]
    public class PositionController : ControllerBase{
        private PositionModel pm = new PositionModel();
        private FirebaseModel firebaseModel = new FirebaseModel();

        [HttpGet("{id}")]

        public async Task<ActionResult<Position>> getPosition(int id){ // int id does not work

            Position position = await firebaseModel.getPosition(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Position with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet]
        public async Task<ActionResult<List<Position>>> getAllPositions(){

            var positions = await firebaseModel.getAllPositions();
            //string jsonArray = JsonConvert.SerializeObject(positions.Values);
            //Console.WriteLine(jsonArray);
            return Ok(positions);

        }

        // Out of date
        /*
        [HttpPost("{id}")]
        public async Task<ActionResult<Position>> Set_position(Position new_position, int id){

            Position position = await firebaseModel.setPosition(new_position, id);

            return Created(nameof(Get_position), position);

        }
        */

        [HttpPost]
        public async Task<ActionResult<PositionData>> pushPosition(PositionData newPosition)
        {
            Console.WriteLine(newPosition.position);
            try{
                Position position = await firebaseModel.pushPosition(newPosition.position);
                return Created(nameof(getPosition), position);
            }catch{
                return BadRequest("Bad request");
            } 
        }


    }
}