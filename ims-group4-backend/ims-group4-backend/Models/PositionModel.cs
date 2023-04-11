
using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;

namespace ims_group4_backend.Models{

    public class PositionModel{

        List<Position> positions = new List<Position>();

        public Position? get_by_id(int id){
 
            Console.WriteLine(positions.Count);
            if(positions.Count == 0){
                Console.WriteLine("No positions");
                return null;
            }
            else{
                return positions[id];
            }
        }

        public List<Position> get_all_position(){ 
            return positions;
        }

        // public int add_position(Position position){
        //     try {
        //         Position new_position = new Position {
        //             m_x_position = position.m_x_position,
        //             m_y_position = position.m_y_position,
        //             m_time_stamp = position.m_time_stamp
        //         };

        //         positions.Add(new_position);
        //         positions.Add(new_position);

        //         foreach(var pos in positions){
        //             Console.WriteLine(pos);
        //         }

        //         return (positions.Count == 1) ? 0 : positions.Count-1;

        //     }catch{
        //         Console.WriteLine("Could not add position to list");
        //         return -1;
        //     }
        // }
    }

}