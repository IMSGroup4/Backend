using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{

    public class ObstacleModel{

        private IFirebaseClient m_firebaseClient;
        private GoogleApiModel m_googleApiModel = new GoogleApiModel();
        public ObstacleModel() {
            FirebaseModel firebaseModel = new FirebaseModel();
            m_firebaseClient = firebaseModel.m_firebaseClient;
        }
        public async Task<Obstacle> getObstacle(int id){

            FirebaseResponse response = await m_firebaseClient.GetAsync("mower/obstacles/" + id);
            Obstacle pos = response.ResultAs<Obstacle>();
            return response.ResultAs<Obstacle>();
        }

        public async Task<List<Obstacle>?> getAllObstacles(){
            FirebaseResponse response = await m_firebaseClient.GetAsync("mower/obstacles/");
            string obstaclesJson = JsonConvert.SerializeObject(response.ResultAs<Dictionary<String, Obstacle>>().Values);
            List<Obstacle>? obstacleList = JsonConvert.DeserializeObject<List<Obstacle>>(obstaclesJson);
            return obstacleList;
        }

        public async Task<Obstacle> pushObstacle(Obstacle newObstacle) {
            List<Google.Cloud.Vision.V1.EntityAnnotation> annotations = await m_googleApiModel.DetectImage(newObstacle.base64_image!);
            
            string annotationJson = JsonConvert.SerializeObject(annotations[0]);
            ImageInformation? infos_image = JsonConvert.DeserializeObject<ImageInformation>(annotationJson);

            newObstacle.infos_image = infos_image;

            Console.WriteLine("New Obstacle");
            Console.WriteLine(newObstacle);

            PushResponse response = await m_firebaseClient.PushAsync("mower/obstacles/", newObstacle);

            string key = response.Result.name;

            FirebaseResponse getResponse = await m_firebaseClient.GetAsync($"mower/obstacles/{key}");
            return getResponse.ResultAs<Obstacle>();
        }
    }
}