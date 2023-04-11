
using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FireSharp;
using FireSharp.Response;


namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/position")]
    public class PositionController : ControllerBase{

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

    }
}