using ims_group4_backend.Models;
using Newtonsoft.Json.Linq;

namespace ims_group4_backend.Models{
    public class Obstacle{
        
        public float x{get;set;}
        public float y{get;set;}
        public string? base64_image{get; set;}
        public List<ImageInformation>? infos_image{get; set;}
    }

    public struct ObstacleData {
        public Obstacle obstacle { get; set; }
        
    }
    public class ImageInformation{
        public float? confidence{get; set;}
        public string? description{get; set;}
        public string? locale{get; set;}
        public string? mid{get; set;}
        public float? score{get; set;}
        public float? topicality{get; set;}
    }
}
