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
        public int RenderDistance = 10;
        public double info_FOVangle;
        public Vector2 ScreenP;
        public  Vector2 MapP = new Vector2(2f,2f);
        public Vector2 Staring_MapP;
        public float speed = 0.02f;
        private int Count = 1;
        public double screenbend = 1;
        public double v = 270;
        public double spread = 1;
        public float running_speed = 2.5f;
        private double sensetivety = 100;
        public int FOV = 0;
        private double vs = 1;
        public double V = 0;
        public float sp = 1.2f;
        private List<RayCastClass> RaysToCast = new List<RayCastClass>();
        public int Keys_found = 0;

      

        public int Max_Stammina = 100;
        public float Stammina;

        private float Max_Stamina_regen = 0.1f;
        private float Max_Running_change = -0.2f;
        private float Step_speed = 0.01f;
        private float Step = 0;

        

        private bool Moving = false;
        private bool Running = false;
        private bool Exhausted = false;
        private float Stamina_change;
       


        private Vector2 CountRay;
        private Vector2 CountRayside;

        public Keys W;
        public Keys A;
        public Keys S;
        public Keys D;
        public Keys E;
        public Keys Q;

        public Keys Shift;
        public Player(Keys w, Keys a, Keys s, Keys d, Keys e, Keys q, Keys shift){
            W = w;
            A = a;
            S = s;
            D = d;
            E = e;
            Q = q;
            Shift = shift;


            Random rd = new Random();
            for(int i = 0 ; i < 200 ; i++){
                int X = rd.Next(1,Game1._Map.MapList.GetLength(0)-2);
                int Y = rd.Next(1,Game1._Map.MapList.GetLength(1)-2);
                if(Game1._Map.MapList[X,Y].Type == 0){
                    MapP = new Vector2(X+0.5f,Y+0.5f);
                    Staring_MapP = MapP;
                    break;
                }
            }

            CountRay = MapP;
            CountRayside = MapP;

            Stamina_change = Max_Stamina_regen;
            
            Stammina = Max_Stammina;
            
        }
        public void SetRay(double S){
            FOV = (int)(Game1.ScreenWidth*0.5)-1;
            spread = S/(double)FOV;
            info_FOVangle = S;
        }
        
        
        
        
        public void Run(){
            RaysToCast.Clear();
            float Xx = 0;
            float Yy = 0;
            Moving = false;
            Running = false;
           
            Stamina_change = Max_Stamina_regen;
            
            float acc = 0.2f;
            vs = 1;

            

            if(Keyboard.GetState().IsKeyDown(Shift)){ 
                vs = 1.4;
                if(sp < running_speed){
                sp += acc;}
                Running = true;
            }
            else{
                sp = 1.2f;
            }
            
            if(Game1.Mouse_turn == true){
                v-= (double)(((Game1.WindowWidth*0.5 - Mouse.GetState().Position.X)/(Game1.WindowWidth)*vs*sensetivety));
            }
            else{
                if(Keyboard.GetState().IsKeyDown(E)){v+= vs;}
                if(Keyboard.GetState().IsKeyDown(Q)){v-= vs;}
            }
            

            if(Keyboard.GetState().IsKeyDown(D)){Xx-=speed; Moving = true;}
            if(Keyboard.GetState().IsKeyDown(A)){Xx+=speed; Moving = true;}
            if(Keyboard.GetState().IsKeyDown(W)){Yy-=speed; Moving = true;}
            else if(Keyboard.GetState().IsKeyDown(S)){Yy+=speed; Moving = true;}
            if(v > 360){v = 0;}
            else if(v < 0){v=360;}
            V = v/180.0*Math.PI;
            double V2 = (v+90)/180.0*Math.PI;

            float Yy_x =  Yy*(float)Math.Cos(V);
            float Yy_y =  Yy*(float)Math.Sin(V);

            float Xx_x = Xx*(float)Math.Cos(V2);
            float Xx_y = Xx*(float)Math.Sin(V2);

            float X = Yy_x + Xx_x;
            float Y = Yy_y + Xx_y;
            if(Game1.Testing_mode == false){
                if(Exhausted){
                    Moving = false;
                }
                
                if(Moving){
                    Stamina_change = 0.01f;
                    if(Running){
                        Stamina_change = Max_Running_change;
                    }
                }


                else{
                    Stamina_change = Max_Stamina_regen;
                }

                if(Stammina + Stamina_change < Max_Stammina || Exhausted == true){
                    Stammina += Stamina_change;
                }

                if(Stammina <= 0 && Exhausted == false){
                    Exhausted = true;
                    Stammina = -10;
                }
                else if(Stammina > (Max_Stammina*0.2f)){
                    Exhausted = false;
                }

                if(Exhausted){
                    sp = 0;
                }

                            
                foreach(Entitiy M in Game1.Monsters){
                    if(Vector2.Distance(M.MapP,MapP) <= 0.5){
                        Game1.Game_over = true;
                        return;
                    }
                }  

                Game1.Info_text[5] = "Stammina: " + Math.Round(Stammina);
            }
            
            if(Moving || Exhausted){
                int step_size = 2;
                float step_height = 0.01f;
                if(Step > 1){
                    Step = 0;
                }

                if(Running){
                    step_size = 4;
                    step_height = 0.015f;
                }

                if(Exhausted){
                    step_size = 1;
                    step_height = 0.04f;
                    Game1.Info_text[5] = "stammina: Exhausted";
                }

                Game1.Height_offset = Game1.Player_Height_offset + (float)(Math.Cos(Step*step_size*Math.PI*2)*step_height);
                Step+= Step_speed;
                
            }
            


            
            Vector2 GridPSide = new Vector2((int)(MapP.X+Xx_x*5),(int)(MapP.Y+Xx_y*5));
            Vector2 GridPFor = new Vector2((int)(MapP.X+Yy_x*15),(int)(MapP.Y+Yy_y*15));
            
            if(Keys_found >= Game1.needed_keys && Game1._Map.MapList[(int)GridPFor.X,(int)GridPFor.Y].Type == 2){
                Game1.Game_over = true;
                Game1.Escape_Game = true;
                return;
            }


            float stopX = 1;
            float stopY = 1;

            Vector2 Movement = new Vector2(X * sp,Y *sp);
            if(Game1._Map.MapList[(int)MapP.X,(int)GridPFor.Y].Type >= 1 )
            {stopY = 0;}
            if(Game1._Map.MapList[(int)MapP.X,(int)GridPSide.Y].Type >= 1 )
            {stopY = 0;}
            
            if(Game1._Map.MapList[(int)GridPSide.X,(int)MapP.Y].Type >= 1)
            {stopX = 0;}
            if(Game1._Map.MapList[(int)GridPFor.X,(int)MapP.Y].Type >= 1)
            {stopX = 0;}

            
            


            MapP += new Vector2(Movement.X*stopX,Movement.Y*stopY);
            
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
            int Tex_off_X = 0;
            

            
            int Tex_off_Y = 0;

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

            float Cos_V = (float)Math.Cos(V);
            float Sin_V = (float)Math.Sin(V);

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

            if(vo > 180){
                CountRay.Y = (int)Math.Floor(CountRay.Y);
                CountRay.X -= Cos_V * (Vector2.Distance(MapP,CountRay))/Sin_V;

            }
            else if(vo < 180){
                CountRay.Y = (int)Math.Ceiling(CountRay.Y);
                CountRay.X += Cos_V * (Vector2.Distance(MapP,CountRay))/Sin_V;

            }


            if(vo < 90 || vo > 270){
                CountRayside.X = (int)Math.Ceiling(CountRayside.X);
                CountRayside.Y += Sin_V * (Vector2.Distance(MapP,CountRayside))/Cos_V;
                
            }
            else if(vo > 90 || vo < 270){
                CountRayside.X = (int)Math.Floor(CountRayside.X);
                CountRayside.Y -= Sin_V * (Vector2.Distance(MapP,CountRayside))/Cos_V;

            }
            
            
            
            
            float YO = 1.0f;
            float XO = ((float)Math.Cos(V) * (1.0f)/(float)Math.Sin(V));
            Vector2 OneStep = new Vector2(XO,YO);
            

            for(RenderCounterY = 0 ; RenderCounterY < RenderDistance ; RenderCounterY++){
                int XX = (int)(CountRay.X);
                int YY = (int)(CountRay.Y);
                if(XX > 0 && XX < Game1.MapWidth && YY > 0 && YY < Game1.MapHight || RenderCounterY == 0){
                    
                    if(vo > 180){
                        
                        if(RenderCounterY != 0){
                            Game1._screen.Add_Floor_to_que(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRay,MapP),new Vector2(XX, YY-1) , new Vector2(CountRay.X, CountRay.Y-1),FishEyeRemoverPR);

                            if(Game1._Map.MapList[XX,YY].Type != 0 ){
                                SaveCountRayY = CountRay;
                                XYc = XX;
                                YYc = YY;
                                break;
                            }
                            
                        }
                        
                        CountRay.Y += YO;
                        CountRay.X +=  XO;    
                    }
                    else if(vo < 180){
                        Tex_off_Y = 1;
                        if(RenderCounterY != 0){
                            Game1._screen.Add_Floor_to_que(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRay,MapP), new Vector2(XX,YY),new Vector2(CountRay.X,CountRay.Y),FishEyeRemoverPR);
                           
                            if(Game1._Map.MapList[XX,YY-1].Type != 0){
                                SaveCountRayY = CountRay;
                                XYc = XX;
                                YYc = YY-1;
                                break;
                            } 
                            
                        }
                        
                        CountRay.Y -= YO;
                        CountRay.X -=  XO;
                        
                    }
                }
            }
                

            
            float XOside = 1.0f;
            float YOside = (float)Math.Sin(V) * (1.0f)/(float)Math.Cos(V);
            Vector2 SideOneStep = new Vector2(XOside,YOside);

            for( RenderCounterX = 0 ; RenderCounterX < RenderDistance ; RenderCounterX++){
                int XX = (int)(CountRayside.X);
                int YY = (int)(CountRayside.Y);
                if(XX > 0 && XX < Game1.MapWidth && YY > 0 && YY < Game1.MapHight || RenderCounterX == 0){
                    
                    if(vo < 90 || vo > 270){
                        Tex_off_X = 1;
                        if(RenderCounterX != 0){
                            Game1._screen.Add_Floor_to_que(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRayside,MapP), new Vector2(XX,YY), new Vector2(CountRayside.X,CountRayside.Y),FishEyeRemoverPR);
                            
                            if(Game1._Map.MapList[XX-1,YY].Type != 0){
                                SaveCountRayX = CountRayside;
                                XXc = XX-1;
                                YXc = YY;  
                                break;
                            }
                            
                            
                            
                        }
                        CountRayside.Y -= YOside;
                        CountRayside.X -=  XOside;    
                    }
                    else if(vo > 90 || vo < 270){
                        if(RenderCounterX != 0){
                            Game1._screen.Add_Floor_to_que(Game1.ScreenWidth - Count,1,Vector2.Distance(CountRayside,MapP), new Vector2(XX-1,YY), new Vector2(CountRayside.X-1,CountRayside.Y),FishEyeRemoverPR);

                            if(Game1._Map.MapList[XX,YY].Type != 0 && RenderCounterX != 0){
                                SaveCountRayX = CountRayside;

                                XXc = XX;
                                YXc = YY; 
                                break;
                                
                            }                                    
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
            Color C0 = Game1._Map.MapList[XYc,YYc].FlorOrWall;
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
                Game1._screen.Add_Wall_to_que(Game1.ScreenWidth - Count,Distance,FishEyeRemoverPR,M,Brightness,new Vector2(XYc,YYc+Tex_off_Y),CountRay);
            }
            else if(D_y > D_x && D_x < RenderDistance-2){
                RaysToCast.Add(new RayCastClass(ScreenCountRayX,ScreenP,Color.Moccasin));
                Distance = Vector2.Distance(CountRayside,MapP);
                int s = (int)Game1._Map.MapList[XXc,YXc]._MAT.Softnes;
                M = Game1._Map.MapList[XXc,YXc];
                Brightness = -(25 * s);
                Game1._screen.Add_Wall_to_que(Game1.ScreenWidth - Count,Distance,FishEyeRemoverPR,M,Brightness,new Vector2(XXc+Tex_off_X,YXc),CountRayside);
                 
            }
            else{
                Vector2 MaxDistance = new Vector2((float)Math.Cos(V) ,(float)Math.Sin(V))*(RenderDistance-2)*-1 * Game1.CellSize + ScreenP;
                RaysToCast.Add(new RayCastClass(MaxDistance,ScreenP,Color.Moccasin));
                
                Game1._screen.Add_Wall_to_que(Game1.ScreenWidth - Count,RenderDistance-2,FishEyeRemoverPR, null , 0 ,new Vector2(),new Vector2());
                
            }               
        }
    }
}