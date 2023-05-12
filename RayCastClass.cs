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
    public class RayCastClass
    {
        private Vector2 A;
        private Vector2 B;
        private Color C;
        public RayCastClass(Vector2 Aa, Vector2 Bb, Color Cc){
            A = Aa;
            B = Bb;
            C = Cc;
        }

        public void drawline()
        {

            Vector2 p = A - B;
            if(A.X >= B.X)
            {
                p = new Vector2(A.X-B.X,p.Y);
            }
            else
            {
                p = new Vector2(B.X-A.X,p.Y);
            }

            if(A.Y >= B.Y)
            {
                p = new Vector2(p.X,A.Y-B.Y);
            }
            else
            {
                p = new Vector2(p.X,B.Y-A.Y);
            }
            
            Vector2 pen = B;
            int max;
            int min;
            
            if(p.X >= p.Y)
            {
                max = (int)Math.Round(p.X); 
                min = (int)Math.Round(p.Y); 
            }
            else
            {
                max = (int)Math.Round(p.Y); 
                min = (int)Math.Round(p.X); 
            }
            float p1 = p.X/max;
            float p2 = p.Y/max;
            if(p1 < 0 || p1 > Game1.MapWidth * Game1.CellSize || p2 < 0 || p2 > Game1.MapHight * Game1.CellSize ){
                return;
            }
            if(A.X < B.X)
            {
                p1*=-1;
            }
            if(A.Y < B.Y)
            {
                p2*=-1;
            }
        
            for(int i = 0 ; i < max ; i++)
            {
                pen+=new Vector2(p1,p2);   
                Game1._spriteBatch.Draw(Game1.pixel,pen,C);
            }

        }
    }
        
}
        
    
