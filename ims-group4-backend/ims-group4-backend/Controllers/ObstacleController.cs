
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

        private ObstacleModel om = new ObstacleModel();
        private FirebaseModel firebaseModel = new FirebaseModel();
		private GoogleApiModel googleApiModel = new GoogleApiModel();

        [HttpGet("{id}")]
        public async Task<ActionResult<Obstacle>> getObstacle(int id){

            Obstacle position = await firebaseModel.getObstacle(id);

            if(position != null){
                return Ok(position);
            }
            string error = ("Obstacle with id " + id + " does not exist");
            return Ok(error);
        }

        [HttpGet]
        public async Task<ActionResult<List<Obstacle>>> getAllObstacles() {
            var obstacles = await firebaseModel.getAllObstacles();
            return Ok(obstacles);

        }

        [HttpPost]
        public async Task<ActionResult<Obstacle>> pushObstacle(ObstacleData new_obstacle) {
			List<Google.Cloud.Vision.V1.EntityAnnotation> annotations = await googleApiModel.DetectImage(new_obstacle.obstacle.base64_image!);

            new_obstacle.obstacle.infos_image = JObject.Parse(JsonSerializer.Serialize(annotations[0]));
            Obstacle obstacle = await firebaseModel.pushObstacle(new_obstacle.obstacle);

            return Created(nameof(getObstacle), obstacle);
        }
    }
}