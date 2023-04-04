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
    public class ScreenObject
    {
        public float distance;
        public float FishEye;
        public Rectangle Object_Box;
        public Color _color;
        public Texture2D Tex;

        public ScreenObject(float d, float f, Rectangle r, Color c, Texture2D t){
            distance = d;
            Object_Box = r;
            _color = c;
            Tex = t;
            FishEye = f;
        }

        public void Draw(float f){
            
            Color C2 = _color*f; C2.A = 255;
                
            _color = C2;
            Game1._spriteBatch.Draw(Tex, Object_Box, _color);
        }
        
    }
}