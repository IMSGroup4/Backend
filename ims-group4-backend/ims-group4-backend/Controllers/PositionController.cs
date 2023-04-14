
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
        private PositionModel m_positionModel = new PositionModel();

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> getPosition(int id){ // int id does not work

            Position position = await m_positionModel.getPosition(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Position with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet]
        public async Task<ActionResult<List<Position>>> getAllPositions(){

            var positions = await m_positionModel.getAllPositions();
            return Ok(positions);

        }

        [HttpPost]
        public async Task<ActionResult<PositionData>> pushPosition(PositionData newPosition)
        {
            Console.WriteLine(newPosition.position);
            try{
                Position position = await m_positionModel.pushPosition(newPosition.position);
                return Created(nameof(getPosition), position);
            }catch{
                return BadRequest("Bad request");
            } 
        }
    }
}