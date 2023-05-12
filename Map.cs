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

           for(int i = 0 ; i < Game1.nr_staring_keys ; i++){
                int X = rd.Next(1,Game1._Map.MapList.GetLength(0)-2);
                int Y = rd.Next(1,Game1._Map.MapList.GetLength(1)-2);
                if(Game1._Map.MapList[X,Y].Type == 0){
                    Game1.Keys_on_map.Add(new Item(Game1._Item_Types[0],new Vector2(X+0.5f,Y+0.5f)));
                    _Entities.Add(Game1.Keys_on_map[Game1.Keys_on_map.Count-1]._Entety);
                }
                else{
                    i--;
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
            //_Entities.Add(new Entitiy(Game1.E_types[1],new Vector2(1f,1f)));
            
            
            for(int iW = 0; iW < W ; iW++){
                
                
                for(int iH = 0; iH < H ; iH++){
                    
                    

                    
                    if(iW > 0 && iW < MapList.GetLength(0)-1 && iH > 0 && iH < MapList.GetLength(1)-1){
                        MapList[iW,iH] = new Cell(MAT[0], 0);
                    }
                        
                    else
                        {MapList[iW,iH] = new Cell(MAT[1], 1);}

                    
                }
                MapList[iW,3] = new Cell(MAT[1], 1);
            }  
            
            MapList[2,3] = new Cell(MAT[0], 0);
            MapList[4,6] = new Cell(MAT[1], 0);
           


        }

       
        

        public void DRAW(Player P){
            int _CellSize = Game1.CellSize;
            if(_CellSize > 0){
                Rectangle R = new Rectangle(0 , 0 , _CellSize * W , Game1.WindowHigth);
                Game1._spriteBatch.Draw(Game1.pixel,R,Game1.UI_color1);

                R = new Rectangle(0 , Game1.ScreenHight , Game1.ScreenWidth + W*_CellSize, Game1.WindowHigth - Game1.ScreenHight);
                Game1._spriteBatch.Draw(Game1.pixel,R,Game1.UI_color2);
                for(int iW = 0; iW < W ; iW++){
                    for(int iH = 0; iH < H ; iH++){
                        R = new Rectangle(iW*_CellSize,iH*_CellSize+1,_CellSize,_CellSize);
                        
                        Color C =  MapList[iW,iH].FlorOrWall;
                        
                        
                        Game1._spriteBatch.Draw(Game1.pixel,R,C);

                        
                    }
                }
                P.DRAW();
                foreach(Entitiy E in _Entities){
                    DrawEntity(E);
                }
            }
        }

        private void DrawEntity(Entitiy E){

            Vector2 P = E.MapP * Game1.CellSize;
            int S = (int)(Game1.CellSize * 0.2);
            Rectangle R = new Rectangle((int)(P.X-S*0.5),(int)(P.Y-S*0.5),S,S);
            Game1._spriteBatch.Draw(Game1.pixel,R,E.Type._Col);
        }

    }
}
