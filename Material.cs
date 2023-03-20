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
        readonly public Color FlorOrWall = Color.Magenta;
        readonly public Color Roof = Color.Azure;
        readonly public float Softnes = 1.0f;
        readonly public int detail = 0;
        
        public Material(Color F, Color R, float S, int D){
            FlorOrWall = F;
            Roof = R;
            Softnes = S;
            detail = D;

        }
    }
}