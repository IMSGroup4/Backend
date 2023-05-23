using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{

    public class SessionModel{

        private IFirebaseClient m_firebaseClient;
        public SessionModel() {
            FirebaseModel firebaseModel = new FirebaseModel();
            m_firebaseClient = firebaseModel.m_firebaseClient;
        }
        public async Task<bool> createSession() {
			FirebaseResponse obstacleResponse = await m_firebaseClient.DeleteAsync("mower/obstacles/");
			FirebaseResponse positionResponse = await m_firebaseClient.DeleteAsync("mower/positions/");
			FirebaseResponse surroundingResponse = await m_firebaseClient.DeleteAsync("mower/surroundings/");
			return true;
        }
    }
}