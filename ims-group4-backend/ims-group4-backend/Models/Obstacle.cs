using ims_group4_backend.Models;

namespace ims_group4_backend.Models{
    public class Obstacle{
        
        public string? timestamp{get; set;}
        public float x{get;set;}
        public float y{get;set;}
        public string? base64_image{get; set;}
    }
}