using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{
    public class SurroundingModel{

        private IFirebaseClient m_firebaseClient;

        public SurroundingModel() {
            FirebaseModel firebaseModel = new FirebaseModel();
            m_firebaseClient = firebaseModel.m_firebaseClient;
        }
        public async Task<List<Position>?> getSurroundings(){
            FirebaseResponse response = await m_firebaseClient.GetAsync("mower/surroundings");
            string postionsJson = JsonConvert.SerializeObject(response.ResultAs<Dictionary<String, Position>>().Values);
            List<Position>? positionsList = JsonConvert.DeserializeObject<List<Position>>(postionsJson);
            Console.WriteLine(positionsList);
            return positionsList;
        }
        public async Task<Position> pushSurrounding(Position position){

            PushResponse response = await m_firebaseClient.PushAsync("mower/surroundings", position);
            Console.WriteLine("Pushed to firebase");
            Console.WriteLine(response.Body);

            return response.ResultAs<Position>();
        }
    }

}