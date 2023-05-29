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
    public class Kompas
    {
        private Texture2D Tex;
        private Rectangle Box;
        private Player _player;
        private double angle_target = 0;
        private double angle = 180;
        private double R_speed = 15;
        public Color Line_color = new Color(255,0,0);
        public Kompas(Texture2D T, int w, Player p){
            Tex = T;
            Vector2 P = new Vector2((int)(Game1.ScreenWidth+Game1.MapWidth*Game1.CellSize-w*0.8), (int)(Game1.ScreenHight - w*0.8));
            Box = new Rectangle((int)(P.X-w*0.5),(int)(P.Y-w*0.5),w,w);
            _player = p;
        }

        public void DRAW(){

            Game1._spriteBatch.Draw(Tex, Box, new Color(200,200,200));
            if(_player.Keys_found < Game1.needed_keys){
                int S = (int)(Box.Width*0.1);
                
                Rectangle R = new Rectangle((int)(Box.Center.X-S*0.5),(int)(Box.Center.Y-S*0.5),S,S);

                

                double angle2 = _player.info_FOVangle;
                
                double V = _player.V;
                double v = _player.v;
                float info_ofsett = ((float)angle2/180f*(float)Math.PI);
                Vector2 Forward = new Vector2( (float)Math.Cos(V + info_ofsett),(float)Math.Sin(V + info_ofsett));
                Vector2 Forward2 = new Vector2( (float)Math.Cos(V + 90),(float)Math.Sin(V + 90));
                
                Forward.X = (float)Math.Round(Forward.X*1000)*0.001f;
                Forward.Y = (float)Math.Round(Forward.Y*1000)*0.001f;
                
                Vector2 Ent_comp_palyer = Game1.Closest_key.MapP;
                

                
                
                
                
                
                angle_target =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward+_player.MapP);
                
                

                if(Vector2.Dot(Ent_comp_palyer-_player.MapP,Forward2) < 0){
                    angle_target = 360 - angle_target;
                }

                if(double.IsNaN(angle_target)){
                    angle_target = 180;
                }

                double curent_R_speed = 0;
                double curent_R_speed_reverse = 0;
                double curent_R_speed_reverse_2 = 0;
                
                curent_R_speed = angle_target - angle;
                curent_R_speed_reverse = (angle_target-360) - angle;
                curent_R_speed_reverse_2 = (angle_target+360) - angle;
                
                if(Math.Abs(curent_R_speed) > Math.Abs(curent_R_speed_reverse)){
                    curent_R_speed = curent_R_speed_reverse;
                }
                if(Math.Abs(curent_R_speed) > Math.Abs(curent_R_speed_reverse_2)){
                    curent_R_speed = curent_R_speed_reverse_2;
                }
                
                
                if(curent_R_speed > R_speed){
                    curent_R_speed = R_speed;
                }
                if(curent_R_speed < -R_speed){
                    curent_R_speed = -R_speed;
                }

                angle += curent_R_speed;
                if(angle > 360){
                    angle -= 360;
                }
                if(angle < 0){
                    angle += 360;
                }
    
                double v2 = (angle-90)/180*Math.PI;
                
                if(double.IsNaN(angle)){
                    angle = angle_target;
                }
                
                Vector2 Angle = new Vector2((float)Math.Cos(v2),(float)Math.Sin(v2));
                
            
                Rectangle R2 = new Rectangle((int)(R.Center.X-(Angle.X*S*3)-S/4),(int)(R.Center.Y-(Angle.Y*S*3)-S/4),S/2,S/2);

                //Vector2.Dot();

                

                Game1._screen.drawline(new Vector2(R.Center.X,R.Center.Y), new Vector2(R2.Center.X,R2.Center.Y), new Color(100,0,0));

                Game1._spriteBatch.Draw(Tex, R, Line_color);
                Game1._spriteBatch.Draw(Tex, R2, Line_color);
                

            }
            else{Line_color = new Color(0,255,0);}
            if(Game1.needed_keys > 0){
                string keys = _player.Keys_found + " / " + Game1.needed_keys;
                Game1._spriteBatch.DrawString(Game1.font,keys,new Vector2(Box.Center.X - keys.Length * 2.5f ,Box.Bottom - 5),Line_color);
            }

        }
    }
}