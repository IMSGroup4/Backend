

using ims_group4_backend.Models;

namespace ims_group4_backend.Models{

    public class Position{
        private float m_x_position;
        private float m_y_position;
        private string? m_time_stamp;

        public Position(float x_position, float y_position, string? time_stamp){
            this.m_x_position = x_position;
            this.m_y_position = y_position;
            this.m_time_stamp = time_stamp;
        }
        
        public void set_x(int x) { m_x_position = x; }
        public void set_y(int y) { m_y_position = y; }
        public void set_time(string time) { m_time_stamp = time; }


    }
}