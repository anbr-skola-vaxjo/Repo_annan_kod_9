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
    public class Item
    {
        public Item_Types Type;
        public Entitiy _Entety;
        public bool on_floor;
        public Vector2 MapP;

        public Item(Item_Types I, Vector2 P){
            Type = I;
            MapP = P;
            if(MapP != null){
                on_floor = true;
                _Entety = new Entitiy(Type.E_type,MapP);
            }
            else{
                on_floor = false;
            }
        }

        public void run(){
            Vector2 P_MapP = Game1.P1.MapP;
            if(on_floor && new Vector2((int)P_MapP.X,(int)P_MapP.Y) == new Vector2((int)MapP.X,(int)MapP.Y) ){
                on_floor = false;
                MapP = new Vector2();
            }
        }
        

    }
}