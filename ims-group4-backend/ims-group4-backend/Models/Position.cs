
namespace ims_group4_backend.Models{
    public class Position{

        public string? time_stamp{get; set;}
        public float x{get;set;}
        public float y{get;set;}

    }


    public struct PositionData {
        public Position position { get; set; }
        
    }


}