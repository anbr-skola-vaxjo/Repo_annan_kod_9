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
    public class Maze_Cell
    {
         public int x;
        public int y;

        public bool _upp = true;
        public bool _down = true;
        public bool _left = true;
        public bool _right = true;
        public Color _color = Color.Lavender;

        public int[,] Wall = new int[3,3]{ {1,1,1},
                                            {1,0,1},
                                            {1,1,1}};
        
        public void run(){
            if(_upp == true){
                Wall[1,0]=0;
            }
            else{
                Wall[1,0]=1;
            }

            if(_down == true){
                Wall[1,2]=0;
            }
            else{
                Wall[1,2]=1;
            }

            if(_left == true){
                Wall[2,1]=0;
            }
            else{
                Wall[2,1]=1;
            }

            if(_right == true){
                Wall[0,1]=0;
            }
            else{
                Wall[0,1]=1;
            }
        }
        
    }
}