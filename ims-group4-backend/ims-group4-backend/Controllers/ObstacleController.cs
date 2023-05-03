
using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json.Linq;

namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/obstacles")]
    public class ObstacleController : ControllerBase{

        private ObstacleModel m_obstacleModel = new ObstacleModel();

        [HttpGet("{id}")]
        public async Task<ActionResult<Obstacle>> getObstacle(int id){

            Obstacle position = await m_obstacleModel.getObstacle(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Obstacle with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet]
        public async Task<ActionResult<List<Obstacle>>> getAllObstacles() {
            var obstacles = await m_obstacleModel.getAllObstacles();
            return Ok(obstacles);
        }

        [HttpGet("coordinates")]
        public async Task<ActionResult<List<Position>>> getAllObstaclesCoordinates() {
            var obstaclesCoordinates = await m_obstacleModel.getAllObstaclesCoordinates();
            return Ok(obstaclesCoordinates);
        }

        [HttpPost]
        public async Task<ActionResult<Obstacle>> pushObstacle(ObstacleData newObstacle) {
            Obstacle obstacle = await m_obstacleModel.pushObstacle(newObstacle.obstacle);
            return Created(nameof(getObstacle), obstacle);
        }
    }
}