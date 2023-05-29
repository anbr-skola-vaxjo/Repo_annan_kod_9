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
        private List<FloorSegment>[] _Que_floor = new List<FloorSegment>[Game1.ScreenWidth];
        private SpriteBatch _spriteBatch;


        public Screen(Player P){
            _player = P;

            for(int i = 0 ; i < _Que_floor.Length; i++){
                _Que_floor[i] = new List<FloorSegment>();
            }

            _spriteBatch = Game1._spriteBatch;

        }
        public void Add_Enteties_to_que(List<Entitiy> Ent){

            double angle = _player.info_FOVangle;
            
            double V = _player.V;
            double v = _player.v;
            float info_ofsett = ((float)angle/180f*(float)Math.PI);
            Vector2 Forward2 = new Vector2( (float)Math.Cos(V + info_ofsett ),(float)Math.Sin(V + info_ofsett))*-1;
            Vector2 Forward = new Vector2( (float)Math.Cos(V  ),(float)Math.Sin(V ))*-1;

            
            
            

            Forward+= _player.MapP;
            Forward2+= _player.MapP;
            Rectangle R2;
            int i = 0;
            
            
            foreach(Entitiy E in Ent){
                Vector2 Ent_comp_palyer = E.MapP;
                if(Vector2.Distance(E.MapP,_player.MapP) < _player.RenderDistance){
                    double angle2 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward2);
                    double angle3 =  Game1.Vector2Angle(_player.MapP,Ent_comp_palyer,Forward);

                    
                    if(angle2  <= angle){
                        i++;
                        
                        int dist = (int)(Game1.Wall_hight * (float)Game1.ScreenHight/Vector2.Distance(Ent_comp_palyer,_player.MapP));
                        int w = (int)(E.Type.size.X * dist);
                        int h = (int)(E.Type.size.Y * dist);
                        int p = (int)((float)(Game1.ScreenWidth) / (angle*2)  * (angle3)) + Game1.MapWidth * Game1.CellSize - (int)(w *0.5f);
                        int exw = p - Game1.MapWidth * Game1.CellSize;
                       
                        R2 = new Rectangle(p,(int)(Game1.ScreenHight*0.5-h *0.5f),w,h);
                        
                        
                        
                        _Que.Add(new ScreenObject(Vector2.Distance(Ent_comp_palyer,_player.MapP),1,R2,E.Type._Col,E.Type.texture_Layers));
                        
                    }
                }
                  
            }

            
            Game1.Info_text[4] = "Entetys Drawn: " + i;
                
            
            
        }
        
        public void Add_Wall_to_que(int Where, double Distance, double FishEyeOffset , Cell MC, int B, Vector2 Thiss_cell, Vector2 MapP){

            int H = (int)( Game1.Wall_hight * Game1.ScreenHight/(Distance * FishEyeOffset));
            int Hs = H;
            Rectangle Wallsegment = new Rectangle(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5-H*0.5), 1, H);
            Color C = Color.Black;
            List<Simple_texture_layer> Tex_Layers = new List<Simple_texture_layer>();

            if(MC != null){
                Material M = MC._MAT;
                C = new Color(MC.FlorOrWall.R+B,MC.FlorOrWall.G+B,MC.FlorOrWall.B+B);
                
                

                float cutof = 2;
                float f = -1f*((float)Distance*cutof/((float)_player.RenderDistance-2))+cutof;
                if(f > 1){f = 1;}
                
            
                
                
                float Cell_segment = Vector2.Distance(Thiss_cell,MapP);
                
                foreach(Material_texture_layer Layer in MC._MAT._Texture_Layers){
                    
                    Texture2D Tex = Layer._tex;

                    if(Layer._divided_tex.Count() > 1){
                        Tex = Layer._divided_tex[(int)((Layer._divided_tex.Count()-1)*Cell_segment)];
                    }

                    Tex_Layers.Add(new Simple_texture_layer(Tex,Layer.Glow));
                
                }
                
            }
            
            _Que.Add(new ScreenObject((float)(Distance*FishEyeOffset),(float)FishEyeOffset,Wallsegment,C,Tex_Layers));
            
        }

        public void Draw_Wall_Que(){
            sort_ScreenObject_list(_Que);
            float cutof = 2;   
            for(int i = _Que.Count-1; i >= 0; i--){
                
                float f = -1f*((_Que[i].distance/_Que[i].FishEye)*cutof/((float)_player.RenderDistance-2))+cutof;
                if(f > 1){f = 1;}
            
                _Que[i].Draw(f);
            }
            
            
            _Que.Clear();
        }

        public void Add_Floor_to_que(int Where, int Width, double Distance, Vector2 thiscell, Vector2 MapP , double FishEyeOffset){
            
            Material C = Game1._Map.MapList[(int)(thiscell.X),(int)(thiscell.Y)]._MAT;
            int H = (int)(Game1.Wall_hight * Game1.ScreenHight/(Distance* FishEyeOffset));
            int H2 = (int)(Game1.Wall_hight * Game1.ScreenHight/((Distance)* FishEyeOffset));
            
            
            
                
            Vector2 FloorSegment = new Vector2(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5+H*0.5));
            Vector2 RoofSegment = new Vector2(Where+Game1.MapWidth * Game1.CellSize, (int)(Game1.ScreenHight*0.5-H*0.5));

            int _Where;
            if(Where < Game1.ScreenWidth && Where >= 0){
                _Where = Where;
            }
            else{
                _Where = Where*-1;
            }

            


            FloorSegment _FloorSegment = new FloorSegment( (int)(Game1.ScreenHight*0.5+H*0.5), H, thiscell, MapP);

            _Que_floor[_Where].Add(_FloorSegment);
         

        }


        public void Draw_Floor_Que(){
            for(int i = 0 ; i < _Que_floor.Length; i++){
                sort_FloorSegment_list(_Que_floor[i]);
                for(int j = _Que_floor[i].Count -2 ; j >= 0; j--){
                    int H = (int)(_Que_floor[i][j].H*0.5*Game1.Height_offset);
                    int H2 = (int)(_Que_floor[i][j+1].H*0.5*Game1.Height_offset);
                    Rectangle Floor;
                    Vector2 Floor_ege;
                    Rectangle Roof;
                    Vector2 Roof_ege;
                    
                    int _where = _Que_floor[i][j].s_Where+H;
                    int _where2 = _Que_floor[i][j+1].s_Where+H2;

                    Floor = new Rectangle(i + Game1.MapWidth * Game1.CellSize ,_where, 1 ,_where2 -_where);
                    Floor_ege = new Vector2(i + Game1.MapWidth * Game1.CellSize, _where);

                    
                    

                    int RH = _Que_floor[i][j+1].s_Where - H2  - _Que_floor[i][j].s_Where - H;

                    int Diff;

                    if(Game1.Height_offset >= 0){
                       Diff = _Que_floor[i][j+1].s_Where  - _Que_floor[i][j].s_Where + H2;
                    }
                    else{
                        Diff = _Que_floor[i][j+1].s_Where - H2  - _Que_floor[i][j].s_Where - H;
                    }
                    Roof = new Rectangle(i + Game1.MapWidth * Game1.CellSize , _Que_floor[i][j].s_Where*-1 - H + Game1.ScreenHight - RH,  1 , Diff);
                    Roof_ege = new Vector2(i + Game1.MapWidth * Game1.CellSize , _Que_floor[i][j].s_Where*-1 - H + Game1.ScreenHight - RH );
                                  

                    float cutof = 5;
                    float f = -1f*((float)Vector2.Distance(_Que_floor[i][j].MapP,_player.MapP)*cutof/((float)_player.RenderDistance-2))+cutof;
                    if(f > 1){f = 1;}

                    Color fC2 = _Que_floor[i][j].fC*f; 
                    Color rC2 = _Que_floor[i][j].rC*f;

                    if(Game1._Map.MapList[(int)_Que_floor[i][j].MapP.X,(int)_Que_floor[i][j].MapP.Y].Type!=0 && (int)_Que_floor[i][j].MapP.X - 1 > 0){
                        Cell _cell;

                        if(Game1._Map.MapList[(int)_Que_floor[i][j].MapP.X-1,(int)_Que_floor[i][j].MapP.Y-1].Type == 0){
                            _cell = Game1._Map.MapList[(int)_Que_floor[i][j].MapP.X-1,(int)_Que_floor[i][j].MapP.Y-1];
                            
                        }
                        else{
                            _cell = Game1._Map.MapList[(int)_player.MapP.X,(int)_player.MapP.Y];
                        }

                        
                        fC2 = _cell.FlorOrWall*f; 
                        rC2 = _cell.Roof*f; 
                        
                    }
                    
                    
                    fC2.A = 255;
                    rC2.A = 255;

                    
                    _spriteBatch.Draw(Game1.pixel, Floor, fC2);
                    
                    _spriteBatch.Draw(Game1.pixel, Floor_ege, Color.Black);
                    


                    _spriteBatch.Draw(Game1.pixel, Roof, rC2);

                    _spriteBatch.Draw(Game1.pixel, Roof_ege, Color.Black);
                    
                }
            }

            for(int i = 0 ; i < _Que_floor.Length; i++){
                _Que_floor[i] = new List<FloorSegment>();
            }

            
        }

        public void Horizon(){
            Cell C = Game1._Map.MapList[(int)_player.MapP.X,(int)_player.MapP.Y];
            Rectangle Floor = new Rectangle(Game1.MapWidth * Game1.CellSize+3,(int)(Game1.ScreenHight*0.5),Game1.ScreenWidth,(int)(Game1.ScreenHight*0.5));
            Rectangle Cealing = new Rectangle(Game1.MapWidth * Game1.CellSize+3,0,Game1.ScreenWidth,(int)(Game1.ScreenHight*0.5));
            _spriteBatch.Draw(Game1.pixel,Floor,C.FlorOrWall);
            _spriteBatch.Draw(Game1.pixel,Cealing,C.Roof);
        }

        public void drawline(Vector2 A, Vector2 B, Color C)
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
                _spriteBatch.Draw(Game1.pixel,pen,C);
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

        private List<FloorSegment> sort_FloorSegment_list(List<FloorSegment> List){
            
            
            
            for(int i = 0; i < List.Count-1; i++){
                for(int j = 0; j < List.Count-1-i; j++){
                    if(List[j].s_Where>List[j+1].s_Where){
                        
                        FloorSegment temp = List[j];
                        List[j] = List[j+1];
                        List[j+1] = temp;
                    }
                    else if(List[j].s_Where==List[j+1].s_Where){  //för att få bort en bug där glovplattornas hörn-lodrätlije  visade fel platta, skapades av att hönen visade samma vertikala position som hörnen på plattorna bervid

                        if(List[j].s_Where < Game1.ScreenHight){
                            if(Vector2.Distance(List[j].MapP, _player.MapP) > Vector2.Distance(List[j+1].MapP, _player.MapP)){
                                List.RemoveAt(j);
                            }
                            else{
                                List.RemoveAt(j+1);
                            }
                            
                            j=0;
                            i=0;
                        }
                    }
                }
            }
            
            if(List.Count != 0){
                List.Add(new FloorSegment(0, 0, List[List.Count-1].MapP, List[List.Count-1].Precise_MapP));
            }        
            
            return new List<FloorSegment>();
        }

        
        

        
    }


}
