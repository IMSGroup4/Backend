using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;

namespace ims_group4_backend.Models{
    public class FirebaseModel{
        
        private IFirebaseClient m_client;
        public FirebaseModel(){
            IFirebaseConfig config = new FirebaseConfig{
                AuthSecret = "FH4Hwrrzm9iK6rBpXnPYC2GRdpAx0XIbSYZr2spN",
                BasePath = "https://ims-group4-980a7-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            m_client = new FirebaseClient(config);
        }


        async public Task<object> getPosition(){
            FirebaseResponse response = await m_client.GetAsync("position/position1");
            return response.ResultAs<object>();
        }
    }
}