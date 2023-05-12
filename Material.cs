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
    public class Material
    {
        readonly public  Color FlorOrWall = Color.Magenta;
        readonly public Color Roof = Color.Azure;
        
        readonly public List<Material_texture_layer> _Texture_Layers = new List<Material_texture_layer>();




        readonly public float Variation = 1.0f;
        readonly public float Softnes = 1.0f;

        
        
        
        public Material(Color F, Color R, float S, float V, List<Material_texture_layer> T){
            
            FlorOrWall = F;
            Roof = R;
            Softnes = S;
            Variation = V;
            _Texture_Layers = T;

        }

        
    }
}