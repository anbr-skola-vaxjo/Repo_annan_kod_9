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
        public List<Simple_texture_layer> texture_Layers = new List<Simple_texture_layer>();
        


        public ScreenObject(float d, float f, Rectangle r, Color c, List<Simple_texture_layer> TL){
            distance = d;
            Object_Box = r;
            _color = c;
            texture_Layers = TL;
            FishEye = f;
        }

        public void Draw(float f){
            
            Object_Box.Y += (int)(Object_Box.Height * 0.5 * Game1.Height_offset);
            Color C = _color;
            if(texture_Layers.Count > 0){
                foreach(Simple_texture_layer TL in texture_Layers){
                    Texture2D T = TL._tex;
                    

                    
                    if(TL.Glow == false){
                        C *= f;
                    } 
                    else{
                        C = Color.White;
                    }
                    C.A = 255;
                    
                    Game1._spriteBatch.Draw(T, Object_Box, C);
                    
                }
            }
            else{
                Game1._spriteBatch.Draw(Game1.pixel, Object_Box, C);
            }
        }
        
    }
}