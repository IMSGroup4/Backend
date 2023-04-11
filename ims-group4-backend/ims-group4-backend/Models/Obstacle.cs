using ims_group4_backend.Models;

namespace ims_group4_backend.Models{
    public class Obstacle{
        
        public string? m_time_stamp{get; set;}
        public float m_x_position{get;set;}
        public float m_y_position{get;set;}
        public string? base64_image{get; set;}
    }
}