using ims_group4_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/surrounding")]
    public class SurroundingController : ControllerBase{
        private SurroundingModel m_surroundingmodel = new SurroundingModel();

        [HttpGet]
        public async Task<ActionResult<List<Position>>> getSurrounding() {

            var surrondigs = await m_surroundingmodel.getSurroundings();
            return Ok(surrondigs);
        }

        [HttpPost]
        public async Task<ActionResult<PositionData>> pushSurrounding(List<Position> newSurroundings)
        {
            Console.WriteLine(newSurroundings);
            try {
                foreach (var surrounding in newSurroundings) {
                    await m_surroundingmodel.pushSurrounding(surrounding);
                }
                return Ok("Created");
            } catch{
                return BadRequest("Bad request");
            } 
        }
    }
}