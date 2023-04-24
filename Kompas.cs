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
        public Kompas(Texture2D T, int w, Player p){
            Tex = T;
            Vector2 P = new Vector2((int)(Game1.ScreenWidth+Game1.MapWidth*Game1.CellSize-w*0.8), (int)(Game1.ScreenHight - w*0.8));
            Box = new Rectangle((int)(P.X-w*0.5),(int)(P.Y-w*0.5),w,w);
            _player = p;
        }

        public void DRAW(){
            int S = (int)(Box.Width*0.1);
            
            Rectangle R = new Rectangle((int)(Box.Center.X-S*0.5),(int)(Box.Center.Y-S*0.5),S,S);
            Vector2 Angle = new Vector2((float)Math.Cos(_player.V+_player.info_FOVangle/180f*Math.PI),(float)Math.Sin(_player.V+_player.info_FOVangle/180f*Math.PI));
            Rectangle R2 = new Rectangle((int)(R.Center.X-(Angle.X*S*3)-S/4),(int)(R.Center.Y-(Angle.Y*S*3)-S/4),S/2,S/2);

            

            Game1._spriteBatch.Draw(Tex, Box, new Color(200,200,200));

            Game1._screen.drawline(new Vector2(R.Center.X,R.Center.Y), new Vector2(R2.Center.X,R2.Center.Y), new Color(100,0,0));

            Game1._spriteBatch.Draw(Tex, R, new Color(255,0,0));
            Game1._spriteBatch.Draw(Tex, R2, new Color(255,0,0));

        }
    }
}