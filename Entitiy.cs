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

        public Entitiy(EntityTypes T, Vector2 P){
            Type = T;
            MapP = P;
        }


        // entity (position)vinkel till player vinkel 
    }
}