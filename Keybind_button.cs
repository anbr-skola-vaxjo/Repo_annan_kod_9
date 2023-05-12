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
    public class Keybind_button : UI_button
    {
        public Keys Current_key;
        private bool Key_is_changed = false;

        public Keybind_button(string T, Rectangle R, Texture2D I , Color BC, Color TC, Keys K) : base(T, R, I, BC, TC, true){
            
            Text = T;
            Hitbox = R;
            Tex = I;
            Button_Color = BC;
            Text_Color = TC;
            Current_key = K;
            
                  
        }

        public override void Draw()
        {
            Color C = Button_Color; 
            Rectangle H = Hitbox;
            
            String T = Text + " " + Extra_text;
            

            if(is_preesed){
                C.R = (byte)(C.R * 1.5f); 
                C = C * 0.5f;
                H.Width = (int)(H.Width*1.3f);
            }
            else if(H.Contains(Mouse.GetState().Position)){
                C = C * 0.5f;
                H.Width = (int)(H.Width*1.2f);
            }
            
            Game1._spriteBatch.Draw(Tex,H,C);

            Vector2 TextPos = new Vector2(H.Left+(H.Center.X - H.Left)*0.5f,(int)(H.Center.Y-Game1.font.MeasureString(T).Y*0.5f));
            Game1._spriteBatch.DrawString(Game1.font, T, TextPos, Text_Color);
        }

        public override void Run()
        {
            base.Run();
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && Hitbox.Contains(Mouse.GetState().Position) == false){
                is_preesed = false;
            }
        }

        public override void Reset()
        {
            if(Key_is_changed){
                is_preesed = false;
                Key_is_changed = false;
            }

        }

        public override void _run(){
            if(is_preesed){
                if(Keyboard.GetState().GetPressedKeyCount() > 0){
                    Current_key = Keyboard.GetState().GetPressedKeys()[0];
                    Key_is_changed = true;
                }
            }
            Extra_text = Current_key.ToString();
            _Var = Current_key;
        }
        
    }
}