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
    public class Maze_Perlin_noize
    {
        int W;
        int H;
        public double[,] NoizeMap;
        public int cellsize;

        public Maze_Perlin_noize(int w, int h, int c){
            W = w;
            H = h;
            List<double[,]> Templist = new List<double[,]>();
            Templist.Add(Noize(15,200,10,2,200));
            Templist.Add(Noize(15,300,3,3,100));
            Templist.Add(Noize(20,-100,3,2,100));
            Templist.Add(Noize(20,100,4,2,200));
            Templist.Add(Noize(50,40,2,2,200));
            
                      
            NoizeMap = Combine_array(Templist);
            
            
            cellsize = c;
        }

        private double[,] Combine_array(List<double[,]> List){
            int Ww = List[0].GetLength(0);
            int Hh = List[0].GetLength(1);
            foreach(double[,] E in List){
                if(E.GetLength(0) != Ww || E.GetLength(1) != Hh){
                    Console.WriteLine("ERROR; arrayera är ej av samma storlek");
                    return new double[0,0];
                }
            }
            double[,] _array = new double[W,H];
            for(int i = 0; i < Ww; i++){
                for(int j = 0; j < Hh; j++){
                        _array[i,j] = 0;
                }
            }

            foreach(double[,] ellement in List){
                for(int i = 0; i < Ww; i++){
                    for(int j = 0; j < Hh; j++){
                            _array[i,j]+=ellement[i,j]/List.Count;
                    }
                }
            }
            return _array;            
        }

        public double[,] Noize(int N_NR , double N_H , int K_Times , int K_W, int Lift){
            
            

            Random RD = new Random();
            double[,] Map = new double[W,H];
            double[,] TempMap;
            for(int i = 0; i < W; i++){
                for(int j = 0; j < H; j++){
                        Map[i,j]=0;
                }
            }

            for(int i = 0; i < N_NR; i++){
                Map[RD.Next(0,W),RD.Next(0,H)] = N_H;
            }
            for(int a = 0; a < K_Times; a++ ){
                TempMap = Map;
                for(int i = 0; i < W; i++){
                    for(int j = 0; j < H; j++){
                        double sum = 0;
                        int count = 0;
                        for(int i1 = -K_W; i1 <=  K_W; i1++){
                            for(int j1 = -K_W; j1 <=  K_W; j1++){
                                if(i+i1 >= 0 && i+i1 < W && j+j1 >= 0 && j+j1 < H ){
                                    sum += Map[i+i1,j+j1];
                                    count ++;
                                }
                            }
                        }
                        TempMap[i,j] = sum/(count) ;

                    }
                }
                Map = TempMap;
            }

            for(int i = 0; i < W; i++){
                for(int j = 0; j < H; j++){
                        Map[i,j]*=Lift;
                }
            }
            return Map;
        }
        
        public void Draw(){
            
            
            for(int i = 0; i < W; i++){
                for(int j = 0; j < H; j++){
                    
                    Rectangle R = new Rectangle((i)*cellsize*2,(j)*cellsize*2 + W*cellsize*2,2*cellsize,2*cellsize);
                    Color c = new Color((int)NoizeMap[i,j],(int)NoizeMap[i,j],(int)NoizeMap[i,j]); 
                    Game1._spriteBatch.Draw(Game1.pixel,R,c);
                }
            }
        }
    }
}

        
    
