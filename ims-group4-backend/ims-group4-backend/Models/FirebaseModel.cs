using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace ims_group4_backend.Models{
    public class FirebaseModel{
        public IFirebaseClient m_firebaseClient {get;set;}
        public FirebaseModel(){
            IFirebaseConfig config = new FirebaseConfig{
                AuthSecret = "okN3hNACyJHsu54JQjvAMj1Lf25XxRPIN5l4fi2q",
                BasePath = "https://ims-group4-980a7-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            m_firebaseClient = new FirebaseClient(config);
        }
        
    }
}