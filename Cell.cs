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
    public class Cell
    {
        readonly public Material _MAT;
        
        public Color FlorOrWall = Color.Magenta;
        public Color Roof = Color.Magenta;
        public int Type = 0;
        private Random rd = new Random();
        

        public Cell(Material M, int T){
            _MAT = M;
            Type = T;
            Color F = _MAT.FlorOrWall;
            Color R = _MAT.Roof;
            float V = _MAT.Variation;

            int Ra_c = rd.Next(-3,4);
            int Ra_r = (int)((float)(Ra_c+rd.Next(-1,2))*V);
            int Ra_g = (int)((float)(Ra_c+rd.Next(-2,1))*V);
            int Ra_b = (int)((float)(Ra_c+rd.Next(0,3))*V);

           
            FlorOrWall = new Color(F.R + Ra_r  , F.G + Ra_g, F.B + Ra_b);

            Ra_c = rd.Next(-2,3);
            

            Roof =new Color(R.R , R.G , R.B);
            
            
        }
        
    }
}