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
    public class Screen
    {
        private Player _player;
        private Color Light = Color.Magenta;
        private Color Dark = Color.Purple;
        public Screen(Player P){
            _player = P;
        }
        public void DrawEntites(List<Entitiy> Ent){
            
            double angle = _player.info_angle;
            Vector2 Forward = _player.info_Forward;
            Vector2 Forward2 = _player.info_Forward2;

            Forward+= _player.MapP;
            Forward2+= _player.MapP;
            Rectangle R2;

            foreach(Entitiy E in Ent){
                Vector2 Ent_comp_palyer = E.MapP;
                double angle2 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward);
                double angle3 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward2)- angle;

                
                if(angle2  <= angle && angle3  <= angle && angle3 >= -angle){
                    float dist = 0.1f * (float)Game1.ScreenHight/Vector2.Distance(Ent_comp_palyer,_player.MapP);
                    int w = (int)(E.Type.size.X * dist);
                    int h = (int)(E.Type.size.Y * dist);
                    int p = (int)((float)(Game1.ScreenWidth) / angle  * angle3 * 0.5) + Game1.MapWidth * Game1.CellSize * 2 - (int)(w *0.5f);
                    int exw = p - Game1.MapWidth * Game1.CellSize;
                    if( exw < 0){
                        w += exw;
                        p -= exw;
                    }
                    R2 = new Rectangle(p,(int)(Game1.ScreenHight*0.5)- (int)(h *0.5f),w,h);
                    
                    Game1._spriteBatch.Draw(Game1.pixel,R2,E.Type._Col);
                    
                }
                
                
            }
        }
        public void DrawWallSegment(int Where, int Width, double Distance, double FishEyeOffset , Cell MC, int B){
            Material M = MC._MAT;
            Color C = new Color(M.FlorOrWall.R+B,M.FlorOrWall.G+B,M.FlorOrWall.B+B);
            int H = (int)( 1.5 * Game1.ScreenHight/(Distance*FishEyeOffset));
            int Hs = H;
            if(H > Game1.ScreenHight){H = Game1.ScreenHight;}
            Rectangle Wallsegment = new Rectangle(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5-H*0.5), Width,H);
            Color C2 = new Color(C.R,C.G,C.B)*((float)(H*10)/(float)Game1.ScreenHight); C2.A = 255;
            if(C2.R + C2.G + C2.B < C.R + C.G + C.B){
                C = C2;}
            int health = 100 - (int)MC.health;
            C = new Color(C.R - health, C.G  - health, C.B  - health);
            Game1._spriteBatch.Draw(Game1.pixel,Wallsegment,C);
            Color C3 = new Color((C.R+C.G+C.B)/6,(C.R+C.G+C.B)/6,(C.R+C.G+C.B)/6);
            if(M.detail > 0){
                
                Rectangle DetailSegment = new Rectangle(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5+Hs*0.5-H*0.1)+1, Width,(int)(Hs*0.1));
                Game1._spriteBatch.Draw(Game1.pixel,DetailSegment,C3*0.5f);
            }
            if(M.detail > 1){
                Rectangle DetailSegment = new Rectangle(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5+Hs*0.5-Hs)-1, Width,(int)(Hs*0.1));
                Game1._spriteBatch.Draw(Game1.pixel,DetailSegment,C3*0.5f);
            }


        }

        public void DrawRoom(int Where, int Width, double Distance, Vector2 thiscell, double FishEyeOffset , Material C){
            
            int H = (int)(1.5 * Game1.ScreenHight/(Distance* FishEyeOffset) );
            if(H> Game1.ScreenHight){H =Game1.ScreenHight;}
                
            Vector2 FloorSegment = new Vector2(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5+H*0.5));
            Vector2 RoofSegment = new Vector2(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5-H*0.5));
            

            Game1._spriteBatch.Draw(Game1.pixel,FloorSegment,C.FlorOrWall);
            Game1._spriteBatch.Draw(Game1.pixel,RoofSegment,C.Roof);

            
            
        }



        public void Room(Color C_Floor,Color C_Cealing){
            Rectangle Floor = new Rectangle(Game1.MapWidth * Game1.CellSize+3,(int)(Game1.ScreenHight*0.5),Game1.ScreenWidth,(int)(Game1.ScreenHight*0.5));
            Rectangle Cealing = new Rectangle(Game1.MapWidth * Game1.CellSize+3,0,Game1.ScreenWidth,(int)(Game1.ScreenHight*0.5));
            Game1._spriteBatch.Draw(Game1.pixel,Floor,C_Floor);
            Game1._spriteBatch.Draw(Game1.pixel,Cealing,C_Cealing);
        }

        private void drawline(Vector2 A, Vector2 B, Color C)
        {

            Vector2 p = A - B;
            if(A.X >= B.X)
            {
                p = new Vector2(A.X-B.X,p.Y);
            }
            else
            {
                p = new Vector2(B.X-A.X,p.Y);
            }

            if(A.Y >= B.Y)
            {
                p = new Vector2(p.X,A.Y-B.Y);
            }
            else
            {
                p = new Vector2(p.X,B.Y-A.Y);
            }
            
            Vector2 pen = B;
            int max;
            int min;
            
            if(p.X >= p.Y)
            {
                max = (int)Math.Round(p.X); 
                min = (int)Math.Round(p.Y); 
            }
            else
            {
                max = (int)Math.Round(p.Y); 
                min = (int)Math.Round(p.X); 
            }
            float p1 = p.X/max;
            float p2 = p.Y/max;
            if(A.X < B.X)
            {
                p1*=-1;
            }
            if(A.Y < B.Y)
            {
                p2*=-1;
            }
        
            for(int i = 0 ; i < max ; i++)
            {
                pen+=new Vector2(p1,p2);   
                Game1._spriteBatch.Draw(Game1.pixel,pen,C);
            }

        }
    }


}
