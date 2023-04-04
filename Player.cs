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
    
    public class Player
    {  
        public int RenderDistance = 20;
        public double info_FOVangle;
        public Vector2 ScreenP;
        public  Vector2 MapP = new Vector2(2f,2f);
        private float speed = 0.02f;
        private int Count = 1;
        public double screenbend = 1;
        public double v = 270;
        public double spread = 1;
        public float running_speed = 2.5f;
        public int FOV = 0;
        private double vs = 1;
        public double V = 0;
        private float sp = 1.2f;
        private List<RayCastClass> RaysToCast = new List<RayCastClass>();

        private Vector2 CountRay;
        private Vector2 CountRayside;
        public Player(Cell[,] M){
            Random rd = new Random();
            for(int i = 0 ; i < 200 ; i++){
                int X = rd.Next(1,M.GetLength(0)-2);
                int Y = rd.Next(1,M.GetLength(1)-2);
                if(M[X,Y].Type == 0){
                    MapP = new Vector2(X+0.5f,Y+0.5f);
                }
            }

            CountRay = MapP;
            CountRayside = MapP;
            
        }
        public void SetRay(double S){
            FOV = (int)(Game1.ScreenWidth*0.5)-1;
            spread = S/(double)FOV;
            info_FOVangle = Math.Round(spread * FOV);
        }
        
        
        
        
        public void Run(){
            
            

            float Xx = 0;
            float Yy = 0;
            
            
            float acc = 0.2f;
            vs = 1;
            if(Keyboard.GetState().IsKeyDown(Keys.LeftShift)){ 
                vs = 1.4;
                if(sp < running_speed){
                sp += acc;}
            }
            else{
                sp = 1.2f;
            }
            
            
            
            
            if(Keyboard.GetState().IsKeyDown(Keys.E)){v+= vs;}
            if(Keyboard.GetState().IsKeyDown(Keys.Q)){v-= vs;}
            if(Keyboard.GetState().IsKeyDown(Keys.D)){Xx-=speed;}
            if(Keyboard.GetState().IsKeyDown(Keys.A)){Xx+=speed;}
            if(Keyboard.GetState().IsKeyDown(Keys.W)){Yy-=speed;}
            else if(Keyboard.GetState().IsKeyDown(Keys.S)){Yy+=speed;}
            if(v > 360){v = 0;}
            else if(v < 0){v=360;}
            V = v/180.0*Math.PI;
            double V2 = (v+90)/180.0*Math.PI;
            float X = Yy*(float)Math.Cos(V) + Xx*(float)Math.Cos(V2);
            float Y = Yy*(float)Math.Sin(V) + Xx*(float)Math.Sin(V2);

            
            
            double Reach = 1.2;
            Vector2 GridPSide = new Vector2((int)(MapP.X+X*15),(int)(MapP.Y+Y*15));
            Vector2 GridPFor = new Vector2((int)(MapP.X+X*15),(int)(MapP.Y+Y*15));
            Vector2 GridPReach = new Vector2((int)(MapP.X-(float)Math.Cos(V)*Reach),(int)(MapP.Y-(float)Math.Sin(V)*Reach));

            if(Game1._Map.MapList[(int)MapP.X,(int)GridPFor.Y].Type >= 1){MapP+= new Vector2(0,0);}
            else {MapP+= new Vector2(0,Y *sp);}
            if(Game1._Map.MapList[(int)GridPSide.X,(int)MapP.Y].Type >= 1){MapP+= new Vector2(0,0);}
            else {MapP+= new Vector2(X * sp,0);}

            double miningspeed = 1;
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && (int)GridPFor.X > 0 && (int)GridPFor.X < Game1._Map.MapList.GetLength(0)-1 && (int)GridPFor.Y > 0 && (int)GridPFor.Y <  Game1._Map.MapList.GetLength(1)-1){
                if(Game1._Map.MapList[(int)GridPFor.X,(int)GridPFor.Y]._MAT == Game1._Map.MAT[2]){
                    Game1._Map.OutdoorMapCreator();
                    Random rd = new Random();
                    for(int i = 0 ; i < 200 ; i++){
                        int X1 = rd.Next(1,Game1._Map.MapList.GetLength(0)-2);
                        int Y1 = rd.Next(1,Game1._Map.MapList.GetLength(1)-2);
                        if(Game1._Map.MapList[X1,Y1].Type == 0){
                            MapP = new Vector2(X1+0.5f,Y1+0.5f);
                            return;
                        }
                    }
                }

                if( Game1._Map.MapList[(int)GridPReach.X,(int)GridPReach.Y].Type > 0){
                    Game1._Map.MapList[(int)GridPReach.X,(int)GridPReach.Y].health -= miningspeed;
                    if(Game1._Map.MapList[(int)GridPReach.X,(int)GridPReach.Y].health<0){
                        Game1._Map.MapList[(int)GridPReach.X,(int)GridPReach.Y] = new Cell(Game1._Map.MAT[0],0);
                    }
                }
                
            }
            
            
            ScreenP = new Vector2(Game1.CellSize*MapP.X,Game1.CellSize*MapP.Y);
        }

        public void DRAW(){
            int S = (int)(Game1.CellSize * 0.2);
            Rectangle R = new Rectangle((int)(ScreenP.X-S*0.5),(int)(ScreenP.Y-S*0.5),S,S);
            Vector2 Angle = new Vector2((float)Math.Cos(V+info_FOVangle/180f*Math.PI),(float)Math.Sin(V+info_FOVangle/180f*Math.PI));
            Rectangle R2 = new Rectangle((int)(R.Center.X-(Angle.X*S*2)-S/4),(int)(R.Center.Y-(Angle.Y*S*2)-S/4),S/2,S/2);
            foreach(RayCastClass ellement in RaysToCast){
                ellement.drawline();
            }
            RaysToCast.Clear();
            
            
            Game1._spriteBatch.Draw(Game1.pixel, R, Color.Lime);
            Game1._spriteBatch.Draw(Game1.pixel, R2, Color.Green);
            
        }


        public void RayCast(){
            
            CastOneRay(0);
            for(double i = spread ; i <= FOV*spread ; i+= spread){
                
                Count = (int)(Game1.ScreenWidth*0.5) - Count;
                Count++;
                CastOneRay(FOV*spread-i);
                int save = Count;
                Count = Count + (int)(Game1.ScreenWidth*0.5)-1;
                CastOneRay(-i);
                Count = save;
                Count = (int)(Game1.ScreenWidth*0.5) - Count;
            }
            Count = (int)(Game1.ScreenWidth*0.5);
        }
        
        public void CastOneRay(double ofsett){
            
            int RenderCounterX = 0;
            int RenderCounterY = 0;

            CountRay = MapP;
            CountRayside = MapP;
            Vector2 RayPrev = MapP;
            int XXc = 0;
            int YXc = 0;
            int XYc = 0;
            int YYc = 0;

            double vo = v+ofsett;
            if(vo >= 360){vo = -360+vo;}
            else if(vo <= 0){vo=360+vo;}
            V = vo/180.0*Math.PI;

            double FishEyeRemoverP =  v/180.0*Math.PI;
            double FishEyeRemoverR =  V;
            
            double FishEyeRemoverPR = 0;
            //FishEyeRemoverPR = FishEyeRemoverP - FishEyeRemoverR;
            FishEyeRemoverPR = (ofsett*screenbend)/180.0*Math.PI;
            if(FishEyeRemoverPR > 30.0/180.0*Math.PI *screenbend){FishEyeRemoverPR = 30.0/180.0*Math.PI *screenbend;}
            if(FishEyeRemoverPR < -30.0/180.0*Math.PI *screenbend){FishEyeRemoverPR = -30.0/180.0*Math.PI *screenbend;}
            FishEyeRemoverPR = Math.Cos(FishEyeRemoverPR);
            //FishEyeRemoverPR = 1;
            
            Vector2 ScreenCountRayY = new Vector2(Game1.CellSize*Game1.MapWidth*Game1.MapWidth,Game1.CellSize*Game1.MapHight*Game1.MapHight);
            Vector2 ScreenCountRayX = new Vector2(Game1.CellSize*Game1.MapWidth*Game1.MapWidth,Game1.CellSize*Game1.MapHight*Game1.MapHight);

            Vector2 SaveCountRayY = new Vector2(Game1.MapWidth*Game1.MapWidth,Game1.CellSize*Game1.MapHight*Game1.MapHight);
            Vector2 SaveCountRayX = new Vector2(Game1.MapWidth*Game1.MapWidth,Game1.CellSize*Game1.MapHight*Game1.MapHight);

            if(vo > 180)
            {CountRay.Y = (int)(CountRay.Y*Game1.MapHight)/Game1.MapHight;
            CountRay.X -= (float)Math.Cos(V) * (Vector2.Distance(MapP,CountRay))/(float)Math.Sin(V);}
            else if(vo < 180)
            {CountRay.Y = (int)((CountRay.Y*Game1.MapHight))/Game1.MapHight+1;
            CountRay.X += (float)Math.Cos(V) * (Vector2.Distance(MapP,CountRay))/(float)Math.Sin(V);}


            if(vo < 90 || vo > 270)
            {CountRayside.X = (int)((CountRayside.X*Game1.MapWidth))/Game1.MapWidth+1;
            CountRayside.Y += (float)Math.Sin(V) * (Vector2.Distance(MapP,CountRayside))/(float)Math.Cos(V);}
            else if(vo > 90 || vo < 270)
            {CountRayside.X = (int)((CountRayside.X*Game1.MapWidth))/Game1.MapWidth;
            CountRayside.Y -= (float)Math.Sin(V) * (Vector2.Distance(MapP,CountRayside))/(float)Math.Cos(V);}
            
            
            
            
            float YO = 1.0f;
            float XO = ((float)Math.Cos(V) * (1.0f)/(float)Math.Sin(V));

            

            for(RenderCounterY = 0 ; RenderCounterY < RenderDistance ; RenderCounterY++){
                int XX = (int)(CountRay.X);
                int YY = (int)(CountRay.Y);
                if(XX > 0 && XX < Game1.MapWidth && YY > 0 && YY < Game1.MapHight || RenderCounterY == 0){
                    
                    if(vo > 180){
                        if(RenderCounterY != 0){
                            if(Game1._Map.MapList[XX,YY].Type != 0 ){
                                SaveCountRayY = CountRay;
                                XYc = XX;
                                YYc = YY;
                                break;
                            }
                            
                            Game1._screen.DrawRoom(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRay,MapP), new Vector2(CountRay.X, CountRay.Y),FishEyeRemoverPR,Game1._Map.MapList[XX,YY]._MAT);
                        }
                        
                        CountRay.Y += YO;
                        CountRay.X +=  XO;    
                    }
                    else if(vo < 180){
                        if(RenderCounterY != 0){
                           
                            if(Game1._Map.MapList[XX,YY-1].Type != 0){
                                SaveCountRayY = CountRay;
                                XYc = XX;
                                YYc = YY-1;
                                break;
                            } 
                            
                            Game1._screen.DrawRoom(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRay,MapP), new Vector2(CountRay.X, CountRay.Y),FishEyeRemoverPR,Game1._Map.MapList[XX,YY-1]._MAT);
                        }
                        
                        CountRay.Y -= YO;
                        CountRay.X -=  XO;
                        
                    }
                }
            }
                

            
            float XOside = 1.0f;
            float YOside = (float)Math.Sin(V) * (1.0f)/(float)Math.Cos(V);

            for( RenderCounterX = 0 ; RenderCounterX < RenderDistance ; RenderCounterX++){
                int XX = (int)(CountRayside.X);
                int YY = (int)(CountRayside.Y);
                if(XX > 0 && XX < Game1.MapWidth && YY > 0 && YY < Game1.MapHight || RenderCounterX == 0){
                    
                    if(vo < 90 || vo > 270){
                        if(RenderCounterX != 0){
                            
                            if(Game1._Map.MapList[XX-1,YY].Type != 0){
                                SaveCountRayX = CountRayside;
                                XXc = XX-1;
                                YXc = YY;  
                                break;
                            }
                            
                            
                            Game1._screen.DrawRoom(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRayside,MapP), new Vector2(CountRayside.X, CountRayside.Y), FishEyeRemoverPR,Game1._Map.MapList[XX,YY]._MAT);
                            
                        }
                        CountRayside.Y -= YOside;
                        CountRayside.X -=  XOside;    
                    }
                    else if(vo > 90 || vo < 270){
                        if(RenderCounterX != 0){
                            if(Game1._Map.MapList[XX,YY].Type != 0 && RenderCounterX != 0){
                                SaveCountRayX = CountRayside;

                                XXc = XX;
                                YXc = YY; 
                                break;
                            }
                            
                          
                            Game1._screen.DrawRoom(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRayside,MapP), new Vector2(CountRayside.X, CountRayside.Y),FishEyeRemoverPR,Game1._Map.MapList[XX,YY]._MAT);
                            
                            
                        }
                        CountRayside.Y += YOside;
                        CountRayside.X +=  XOside;    
                    }
                    
                }
            }
            double PosOfsett;
            if(ofsett >= 0){
                PosOfsett = ofsett;
            }
            else{
                PosOfsett = -ofsett;
            }

            Cell M = Game1._Map.MapList[XYc,YYc];
            Color C0 = Game1._Map.MapList[XYc,YYc].COLOR;
            double Distance = 0;
            int Brightness = 0;

            ScreenCountRayY = CountRay*Game1.CellSize;
            ScreenCountRayX = CountRayside*Game1.CellSize;
            
            float D_y = Vector2.Distance(SaveCountRayY,MapP);
            float D_x = Vector2.Distance(SaveCountRayX,MapP);
            if(D_y < D_x && D_y < RenderDistance-2){
                RaysToCast.Add(new RayCastClass(ScreenCountRayY,ScreenP,Color.Moccasin));
                Distance = Vector2.Distance(CountRay,MapP);
                M = Game1._Map.MapList[XYc,YYc];
                Game1._screen.DrawWallSegment(Game1.ScreenWidth - Count,1,Distance,FishEyeRemoverPR,M,Brightness);
            }
            else if(D_y > D_x && D_x < RenderDistance-2){
                RaysToCast.Add(new RayCastClass(ScreenCountRayX,ScreenP,Color.Moccasin));
                Distance = Vector2.Distance(CountRayside,MapP);
                int s = (int)Game1._Map.MapList[XXc,YXc]._MAT.Softnes;
                M = Game1._Map.MapList[XXc,YXc];
                Brightness = -(25 * s);
                Game1._screen.DrawWallSegment(Game1.ScreenWidth - Count,1,Distance,FishEyeRemoverPR,M,Brightness);
                
            }
            else{
                Vector2 MaxDistance = new Vector2((float)Math.Cos(V) ,(float)Math.Sin(V))*(RenderDistance-2)*-1 * Game1.CellSize + ScreenP;
                RaysToCast.Add(new RayCastClass(MaxDistance,ScreenP,Color.Moccasin));
                Game1._screen.DrawWallSegment(Game1.ScreenWidth - Count,1,RenderDistance-2,FishEyeRemoverPR,new Cell(Game1._Map.MAT[0],0),0);
                
            }               
        }
    }
}