
using ims_group4_backend.Models;

namespace ims_group4_backend.Models{

    public class PositionModel{

        List<Position> positions = new List<Position>();

        public PositionModel(){
            positions.Add(new Position(1,2,"11:30"));
            positions.Add(new Position(2,3,"11:31"));
            positions.Add(new Position(3,4,"11:32"));
        }
        
        public Position? get_by_id(int id){
            if(this.positions == null){
                return null;
            }
            if(positions.Count > id+1){
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