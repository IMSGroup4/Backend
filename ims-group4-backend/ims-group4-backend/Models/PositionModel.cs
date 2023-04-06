
using ims_group4_backend.Models;

namespace ims_group4_backend.Models{

    public class PositionModel{

        List<Position> positions = new List<Position>() {new Position(),new Position(),new Position()};

        public PositionModel(){
            positions[0].m_x_position = 2;
            positions[0].m_y_position = 3;
            positions[0].m_time_stamp = "11:30";

            positions[1].m_x_position = 3;
            positions[1].m_y_position = 4;
            positions[1].m_time_stamp = "11:31";

            positions[2].m_x_position = 4;
            positions[2].m_y_position = 5;
            positions[2].m_time_stamp = "11:32";
        }
        public Position? get_by_id(int id){

            Console.WriteLine(positions.Count);
            if(this.positions == null){
                Console.WriteLine("position has null value");
                return null;
            }
            if(positions.Count < id+1){
                Console.WriteLine("Count lower than id number");
                return null;
            }
            else{
                return positions[id];
            }
        }

        public List<Position> get_all_position(){ 
            return positions;
        }



    }

}