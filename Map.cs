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
    public class Map
    {
        public Cell[,] MapList = new Cell[Game1.MapWidth,Game1.MapHight];
        private int W = Game1.MapWidth;
        private int H = Game1.MapHight;
        public List<Material> MAT = new List<Material>();
        public Player P = Game1.P1;
        private Random rd = new Random();

        public List<Entitiy> _Entities = new List<Entitiy>();
        
        
        public void IndoorMapCreator(){
            _Entities.Add(new Entitiy(Game1.E_types[0],new Vector2(5f,5f)));
            for(int iW = 0; iW < W ; iW++){
                
                
                for(int iH = 0; iH < H ; iH++){
                    if(iW > 0 && iW < MapList.GetLength(0)-1 && iH > 0 && iH < MapList.GetLength(1)-1)
                        {MapList[iW,iH] = new Cell(MAT[0], 0);}
                    else
                        {MapList[iW,iH] = new Cell(MAT[1], 1);}
                }
                MapList[iW,3] = new Cell(MAT[1], 1);
            }  

            MapList[2,3] = new Cell(MAT[0], 0);


        }

        public void OutdoorMapCreator(){
            double[,] perlinmap = perlinmaker(60,3,4,200,-10,10,-1);
            double[,] perlinmap2 = perlinmaker(15,3,6,30,20,4,0);
            double[,] perlinmap3 = perlinmaker(20,3,2,100,30,10,0);
            double[,] perlinmap4 = perlinmaker(20,3,2,100,30,10,0);
            double bottom = 1;
            double bottom2 = 0.5;

            for(int iW = 0; iW < W ; iW++){
                for(int iH = 0; iH < H ; iH++){
                    if(perlinmap2[iW,iH] < bottom2 ){
                        perlinmap[iW,iH] = perlinmap2[iW,iH];
                    }
                    if(perlinmap3[iW,iH] > bottom2 ){
                        perlinmap[iW,iH] = perlinmap3[iW,iH];
                    }
                    if(perlinmap4[iW,iH] > bottom2 ){
                        perlinmap[iW,iH] = perlinmap4[iW,iH];
                    }
                    
                }
            }
            

            for(int iW = 0; iW < W ; iW++){
                for(int iH = 0; iH < H ; iH++){
                    
                    int h =(int)(((perlinmap[iW,iH]*perlinmap[iW,iH]*0.5)+50));
                    if(h>100){
                        h=100;
                    }
                    if(iW > 0 && iW < MapList.GetLength(0)-1 && iH > 0 && iH < MapList.GetLength(1)-1)
                        {MapList[iW,iH] = new Cell(MAT[0], 0);}
                    else 
                        {MapList[iW,iH] = new Cell(new Material(new Color(h,h,h),Color.White,1f,2), 1);}
                    if(perlinmap[iW,iH] > bottom){
                        
                        MapList[iW,iH] = new Cell(new Material(new Color(h,h,h),Color.White,1f,2), 1);
                    }
                    
                }
            }   
            
            for(int i = 0 ; i < 10 ; i++){
                int X = rd.Next(1,MapList.GetLength(0)-2);
                int Y = rd.Next(1,MapList.GetLength(1)-2);
                if(MapList[X,Y].Type == 0){
                    MapList[X,Y] = new Cell(MAT[2], 1);
                    break;
                }
            }
            
                     
        }
        

        public void DRAW(Player P){
            for(int iW = 0; iW < W ; iW++){
                for(int iH = 0; iH < H ; iH++){
                    Rectangle R = new Rectangle(iW*Game1.CellSize+1,iH*Game1.CellSize+1,Game1.CellSize-2,Game1.CellSize-2);
                    
                    Color C =  MapList[iW,iH].COLOR;
                    
                    
                    Game1._spriteBatch.Draw(Game1.pixel,R,C);

                    if(MapList[iW,iH]._MAT == MAT[2]){
                        Game1._spriteBatch.Draw(Game1.pixel,R, MAT[0].FlorOrWall);
                    }
                }
            }
            P.DRAW();
            foreach(Entitiy E in _Entities){
                DrawEntity(E);
            }
        }

        private void DrawEntity(Entitiy E){

            Vector2 P = E.MapP * Game1.CellSize;
            int S = (int)(Game1.CellSize * 0.2);
            Rectangle R = new Rectangle((int)(P.X-S*0.5),(int)(P.Y-S*0.5),S,S);
            Game1._spriteBatch.Draw(Game1.pixel,R,E.Type._Col);
        }

    
        
        private double[,] perlinmaker (int NrKernals,int KernalRadius,int BlendNR,double HeightMax, double HeightMin,double WallHeight, double FloorHeight){

            double[,] perlinmap = new double[W,H];
            double[,] SAVEperlinmap = new double[W,H];
            for(int iW = 0; iW < W ; iW++){
                for(int iH = 0; iH < H ; iH++){
                    if(iW > 0 && iW < MapList.GetLength(0)-2 && iH > 0 && iH < MapList.GetLength(1)-2)
                        {perlinmap[iW,iH]=FloorHeight;}
                    else 
                        {perlinmap[iW,iH]=WallHeight;}
                    
                }
            }

            for(int i = 0 ; i < NrKernals ; i++){
                int x = rd.Next(0,W);
                int y = rd.Next(0,H);
                perlinmap[x,y] = rd.Next((int)HeightMin,(int)HeightMax+1);
            }
            for(int j = 0 ; j < BlendNR ; j++){
                for(int iW = 0; iW < W ; iW++){
                    for(int iH = 0; iH < H ; iH++){
                        double HC = 0;
                        double counter = 0;
                        for(int Kx = -KernalRadius; Kx < KernalRadius ; Kx++){
                            for(int Ky = -KernalRadius; Ky< KernalRadius ; Ky++){
                                if(Kx + iW  >= 0 && Kx + iW < W && Ky + iH >= 0 && Ky + iH < H ){
                                    HC += perlinmap[iW+Kx,iH+Ky];
                                    counter++;
                                }
                            }
                        }
                        SAVEperlinmap[iW,iH] = HC / counter;
                    }
                }
                perlinmap = SAVEperlinmap;
                SAVEperlinmap = new double[W,H];
            }

            return perlinmap;
        } 
    }
}
