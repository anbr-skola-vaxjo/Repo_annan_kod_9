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
        private Maze_Map _Maze;

        public List<Entitiy> _Entities = new List<Entitiy>();
        
        public void Maze_Creator(){
            
            _Maze = new Maze_Map(W,H,Game1.CellSize);
            _Maze.run();
            for(int iW = 0; iW < W ; iW++){
                
                
                for(int iH = 0; iH < H ; iH++){
                    
                    

                    
                    if(iW > 2 && iW < MapList.GetLength(0)-3 && iH > 2 && iH < MapList.GetLength(1)-3)
                        {MapList[iW,iH] = new Cell(MAT[0], 0);}
                    else
                        {MapList[iW,iH] = new Cell(MAT[1], 1);}

                    
                }
                
            }  
            

            for(int i = 0; i < _Maze.Width; i++){
                for(int j = 0; j < _Maze.Height; j++){
                    for(int i1 = 0; i1 < 3; i1++){
                        for(int j1 = 0; j1 < 3; j1++){
                            if(_Maze._cell[i,j].Wall[i1,j1]>0){
                                if(i*2+i1 >= 0 && i*2+i1 < MapList.GetLength(0) && j*2+j1 >= 0 && j*2+j1 < MapList.GetLength(1)){
                                    MapList[i*2+i1,j*2+j1] = new Cell(MAT[1],1);
                                }
                                 
                            }
                        }
                    }                    
                }
            }

            for(int i = 0 ; i < 120 ; i++){
                int _x = rd.Next(3,MapList.GetLength(0) - 3);
                int _y = rd.Next(3,MapList.GetLength(1) - 3);
                int _X = rd.Next(0,2)*2-1;
                int _Y = rd.Next(0,2)*2-1;
                if(MapList[_x,_y].Type != 0){
                    MapList[_x,_y] = new Cell(MAT[0], 0);
                    MapList[_x+_X,_y+_Y] = new Cell(MAT[1], 1);
                }
                else{
                    i--;
                }
            }
            
            
            AddRoom_Rectangle(10,10,1,1,2);
            AddRoom_Rectangle(1,1,30,30,2);

            for(int i = 0; i < 2 ; i++){
                AddRoom_Square(10,3,1);
                AddRoom_Square(10,3,2);
                AddRoom_Square(10,3,3);
                AddRoom_Square(10,3,4);
            }

            Add_Walled_Room_Rectangle(20,5,10,5,1);
            Add_Walled_Room_Rectangle(10,5,10,3,3);

            Add_Walled_Room_Rectangle(10,5,10,5,1);
            Add_Walled_Room_Rectangle(10,5,10,5,4);
            

            for(int iW = 3; iW < W-3 ; iW++){
                for(int iH = 3; iH < H -3 ; iH++){
                    if(MapList[iW,iH].Type == 0){
                        if(MapList[iW-1,iH-1].Type != 0 && MapList[iW,iH-1].Type != 0 && MapList[iW+1,iH-1].Type != 0){
                            if(MapList[iW-1,iH].Type != 0 && MapList[iW+1,iH].Type != 0){
                                if(MapList[iW-1,iH+1].Type != 0 && MapList[iW,iH+1].Type != 0 && MapList[iW+1,iH+1].Type != 0){
                                    MapList[iW,iH] = new Cell(MAT[0],1);
                                }
                            }
                        }
                    }
                } 
            }

        }

        private void AddRoom_Rectangle(int Max_Width, int Min_Width, int Max_Height, int Min_Height, int Segment){
            int S = Segment;
            
            int Width = rd.Next(Min_Width,Max_Width);
            int Height = rd.Next(Min_Height,Max_Height);


            int X_Max = MapList.GetLength(0) - 3 - Width;
            int Y_Max = MapList.GetLength(1) - 3 - Height;
            int X_Min = 3;
            int Y_Min = 3;

            int Half_Width = (int)(MapList.GetLength(0)*0.5);
            int Half_Hight = (int)(MapList.GetLength(1)*0.5);

            if(S == 1){
                X_Max = Half_Width;
                Y_Max = Half_Hight;
            }
            if(S == 2){
                Y_Max = Half_Hight;

                X_Min = Half_Width;
            }
            if(S == 3){
                X_Max = Half_Width;

                Y_Min = Half_Hight;
            }
            if(S == 4){
                X_Min = Half_Width;
                Y_Min = Half_Hight;
            }


            int X = rd.Next(X_Min, X_Max);
            int Y = rd.Next(Y_Min, Y_Max);

            for(int i = 0; i < Width; i++){
                for(int j = 0; j < Height; j++){
                    MapList[i+X,j+Y] = new Cell(MAT[0],0);
                }
            }
            
        }
        private void AddRoom_Square(int Max_Width, int Min_Width, int Segment){
            int S = Segment;
            int Width = rd.Next(Min_Width,Max_Width);

            int X_Max = MapList.GetLength(0) - 3 - Width;
            int Y_Max = MapList.GetLength(1) - 3 - Width;
            int X_Min = 3;
            int Y_Min = 3;

            int Half_Width = (int)(MapList.GetLength(0)*0.5);
            int Half_Hight = (int)(MapList.GetLength(1)*0.5);

            if(S == 1){
                X_Max = Half_Width;
                Y_Max = Half_Hight;
            }
            if(S == 2){
                Y_Max = Half_Hight;

                X_Min = Half_Width;
            }
            if(S == 3){
                X_Max = Half_Width;

                Y_Min = Half_Hight;
            }
            if(S == 4){
                X_Min = Half_Width;
                Y_Min = Half_Hight;
            }


            int X = rd.Next(X_Min, X_Max);
            int Y = rd.Next(Y_Min, Y_Max);

            for(int i = 0; i < Width; i++){
                for(int j = 0; j < Width; j++){
                    MapList[i+X,j+Y] = new Cell(MAT[0],0);
                }
            }
            
        }

        private void Add_Walled_Room_Rectangle(int Max_Width, int Min_Width, int Max_Height, int Min_Height, int Segment){
            int S = Segment;
            
            int Width = rd.Next(Min_Width,Max_Width);
            int Height = rd.Next(Min_Height,Max_Height);


            int X_Max = MapList.GetLength(0) - 5 - Width;
            int Y_Max = MapList.GetLength(1) - 5 - Height;
            int X_Min = 5;
            int Y_Min = 5;

            int Half_Width = (int)(MapList.GetLength(0)*0.5);
            int Half_Hight = (int)(MapList.GetLength(1)*0.5);

            if(S == 1){
                X_Max = Half_Width;
                Y_Max = Half_Hight;
            }
            if(S == 2){
                Y_Max = Half_Hight;

                X_Min = Half_Width;
            }
            if(S == 3){
                X_Max = Half_Width;

                Y_Min = Half_Hight;
            }
            if(S == 4){
                X_Min = Half_Width;
                Y_Min = Half_Hight;
            }


            int X = rd.Next(X_Min, X_Max);
            int Y = rd.Next(Y_Min, Y_Max);

            for(int i = -1; i <= Width+1; i++){
                for(int j = -1; j <= Height+1; j++){
                    MapList[i+X,j+Y] = new Cell(MAT[0],0);

                    if(j >= 0 && j <= Height && i >= 0 && i <= Width){
                        MapList[X,j+Y] = new Cell(MAT[1],1);
                        MapList[X+Width,j+Y] = new Cell(MAT[1],1);
                        MapList[i+X,Y] = new Cell(MAT[1],1);
                        MapList[i+X,Y+Height] = new Cell(MAT[1],1);
                    }
                }
            }


            for(int i = 0 ; i < rd.Next(1,6) ; i++){
                int d4 = rd.Next(1,4);
                if(d4 == 1){
                    MapList[X+rd.Next(1,Width),Y] = new Cell(MAT[0],0);
                }

                else if(d4 == 2){
                    MapList[X+rd.Next(1,Width),Y+Height] = new Cell(MAT[0],0);
                }

                else if(d4 == 3){
                    MapList[X,Y+rd.Next(1,Height)] = new Cell(MAT[0],0);
                }
                
                else if(d4 == 4){
                    MapList[X+Width,Y+rd.Next(1,Height)] = new Cell(MAT[0],0);
                }
            }
            
            

        }


        public void IndoorMapCreator(){
            _Entities.Add(new Entitiy(Game1.E_types[0],new Vector2(9f,9f)));
            _Entities.Add(new Entitiy(Game1.E_types[0],new Vector2(9f,10f)));
            _Entities.Add(new Entitiy(Game1.E_types[1],new Vector2(6f,2f)));
            
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
            MapList[4,6] = new Cell(MAT[1], 0);
            MapList[5,7] = new Cell(MAT[3], 0);


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
                        {MapList[iW,iH] = new Cell(new Material(new Color(h,h,h),Color.White,1f,1f,Game1.detail), 1);}
                    if(perlinmap[iW,iH] > bottom){
                        
                        MapList[iW,iH] = new Cell(new Material(new Color(h,h,h),Color.White,1f,1f,Game1.detail), 1);
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
            int _CellSize = Game1.CellSize;
            Rectangle R = new Rectangle(0 , 0 , _CellSize * W , Game1.WindowHigth);
            Game1._spriteBatch.Draw(Game1.pixel,R,Game1.UI_color1);

            R = new Rectangle(0 , Game1.ScreenHight , Game1.ScreenWidth + W*_CellSize, Game1.WindowHigth - Game1.ScreenHight);
            Game1._spriteBatch.Draw(Game1.pixel,R,Game1.UI_color2);
            for(int iW = 0; iW < W ; iW++){
                for(int iH = 0; iH < H ; iH++){
                    R = new Rectangle(iW*_CellSize,iH*_CellSize+1,_CellSize,_CellSize);
                    
                    Color C =  MapList[iW,iH].FlorOrWall;
                    
                    
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
