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
    public class Simple_texture_layer
    {
        public Texture2D _tex;
        public bool Glow;

        public Simple_texture_layer(Texture2D T, bool G){
            if(T == null){
                T = Game1.pixel;
            }
            _tex = T;
            Glow = G;

            

        }
        
    }
}