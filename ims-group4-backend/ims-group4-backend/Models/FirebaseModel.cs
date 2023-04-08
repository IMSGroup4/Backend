using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;


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
            FirebaseResponse response = await m_client.GetAsync("positions/" + id);
            // Position pos = response.ResultAs<Position>();
            return response.ResultAs<Position>();
        }

        public async Task<List<Position>> getAllPositions(){
            FirebaseResponse response = await m_client.GetAsync("positions");
            Console.WriteLine(response.Body);
            return response.ResultAs<List<Position>>();
        }

        public async Task<Position> setPosition(Position position, int id){
            Console.WriteLine("New Position");
            Console.WriteLine(position);

            SetResponse response = await m_client.SetAsync("positions/"+id, position);

            Console.WriteLine(response.Body);

            return response.ResultAs<Position>();
        }
    }
}