using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{
    public class FirebaseModel{
        
        private IFirebaseClient m_client;
        public FirebaseModel(){
            IFirebaseConfig config = new FirebaseConfig{
                AuthSecret = "okN3hNACyJHsu54JQjvAMj1Lf25XxRPIN5l4fi2q",
                BasePath = "https://ims-group4-980a7-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            m_client = new FirebaseClient(config);
        }

        public async Task<Position> getPosition(int id){
            FirebaseResponse response = await m_client.GetAsync("mower/positions" + id);
            // Position pos = response.ResultAs<Position>();
            return response.ResultAs<Position>();
        }

        public async Task<List<Position>?> getAllPositions(){
            FirebaseResponse response = await m_client.GetAsync("mower/positions");
            string postionsJson = JsonConvert.SerializeObject(response.ResultAs<Dictionary<String, Position>>().Values);
            List<Position>? positionsList = JsonConvert.DeserializeObject<List<Position>>(postionsJson);
            Console.WriteLine(positionsList);
            return positionsList;
        }

        // Out of date
        /*
        public async Task<Position> setPosition(Position position, int id){ 
            
            SetResponse response = await m_client.SetAsync("mower/positions/", position);
            return response.ResultAs<Position>();
        }
        */
        public async Task<Position> pushPosition(Position position){

            PushResponse response = await m_client.PushAsync("mower/positions/", position);
            Console.WriteLine("Pushed to firebase");
            Console.WriteLine(response.Body);

            return response.ResultAs<Position>();
        }

        public async Task<Obstacle> getObstacle(int id){

            FirebaseResponse response = await m_client.GetAsync("mower/obstacles/" + id);
            Obstacle pos = response.ResultAs<Obstacle>();
            return response.ResultAs<Obstacle>();
            
        }

        public async Task<List<Obstacle>> getAllObstacles(){
            FirebaseResponse response = await m_client.GetAsync("mower/obstacles/");
            var obstaclesDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Obstacle>>(response.Body);
            Console.WriteLine(response.Body);
            
            return obstaclesDict.Values.ToList();
        }

        public async Task<Obstacle> setObstacle(Obstacle obstacle) {
            Console.WriteLine("New Obstacle");
            Console.WriteLine(obstacle);

            PushResponse response = await m_client.PushAsync("mower/obstacles/", obstacle);

            string key = response.Result.name;

            FirebaseResponse getResponse = await m_client.GetAsync($"mower/obstacles/{key}");
            return getResponse.ResultAs<Obstacle>();
        }
    }
}