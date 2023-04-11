
using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FireSharp;
using FireSharp.Response;


namespace ims_group4_backend.Controllers{
    [ApiController]
    [Route("api/obstacle")]
    public class ObstacleController : ControllerBase{

        private ObstacleModel om = new ObstacleModel();
        private FirebaseModel firebaseModel = new FirebaseModel();

        [HttpGet("{id}")]
        public async Task<ActionResult<Obstacle>> Get_obstacle(int id){

            Obstacle position = await firebaseModel.getObstacle(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Obstacle with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Obstacle>>> Get_all_obstacles() {
            var obstacles = await firebaseModel.getAllObstacles();
            return Ok(obstacles);

        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Obstacle>> Set_obstacle(Obstacle new_obstacle, int id) {
            Obstacle obstacle = await firebaseModel.setObstacle(new_obstacle, id);
            return Created(nameof(Get_obstacle), obstacle);
        }

    }
}