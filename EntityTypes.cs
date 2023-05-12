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
    public class EntityTypes
    {
        public Color _Col;
        public Vector2 size;
        public List<Simple_texture_layer> texture_Layers = new List<Simple_texture_layer>();
        public float Speed = 0.02f;



        public EntityTypes(Color C, List<Simple_texture_layer> TL, Vector2 S){
            _Col = C;
            size = S;
            texture_Layers = TL;
        }

    }
}