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
    public class UI_button
    {
        public bool Is_flip;
        
        public bool is_preesed;
        public Rectangle Hitbox;
        public Texture2D Tex;
        public Color Button_Color;
        public Color Text_Color;
        public string Text;
        public bool mouse_is_pressed = false;
        public string Extra_text;
        public object _Var;

        public UI_button(string T, Rectangle R, Texture2D I , Color BC, Color TC, bool F){
            Text = T;
            Hitbox = R;
            Tex = I;
            Button_Color = BC;
            Text_Color = TC;
            Is_flip = F;
        }

        public virtual void Draw(){
            Color C = Button_Color; 
            Rectangle H = Hitbox;
            
            String T = Text + " " + Extra_text;
            

            if(is_preesed){
                C.R = (byte)(C.R * 1.5f); 
            }
            if(H.Contains(Mouse.GetState().Position)){
                C = C * 0.5f;
                H.Width = (int)(H.Width*1.2f);
            }
            
            Game1._spriteBatch.Draw(Tex,H,C);

            Vector2 TextPos = new Vector2(H.Left+(H.Center.X - H.Left)*0.5f,(int)(H.Center.Y-Game1.font.MeasureString(T).Y*0.5f));
            Game1._spriteBatch.DrawString(Game1.font, T, TextPos, Text_Color);
        }
        
        public virtual void Run(){
            
            if(Hitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed){
                if(mouse_is_pressed == false){
                    if(Is_flip == false){
                        is_preesed = true;
                    }
                    else{
                        if(is_preesed == false){
                            is_preesed = true;
                        }
                        else{
                            is_preesed = false;
                        }
                    }
                }    
            
            }
            else if(Is_flip == false){
                is_preesed = false;
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                mouse_is_pressed = true;
            }
            else{
                mouse_is_pressed = false;
            }
            
            
        }

       

        public virtual void _run(){

        }
        
        public virtual void Reset(){
            if(Is_flip == false){
                is_preesed = false;
            }

            
        }
    }
}