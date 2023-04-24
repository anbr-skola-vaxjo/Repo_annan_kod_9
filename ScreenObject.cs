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
            Texture2D T = Tex;
            if(Tex == null){
                T = Game1.pixel;
                
            }
            
            Object_Box.Y += (int)(Object_Box.Height*0.5*Game1.Height_offset);
                
            Color C2 = _color*f;
            _color = C2;            
            _color.A = 255;
            
            Game1._spriteBatch.Draw(T, Object_Box, _color);
        }
        
    }
}