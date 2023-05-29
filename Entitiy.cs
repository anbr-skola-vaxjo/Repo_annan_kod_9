using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo_annan_kod_9;

namespace Repo_annan_kod_9
{
    public class Entitiy
    {
        public Vector2 MapP;
        public Vector2 ScreenP;
        public EntityTypes Type;
        public List<Vector2> Movement_que = new List<Vector2>();

       


        public Entitiy(EntityTypes T, Vector2 P){
            Type = T;
            MapP = P;
        }

        private void PF_follow(){
            Vector2 P_MapP = Game1.P1.MapP;
            if(Movement_que.Contains(new Vector2((int)(P_MapP.X),(int)(P_MapP.Y))) == false){
                Movement_que.Add(new Vector2((int)(P_MapP.X),(int)(P_MapP.Y)));
                Game1.Info_text[2] =  "Distance: " + Movement_que.Count();
            }
            
            Go_to(new Vector2(Movement_que[0].X+0.5f,Movement_que[0].Y+0.5f));

            if(new Vector2((int)(MapP.X),(int)(MapP.Y)) == Movement_que[0]){
                Movement_que.RemoveAt(0);
                Game1.Info_text[2] =  "Distance: " + Movement_que.Count();
            }

            if(Vector2.Distance(P_MapP,MapP) < Game1.P1.RenderDistance){
                Vector2 Counter = new Vector2((int)MapP.X,(int)MapP.Y);
                Vector2 TO = new Vector2((int)P_MapP.X,(int)P_MapP.Y);
                List<Vector2> temp_que = new List<Vector2>();
                while(true){    

                    Vector2 CC = new Vector2(0,0);          
                    if(Counter.X > TO.X ){
                        CC.X -= 1;
                    }
                    else if(Counter.X < TO.X ){
                        CC.X += 1;                        
                    }

                    


                    if(Counter.Y > TO.Y ){
                        CC.Y -= 1;
                    }

                    else if(Counter.Y < TO.Y ){
                        CC.Y += 1;    
                    }  

                    if(Game1._Map.MapList[(int)(Counter.X + CC.X),(int)(Counter.Y)].Type != 0 ||  Game1._Map.MapList[(int)(Counter.X ),(int)(Counter.Y + CC.Y)].Type != 0 || Game1._Map.MapList[(int)(Counter.X + CC.X),(int)(Counter.Y + CC.Y)].Type != 0){
                        Game1.Info_text[1] =  "Serching";
                        break;
                    }
                    Counter += CC;
                    temp_que.Add(Counter);
                    if(new Vector2((int)Counter.X,(int)Counter.Y) == new Vector2((int)TO.X,(int)TO.Y)){
                        Movement_que.Clear();
                        Movement_que.AddRange(temp_que);
                        Game1.Info_text[2] =  "Distance: " + Movement_que.Count();
                        Game1.Info_text[1] =  "Hunting";
                        break;
                    }          
                }
            }

        }

        public void run(int i){
            if(i == 1){
                PF_follow();
            }

        }

        public void Go_to(Vector2 TO){
            if(MapP.X > TO.X ){
                if(MapP.X - TO.X < Type.Speed){
                    MapP.X -= MapP.X - TO.X;
                }
                else{
                    MapP.X -= Type.Speed;
                }
            }
            if(MapP.X < TO.X ){
                if(TO.X - MapP.X < Type.Speed){
                    MapP.X +=  TO.X - MapP.X;
                }
                else{
                    MapP.X += Type.Speed;
                }
            }


            if(MapP.Y > TO.Y ){
                if(MapP.Y - TO.Y < Type.Speed){
                    MapP.Y -= MapP.Y - TO.Y;
                }
                else{
                    MapP.Y -= Type.Speed;
                }
            }
            if(MapP.Y < TO.Y ){
                if(TO.Y - MapP.Y < Type.Speed){
                    MapP.Y +=  TO.Y - MapP.Y;
                }
                else{
                    MapP.Y += Type.Speed;
                }
            }
        }


        // entity (position)vinkel till player vinkel 
    }
}