
using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{
    public class PositionModel{

        private IFirebaseClient m_firebaseClient;

        public PositionModel() {
            FirebaseModel firebaseModel = new FirebaseModel();
            m_firebaseClient = firebaseModel.m_firebaseClient;
        }
        public async Task<List<Position>?> getAllPositions(){
            FirebaseResponse response = await m_firebaseClient.GetAsync("mower/positions");
            string postionsJson = JsonConvert.SerializeObject(response.ResultAs<Dictionary<String, Position>>().Values);
            List<Position>? positionsList = JsonConvert.DeserializeObject<List<Position>>(postionsJson);
            Console.WriteLine(positionsList);
            return positionsList;
        }
        public async Task<Position> pushPosition(Position position){

            PushResponse response = await m_firebaseClient.PushAsync("mower/positions/", position);
            Console.WriteLine("Pushed to firebase");
            Console.WriteLine(response.Body);

            return response.ResultAs<Position>();
        }
        
        public async Task<Position> getPosition(int id){
            FirebaseResponse response = await m_firebaseClient.GetAsync("mower/positions" + id);
            return response.ResultAs<Position>();
        }
    }

}