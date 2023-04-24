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
    public class Maze_Map
    {
        public int Width;
        public int Height;
        public int cellsize;
        public Maze_Perlin_noize P_map;
        public Maze_Cell[,] _cell;

        public Maze_Map(int W, int H, int C){
            Width = W;
            Height = H;
            cellsize = C;
            P_map = Game1._Perlin;
            _cell = new Maze_Cell[W,H];
            for(int i = 0; i < W; i++){
                for(int j = 0; j < H; j++){

                    _cell[i,j] = new Maze_Cell();
                }
            }

        }
        public void run(){

           
            
            Noize_to_map();
            foreach(Maze_Cell c in _cell){
                c.run();
            }

            
        }
        

        

        private void Noize_to_map(){
            for(int i = 0; i < Width; i++){
                for(int j = 0; j < Height; j++){
                    _cell[i,j]._down = true;
                    _cell[i,j]._upp = true;
                    _cell[i,j]._left = true;
                    _cell[i,j]._right = true;
                }
            }

            for(int i = 0; i < Width; i++){
                for(int j = 0; j < Height; j++){
                    
                    if(i-1 >= 0 && i+1 < P_map.NoizeMap.GetLength(0) && j-1 >= 0 && j+1 < P_map.NoizeMap.GetLength(1) ){
                        if(P_map.NoizeMap[i-1,j] < P_map.NoizeMap[i,j+1]){
                            _cell[i,j]._left = true;
                            _cell[i,j]._down = false;
                        }
                        else{
                            _cell[i,j]._left = false;
                            _cell[i,j]._down = true;
                        }

                        if(P_map.NoizeMap[i+1,j] > P_map.NoizeMap[i,j-1]){
                            _cell[i,j]._right = true;
                            _cell[i,j]._upp = false;
                        }
                        else{
                            _cell[i,j]._right = true;
                            _cell[i,j]._upp = true;
                        }

                        

                        if( Math.Round(P_map.NoizeMap[i,j]) > 100 && Math.Round(P_map.NoizeMap[i,j]) < 150){
                            _cell[i,j]._down = true;
                            _cell[i,j]._upp = true;
                        }
                        if( Math.Round(P_map.NoizeMap[i,j]) < 100){
                            _cell[i,j]._left = true;
                            _cell[i,j]._right = true;
                        }
                        
                        if(_cell[i+1,j]._left == false){
                            _cell[i,j]._right = false;
                        }
                        if(_cell[i,j+1]._upp == false){
                            _cell[i,j]._down = true;
                        }
                        
                        if(_cell[i-1,j]._right == false){
                            _cell[i,j]._left = true;
                        }
                        if(_cell[i,j-1]._down == false){
                            _cell[i,j]._upp = false;
                        }
                        
                        if(_cell[i,j]._right == false && _cell[i,j]._left == false && _cell[i,j]._upp == false && _cell[i,j]._down == false){
                            _cell[i,j]._upp = true;
                            _cell[i,j]._down = true;
                        }
                        
                        _cell[0,j]._left = false;
                        _cell[Width-1,j]._right = false;
                        _cell[i,0]._down = false;
                        _cell[i,Height-1]._upp = false;
                    }
                }
            }

        }
        
    }
}