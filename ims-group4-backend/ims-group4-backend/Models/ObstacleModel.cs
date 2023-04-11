using ims_group4_backend.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;

namespace ims_group4_backend.Models{

    public class ObstacleModel{

        List<Obstacle> obstacles = new List<Obstacle>();

        public Obstacle? get_by_id(int id){
 
            Console.WriteLine(obstacles.Count);
            if(obstacles.Count == 0){
                Console.WriteLine("No positions");
                return null;
            }
            else{
                return obstacles[id];
            }
        }

        public List<Obstacle> get_all_obstacle(){ 
            return obstacles;
        }

        public int add_obstacle(Obstacle obstacle){
            try {
                Obstacle new_obstacle = new Obstacle {
                    m_x_position = obstacle.m_x_position,
                    m_y_position = obstacle.m_y_position,
                    m_time_stamp = obstacle.m_time_stamp,
					base64_image = obstacle.base64_image,
                };

                obstacles.Add(new_obstacle);

                foreach(var obs in obstacles){
                    Console.WriteLine(obs);
                }

                return (obstacles.Count == 1) ? 0 : obstacles.Count-1;

            }catch{
                Console.WriteLine("Could not add position to list");
                return -1;
            }
        }
    }
}