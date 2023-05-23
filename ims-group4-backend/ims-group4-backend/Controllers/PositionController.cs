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

            if (position != null) {
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
        public async Task<ActionResult<PositionData>> pushPosition(List<PositionData> newPositions)
        {
            Console.WriteLine(newPositions);
            try {
                foreach (var position in newPositions) {
                    await m_positionModel.pushPosition(position.position);
                }
                return Ok("Created");
            } catch {
                return BadRequest("Bad request");
            } 
        }
    }
}