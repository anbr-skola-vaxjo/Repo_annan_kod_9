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
        private List<ScreenObject> _Que = new List<ScreenObject>();
        public Screen(Player P){
            _player = P;
        }
        public void DrawEntites(List<Entitiy> Ent){

            double angle = _player.info_FOVangle;
            
            double V = _player.V;
            double v = _player.v;
            float info_ofsett = ((float)angle/180f*(float)Math.PI);
            Vector2 Forward2 = new Vector2( (float)Math.Cos(V + info_ofsett ),(float)Math.Sin(V + info_ofsett))*-1;
            Vector2 Forward = new Vector2( (float)Math.Cos(V  ),(float)Math.Sin(V ))*-1;

            
            
            

            Forward+= _player.MapP;
            Forward2+= _player.MapP;
            Rectangle R2;

            foreach(Entitiy E in Ent){
                Vector2 Ent_comp_palyer = E.MapP;
                if(Vector2.Distance(E.MapP,_player.MapP) < _player.RenderDistance){
                    double angle2 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward2);
                    double angle3 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward);

                    
                    if(angle2  <= angle){
                        
                        
                        float dist = 0.1f * (float)Game1.ScreenHight/Vector2.Distance(Ent_comp_palyer,_player.MapP);
                        int w = (int)(E.Type.size.X * dist);
                        int h = (int)(E.Type.size.Y * dist);
                        int p = (int)((float)(Game1.ScreenWidth) / (angle*2)  * (angle3)) + Game1.MapWidth * Game1.CellSize - (int)(w *0.5f);
                        int exw = p - Game1.MapWidth * Game1.CellSize;
                       
                        R2 = new Rectangle(p,(int)(Game1.ScreenHight*0.5)- (int)(h *0.5f),w,h);
                        
                        
                        
                        _Que.Add(new ScreenObject(Vector2.Distance(Ent_comp_palyer,_player.MapP),1,R2,E.Type._Col,E.Type.Tex));
                        
                    }
                }
            }
        }
        
        public void DrawWallSegment(int Where, int Width, double Distance, double FishEyeOffset , Cell MC, int B){
            Material M = MC._MAT;
            Color C = new Color(M.FlorOrWall.R+B,M.FlorOrWall.G+B,M.FlorOrWall.B+B);
            int H = (int)( 1.5 * Game1.ScreenHight/(Distance*FishEyeOffset));
            int Hs = H;
            if(H > Game1.ScreenHight*4){H = Game1.ScreenHight*4;}
            Rectangle Wallsegment = new Rectangle(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5-H*0.5), Width,H);
            float cutof = 5;
            float f = -1f*((float)Distance*cutof/((float)_player.RenderDistance-2))+cutof;
            if(f > 1){f = 1;}
            
            
            Color C2 = C*f; C2.A = 255;
            
            int health = 100 - (int)MC.health;
            C = new Color(C.R - health, C.G  - health, C.B  - health);
            
            _Que.Add(new ScreenObject((float)(Distance*FishEyeOffset),(float)FishEyeOffset,Wallsegment,C,MC._MAT._Tex));
        }

        public void Draw_Que(){
            sort_ScreenObject_list(_Que);
            float cutof = 5;   
            for(int i = _Que.Count-1; i >= 0; i--){
                
                float f = -1f*((_Que[i].distance/_Que[i].FishEye)*cutof/((float)_player.RenderDistance-2))+cutof;
                if(f > 1){f = 1;}
            
                _Que[i].Draw(f);
            }
            
            
            _Que.Clear();
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

        private List<ScreenObject> sort_ScreenObject_list(List<ScreenObject> List){
            for(int i = 0; i < List.Count-1; i++){
                for(int j = 0; j < List.Count-1-i; j++){
                    if(List[j].distance>List[j+1].distance){
                        
                        ScreenObject temp = List[j];
                        List[j] = List[j+1];
                        List[j+1] = temp;

                    }
                }
            }
            return new List<ScreenObject>();
        }
        

        
    }


}
