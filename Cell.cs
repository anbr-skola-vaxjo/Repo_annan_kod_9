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
        readonly public Material _MAT = new Material(Color.HotPink,Color.HotPink,0,Game1.pixel);
        readonly public Color COLOR = Color.Magenta;
        readonly public int Type = 0;
        public double health = 100;
        

        public Cell(Material M, int T){
            _MAT = M;
            Type = T;

            COLOR = _MAT.FlorOrWall;
            
        }
        
    }
}