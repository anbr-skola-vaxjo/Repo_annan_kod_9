using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo_annan_kod_9;

namespace Repo_annan_kod_9;

public class Game1 : Game
{
    public static Game1 Instance => instance;

    static Game1 instance;
    //graphics
    static public GraphicsDeviceManager _graphics;
    static public GraphicsDevice _graphicsdevice;
    static public SpriteBatch _spriteBatch;
    static public Texture2D pixel;
    static public Texture2D detail;
    static public Texture2D Wallpaper1;
    static public Texture2D Door;
    static public Texture2D Door_closed;
    static public Texture2D Entety1;
    static public Texture2D Entety1_glow;
    static public Texture2D Light1; 
    static public Texture2D Kompas1; 
    static public Texture2D Key; 
    static public Texture2D Screen_ege; 
    static public Texture2D Screen_ege2; 
    static public Texture2D Game_over_screen; 
    static public Texture2D Game_over_screen_W; 
    static public Texture2D Sploosh;

    static public SpriteFont font;

    static public RenderTarget2D RT_render;

    static public RenderTarget2D RT_render_2;

    


    //vaiebles
    int k = 0;
    public static double FPS = 0;
    private double FPS_median = 0;
    private int  FPS_counter = 0;


    private int blink_counter = 0;
    private int blink_max = 100;
    private int blink_time_conter = 0;

    private float Fade_to_black = 1;


    //varieble bools
    private bool FS_isPressed = false;
    private bool Paused_isPressed = false;
    private bool Spaw_enemy = true;

    static public bool Mouse_turn = false;
    private bool Mouse_turn_fullscreen = true;

    static public bool Game_over = false;
    static public bool Escape_Game = false;
    private bool first_time_in_menue = true;
    private bool has_renderd = false;
    
    

    //misc
    
    static public int _frame = 1;
    
    public static int CellSize;
    public static int ScreenWidth; 
    public static int ScreenHight; 
    static public float Distance_to_monster = 1;

    
    Random rd = new Random();

    

    static public Map _Map;
    static public Player P1;

    static public Screen _screen;
    static public List<EntityTypes> E_types = new List<EntityTypes>();
    static public Maze_Perlin_noize _Perlin;
    public int First_frame = 10;
    public Kompas _kompas;
    private Effect shader_01;
    private Effect shader_02;

    static public string[] Info_text;
    
    private Color FloorColor;
    private Color RoofColor;
    private Color Screen_filter_color = Nutral_Screen_filter_color;
    float Shadow_shake = 0;

    static public List<Entitiy> Monsters = new List<Entitiy>();

    private List<Vector2> temp_movement_que = new List<Vector2>();

    static public Item Closest_key; 

    static public List<Item_Types> _Item_Types = new List<Item_Types>();
    static public List<Item> Keys_on_map = new List<Item>();
    private List<UI_button>[] Menue_tree = new List<UI_button>[3];
    private int Where_in_tree = 0;
    


    
    


//Starting Settings
    //word settings
        static public int Location = 2;

        public static int MapWidth = 40;
        public static int MapHight = 40;
        public static int IndoorMapWidth = 20;
        public static int IndoorMapHight = 20;
        public static int MazeMapWidth = 100;
        public static int MazeMapHight = 100;

        public static int nr_staring_keys = 30;


    //Minimap settings
        private int MapScreenWidth = 0;
    

    //Screen Settings
        public static int WindowWidth = 1366; //1366
        public static int WindowHigth = 768; //768

        public static int WindowWidth_render = 1366; //1366
        public static int WindowHigth_render = 768; //768

        static public bool HD_textures = true;
        private Color Info_text_color = Color.SlateGray;


    //spawing settings
        static public float Player_Height_offset = -0.2f;
        static public float Height_offset = Player_Height_offset;
        static public float Wall_hight = 3f;
        static public bool Paused = true;


    //screen effects
        public bool Lights_on = false; 
        public bool ScreenShadow_on = true;

        public static Color UI_color1 = new Color(5,10,10);
        public static Color UI_color2 = new Color(10,15,15);

        static private Color Nutral_Screen_filter_color = new Color(200,200,200);

        private float fade_to_black_speed = -0.01f;



    //entety
        private int Spawing_distance = 10;

    //keybindes
        public static Keys Fast_exit_key = Keys.Delete; 
        private Keys Pause_key = Keys.Escape; 
        private Keys Fullscreen = Keys.Home;


    //misc
        private int loading_screen_s = 150;
        private int  FPS_counter_max = 10;

        static public int needed_keys = 3;

        static public bool Testing_mode = false;
        static public bool Debug_info = false;




    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        instance = this;
    }

    protected override void Initialize()
    {
        
        //this.Window.IsBorderless = true;

        _graphics.PreferredBackBufferWidth = loading_screen_s;
        
        _graphics.PreferredBackBufferHeight = loading_screen_s;
        
        _graphics.ApplyChanges();
        
        this.Window.AllowAltF4 = true;
        
        // TODO: Add your initialization logic here

        base.Initialize();
        
    }

    protected override void LoadContent()
    {
        font = Content.Load<SpriteFont>("Font1");

        detail = Content.Load<Texture2D>("Tex_1");     
        Wallpaper1 = Content.Load<Texture2D>("Old_Walpaper_01");
        Door = Content.Load<Texture2D>("Door_open");      
        Door_closed = Content.Load<Texture2D>("Door3");             
        Light1 = Content.Load<Texture2D>("Lampa1");
        pixel = Content.Load<Texture2D>("Pixel");
        Entety1 = Content.Load<Texture2D>("Monster_all_parts");
        Entety1_glow = Content.Load<Texture2D>("Eyes");
        Kompas1 = Content.Load<Texture2D>("Kompas");
        Key = Content.Load<Texture2D>("Key");
        Sploosh = Content.Load<Texture2D>("Sploosh");
        Screen_ege = Content.Load<Texture2D>("Shadow");
        Screen_ege2 = Content.Load<Texture2D>("Shadow2");
        Game_over_screen = Content.Load<Texture2D>("Jumscare");
        Game_over_screen_W = Content.Load<Texture2D>("Escape2");

        shader_01 = Content.Load<Effect>("Shader_");
        shader_02 = Content.Load<Effect>("Shader_02");

        //Font1

        //Tex_1
        //Old_Walpaper_01
        //Lampa1
        //Pixel
        //Monster_all_parts
        //Eyes
        //Kompas
        //Shadow
        //Shadow2
        
       
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _graphicsdevice = GraphicsDevice;

        RT_render = new RenderTarget2D(GraphicsDevice,WindowWidth,WindowHigth);
        RT_render_2 = new RenderTarget2D(GraphicsDevice,WindowWidth_render,WindowHigth_render);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        
        if(Keyboard.GetState().IsKeyDown(Fast_exit_key))
            Exit();
        
        //first frame
        if(_frame == First_frame){
            
            IsMouseVisible = true;

            Escape_Game = false;

            if(Location==1 ){
                MapHight = IndoorMapHight;
                MapWidth = IndoorMapWidth;   
            }
            else if(Location == 2){
                MapHight = MazeMapHight;
                MapWidth = MazeMapWidth;  
            }

            _Perlin = new Maze_Perlin_noize((int)(MapWidth/2f),(int)(MapHight/2f),CellSize);

            if(has_renderd == false){
                CellSize = (int)(Math.Round((float)MapScreenWidth / (float)MapWidth));

                ScreenWidth = WindowWidth-MapWidth*CellSize; 
                ScreenHight = (int)(WindowHigth*((float)(WindowWidth-MapWidth*CellSize)/(float)WindowWidth));
                

                
                E_types.Add(new EntityTypes(Color.White, new List<Simple_texture_layer>(){new Simple_texture_layer(Entety1,false),new Simple_texture_layer(Entety1_glow,true)},new Vector2(0.6f,1f),0.02f*1.2f));
                

                _Item_Types.Add(new Item_Types(new EntityTypes(Color.White,new List<Simple_texture_layer>(){new Simple_texture_layer(Key,false)},new Vector2(0.5f,0.5f),0),true));

                
                

                _Map= new Map();
                
                

                FloorColor = new Color(0,50,10);
                RoofColor = Color.CadetBlue;
                
                Material_texture_layer layer_pixel;
                Material_texture_layer layer_wallpaper;
                Material_texture_layer Door_c;
                

                List<Material_texture_layer> ll_pixel;
                List<Material_texture_layer> ll_wall_paper;
                List<Material_texture_layer> ll_wall_paper_windoor;

            
                layer_pixel = new Material_texture_layer(pixel,false);
                layer_wallpaper = new Material_texture_layer(Wallpaper1,false);
                Door_c = new Material_texture_layer(Door_closed,false);
                

                ll_pixel = new List<Material_texture_layer>(){layer_pixel};
                ll_wall_paper = new List<Material_texture_layer>(){layer_wallpaper};
                ll_wall_paper_windoor = new List<Material_texture_layer>(){Door_c};
            
                if(Location == 1 ){
                    _Map.MAT.Add(new Material(new Color(21,24,20),new Color(21,24,20),1f, 1f,ll_pixel));
                    _Map.MAT.Add(new Material(new Color(50,100,110),Color.CadetBlue,1f, 1f,ll_wall_paper));
                    _Map.MAT.Add(new Material(Color.White,Color.DarkGray,1f, 1f,ll_wall_paper_windoor));
                    FloorColor = new Color(30,40,40);
                    RoofColor = new Color(40,40,40);
                    _Map.IndoorMapCreator();
                }
                else if(Location == 2){

                    _Map.MAT.Add(new Material(new Color(16,18,15),new Color(16,18,15),1f, 1f,ll_pixel));
                    _Map.MAT.Add(new Material(new Color(110,100,70),Color.DarkGray,1f, 1f,ll_wall_paper));
                    _Map.MAT.Add(new Material(Color.Gray,Color.DarkGray,1f, 1f,ll_wall_paper_windoor));
                    
                    FloorColor = new Color(30,40,40);
                    RoofColor = new Color(40,40,40);
                    _Map.Maze_Creator();
                }
                has_renderd = true;
            }
            else{
                if(Location == 1 ){
                    _Map.IndoorMapCreator();
                }
                if(Location == 2 ){
                    _Map.Maze_Creator();
                }
            }
            
            
            P1 = new Player(Keys.W , Keys.A , Keys.S , Keys.D , Keys.E , Keys.Q , Keys.LeftShift);
            _screen = new Screen(P1);
            P1.SetRay(30);
            

            _kompas = new Kompas(Kompas1, (int)(100*((float)(WindowWidth-MapWidth*CellSize)/(float)WindowWidth)), P1);

            for(int i = 0 ; i < Menue_tree.Length ; i++){
                Menue_tree[i] = new List<UI_button>();
            }
            Color Button_color = new Color(30,20,20);
            Color Text_color = new Color(200,20,20);
            Menue_tree[0].Add(new UI_button("Enter",new Rectangle(50,100,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[0].Add(new UI_button("Restart Game",new Rectangle(50,200,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[0].Add(new UI_button("Settings",new Rectangle(50,300,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[0].Add(new UI_button("Exit Game",new Rectangle(50,400,400,100),Sploosh,Button_color,Text_color,false));
            
            Menue_tree[1].Add(new UI_button("Back",new Rectangle(50,100,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[1].Add(new UI_button("Keybindes",new Rectangle(50,200,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[1].Add(new UI_button("Test mode",new Rectangle(50,300,400,100),Sploosh,Button_color,Text_color,true));
            Menue_tree[1].Add(new UI_button("Show Debug Info",new Rectangle(50,400,400,100),Sploosh,Button_color,Text_color,true));
            
            Menue_tree[2].Add(new UI_button("Back",new Rectangle(50,100,400,100),Sploosh,Button_color,Text_color,false));
            Menue_tree[2].Add(new Keybind_button("Sprint     ",new Rectangle(50,200,400,50),Sploosh,Button_color,Text_color, P1.Shift));
            Menue_tree[2].Add(new Keybind_button("Forward    ",new Rectangle(50,250,400,50),Sploosh,Button_color,Text_color, P1.W));
            Menue_tree[2].Add(new Keybind_button("Back       ",new Rectangle(50,300,400,50),Sploosh,Button_color,Text_color, P1.S));
            Menue_tree[2].Add(new Keybind_button("Left       ",new Rectangle(50,350,400,50),Sploosh,Button_color,Text_color, P1.A));
            Menue_tree[2].Add(new Keybind_button("Right      ",new Rectangle(50,400,400,50),Sploosh,Button_color,Text_color, P1.D));
            Menue_tree[2].Add(new Keybind_button("Turn Left  ",new Rectangle(50,450,400,50),Sploosh,Button_color,Text_color, P1.Q));
            Menue_tree[2].Add(new Keybind_button("Turn Right ",new Rectangle(50,500,400,50),Sploosh,Button_color,Text_color, P1.E));

            Menue_tree[1][2].is_preesed = Testing_mode;
            Menue_tree[1][3].is_preesed = Debug_info;

            Info_text = new string[7];

            for(int i = 0 ; i < Info_text.Length ; i++){
                Info_text[i] = "-";
            }
            Info_text[0] = "FPS";
            Info_text[1] = "No monster";
            Info_text[2] = "Distance: na";
            Info_text[3] = "Entety nr: " + _Map._Entities.Count;
            Info_text[4] = "Entetys Drawn: 0";
            Info_text[5] = "stammina: ";
            Info_text[6] = "Keys: 0";


            Spaw_enemy = true;

            _graphics.PreferredBackBufferWidth = WindowWidth;
        
            _graphics.PreferredBackBufferHeight = WindowHigth;

            
            _graphics.ApplyChanges();

            
            
            
        }
        if(_frame < First_frame*4){
                _frame++;
            }

        
        if(_frame > First_frame){
            if(Game_over){
                
                Paused = true;
            }
            
            if(_frame < First_frame*2){
                this.Window.Position = new Point(-10,-30);
                    
            }
                

            if( Keyboard.GetState().IsKeyDown(Fullscreen) && FS_isPressed == false){
                if(_graphics.IsFullScreen == false){//to fullscreen


                    _graphics.IsFullScreen = true;
                    
                    
                    if(Mouse_turn_fullscreen){
                        if(Paused == false){
                            IsMouseVisible = false;
                            Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
                        }
                        Mouse_turn = true;
                    }
                }
                else{//to samall
                        
                    _graphics.IsFullScreen = false;
                        
                    IsMouseVisible = true;
                    if(Mouse_turn_fullscreen){
                        Mouse_turn = false;
                    }
                }
                    
                    
                _graphics.ApplyChanges();
                FS_isPressed = true;
            }


            if(Keyboard.GetState().IsKeyDown(Fullscreen) == false){
                FS_isPressed = false;
            }

            if(Game_over == false){
                if( Keyboard.GetState().IsKeyDown(Pause_key) && Paused_isPressed == false && Where_in_tree == 0){
                    //Pause
                    bool MT = Mouse_turn;
                    Where_in_tree = 0;
                    if(first_time_in_menue){
                        Menue_tree[0][0].Text = "Resume";
                    }
                    if(Paused == false){
                        IsMouseVisible = true;
                        
                        Paused = true;
                    }
                    else{
                        Paused = false;
                        if(Mouse_turn){
                            IsMouseVisible = false;
                            Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
                        }
                    }
                    Paused_isPressed = true;
                    if(Testing_mode){
                        Game1.Info_text[5] = "stammina: na" ;
                    }
                }


                if(Keyboard.GetState().IsKeyDown(Pause_key) == false){
                    Paused_isPressed = false;
                }
            }



            if(Paused == false){  

                P1.Run();
                if(Game_over){
                    base.Update(gameTime);
                    return;
                }

                if(Location == 2 && Spaw_enemy == true){
                    if( Vector2.Distance(P1.Staring_MapP, P1.MapP) > Spawing_distance){
                        Entitiy Monster = new Entitiy(E_types[0],P1.Staring_MapP);
                        Monster.Movement_que.AddRange(temp_movement_que);
                        _Map._Entities.Add(Monster); 
                        Spaw_enemy = false;
                        temp_movement_que.Clear();
                        Game1.Info_text[1] =  "Serching";
                        Game1.Info_text[2] =  "Distance: " + temp_movement_que.Count();
                        foreach(Entitiy E in _Map._Entities){
                            if(E.Type == E_types[0] && Monsters.Contains(E) == false){
                                Monsters.Add(E);
                            }
                        }
                        
                    }
                    else{
                        if(temp_movement_que.Contains(new Vector2((int)(P1.MapP.X),(int)(P1.MapP.Y))) == false){
                            temp_movement_que.Add(new Vector2((int)(P1.MapP.X),(int)(P1.MapP.Y)));
                        }
                    }
                }
                
                

                foreach(Entitiy E in Monsters){
                    E.run(1);
                }
                for(int i = 0 ; i < Keys_on_map.Count ; i++){
                    Keys_on_map[i].run();
                    
                }

                Shadow_shake = 0;
                Screen_filter_color = Nutral_Screen_filter_color;
                if(Monsters.Count > 0 && Vector2.Distance(Monsters[0].MapP, P1.MapP) < Spawing_distance){
                    
                    float D = Vector2.Distance(Monsters[0].MapP, P1.MapP);

                    if(D < Spawing_distance*0.5){
                        float c = (D/((float)Spawing_distance*0.5f));

                        
                        Screen_filter_color = Screen_filter_color * c;
                        Screen_filter_color.R = (byte)(Nutral_Screen_filter_color.R + (150 - Nutral_Screen_filter_color.R)*(1-c));
                        
                        
                    }

                    Distance_to_monster = (D/((float)Spawing_distance));;
                    Shadow_shake = 1 - (D/((float)Spawing_distance));
                    
                }


                
                //every x frames
                if(k > 5){
                    
                    k = 0;
                    Info_text[3] = "Entety nr: " + _Map._Entities.Count;
                    if(Mouse_turn == true){
                        Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
                        
                    }
                    if(Keys_on_map.Count > 0){
                        Item Closest = Keys_on_map[0];
                        foreach(Item I in Keys_on_map){
                            if(I != null){
                                if(Vector2.Distance(P1.MapP,I.MapP) < Vector2.Distance(P1.MapP,Closest.MapP)){
                                    Closest = I;
                                }
                            }
                        }
                        Closest_key = Closest;
                    }
                    
                }
                k++;
                
            }
            else{
                int Where_in_tree_save = Where_in_tree;
                foreach(List<UI_button> list in Menue_tree){
                    foreach(UI_button button in list){
                        button.Reset();
                    }
                }
                foreach(UI_button button in Menue_tree[Where_in_tree]){
                    button._run();
                    button.Run();
                    
                    
                }

                
                
                if(Menue_tree[0][0].is_preesed){
                    if(Mouse_turn){
                        IsMouseVisible = false;
                        Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
                    }
                    Paused = false;
                }
                if(Menue_tree[0][1].is_preesed){
                    Reset_game();
                    
                }
                if(Menue_tree[0][2].is_preesed){
                    Where_in_tree = 1;
                }
                if(Menue_tree[0][3].is_preesed){
                    Exit();
                }
                if(Menue_tree[1][0].is_preesed){
                    Where_in_tree = 0;
                }
                if(Menue_tree[1][1].is_preesed){
                    Where_in_tree = 2;
                }
                if(Menue_tree[2][0].is_preesed){
                    Where_in_tree = 1;
                }
                if(Menue_tree[2][1].is_preesed){
                    P1.Shift = (Keys)Menue_tree[2][1]._Var;
                }
                if(Menue_tree[2][2].is_preesed){
                    P1.W = (Keys)Menue_tree[2][2]._Var;
                }
                if(Menue_tree[2][3].is_preesed){
                    P1.S = (Keys)Menue_tree[2][3]._Var;
                }
                if(Menue_tree[2][4].is_preesed){
                    P1.A = (Keys)Menue_tree[2][4]._Var;
                }
                if(Menue_tree[2][5].is_preesed){
                    P1.D = (Keys)Menue_tree[2][5]._Var;
                }
                if(Menue_tree[2][6].is_preesed){
                    P1.Q = (Keys)Menue_tree[2][6]._Var;
                }
                if(Menue_tree[2][7].is_preesed){
                    P1.E = (Keys)Menue_tree[2][7]._Var;
                }
                
                
                
                
                Testing_mode = Menue_tree[1][2].is_preesed;
                Debug_info = Menue_tree[1][3].is_preesed;
                

                if(Where_in_tree_save != Where_in_tree){
                    foreach(List<UI_button> list in Menue_tree){
                        foreach(UI_button button in list){
                            button.mouse_is_pressed = true;
                        }
                    }
                }
                
            }
        }
        TimeSpan T = new TimeSpan(0,0,1);
        
        
        
        
        
        // TODO: Add your update logic here
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        
        
        GraphicsDevice.Clear(UI_color1);
        
        if(_frame > First_frame){
            
            if(Paused == false && Game_over == false){

                FPS += Math.Round(1f/ gameTime.ElapsedGameTime.TotalSeconds);
                if(FPS_counter >= FPS_counter_max){
                    FPS_median = FPS/FPS_counter;
                    FPS = 0;
                    FPS_counter = 0;
                    if(Debug_info){
                        Info_text[0] = "FPS: " + FPS_median;
                        
                    }
                }
                
                
                
                FPS_counter++;

                GraphicsDevice.SetRenderTarget(RT_render);
                _spriteBatch.Begin();

                _screen.Horizon();
                
                
                P1.RayCast();
                _screen.Add_Enteties_to_que(_Map._Entities);
                _screen.Draw_Floor_Que();
                _screen.Draw_Wall_Que();
                
                Rectangle Screen = new Rectangle(MapWidth * CellSize, 0, ScreenWidth, ScreenHight);
                Color _light = new Color(255,255,255,170);
                int b = rd.Next(0,(int)(100f*Shadow_shake)+10);
                
                
                if(Lights_on == false){
                    
                    
                    if(blink_counter < blink_max && blink_time_conter <= 0){                    
                        
                        _spriteBatch.Draw(Light1,Screen,_light);

                    }
                    else{
                        if(blink_time_conter <= 0){
                            int btc_m = 20;
                            int BM_max = 500;
                            int BM = BM_max - (int)((BM_max+300)*(Shadow_shake));
                            if(BM < btc_m){
                                BM = btc_m;
                            }
                            blink_max = rd.Next(1, BM);
                            blink_counter = 0;
                            blink_time_conter = rd.Next(0, 1 + (btc_m - 1)*(int)(1-Shadow_shake));
                        }
                        else{
                            blink_time_conter--;
                        }
                        

                        _spriteBatch.Draw(pixel,Screen,new Color((byte)0,(byte)0,(byte)0,_light.A));
                        
                    }
                }
                if(ScreenShadow_on){
                    Screen.Height+=b+10;
                    Screen.Y -= (int)(b*0.5f);
                    
                    _spriteBatch.Draw(Screen_ege,Screen,Color.Black);
                    _spriteBatch.Draw(Screen_ege,Screen,Color.Black);
                }

                _Map.DRAW(P1);
                _kompas.DRAW();
                
                


                blink_counter++;
                    
                _spriteBatch.End();


                GraphicsDevice.SetRenderTarget(RT_render_2);



                _spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,shader_01);
                
                Color info = new Color(Distance_to_monster,0,0);
                if(Game_over){
                    //kanske onödig
                    info = new Color(100,0,0);
                }
                _spriteBatch.Draw(RT_render, new Rectangle(0,0,WindowWidth,WindowHigth),info);
                
            
                _spriteBatch.End();
                

                GraphicsDevice.SetRenderTarget(null);
                
                _spriteBatch.Begin();
                _spriteBatch.Draw(RT_render_2, new Rectangle(0,0,WindowWidth_render,WindowHigth_render), Nutral_Screen_filter_color);
                if(_frame > First_frame){
                    if(Debug_info){
                        int i = 0;
                        foreach(string inf in Info_text){
                            Vector2 FPS_pos = new Vector2(MapWidth * CellSize + 10, 14*i);
                            _spriteBatch.DrawString(font,inf,FPS_pos,Info_text_color);
                            i++;
                        }
                    }
                }
                _spriteBatch.End();
            }
            else{
                GraphicsDevice.Clear(UI_color1);
                Rectangle Screen = new Rectangle(0,0,WindowWidth_render,WindowHigth_render);
                Color ScreenC = Nutral_Screen_filter_color;
                if(Game_over){
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(pixel,Screen,Color.Black);
                    _spriteBatch.End();
                    
                }

                _spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,shader_02);
                
                if(Game_over){  
                    
                    float fb = Fade_to_black;
                    ScreenC = new Color(fb,fb,fb);
                    Fade_to_black+=fade_to_black_speed;
                    if(fb>0){
                        
                        if(Escape_Game ){
                            
                            _spriteBatch.Draw(Game_over_screen_W,Screen, ScreenC);
                        }
                        else{
                            _spriteBatch.Draw(Game_over_screen,Screen, ScreenC);
                        }
                    }
                    else if(Fade_to_black < 0){
                        
                        _spriteBatch.End();
                        base.Draw(gameTime);
                        Reset_game();

                        
                        return;
                    }
                }
                else{
                    _spriteBatch.Draw(RT_render_2, Screen, Nutral_Screen_filter_color);
                }
                
                _spriteBatch.End();

                if( Game_over == false){
                    _spriteBatch.Begin();
                    foreach(UI_button button in Menue_tree[Where_in_tree]){
                        button.Draw();
                    }
                    _spriteBatch.End();
                }

            }
            
        
            
        }
        else if(has_renderd == false){

            
            _spriteBatch.Begin();
            _spriteBatch.Draw(Wallpaper1, new Rectangle(0,0,loading_screen_s,loading_screen_s), Nutral_Screen_filter_color);
            _spriteBatch.Draw(Kompas1, new Rectangle(0,0,loading_screen_s,loading_screen_s), Nutral_Screen_filter_color);
            _spriteBatch.End();

        }
        
        base.Draw(gameTime);
    }


    static public float Vector2Angle(Vector2 A, Vector2 B, Vector2 C){
        Vector2 origo = new Vector2(0,0);
        
        
        // A = arccos( b/2c + c/2b - a^2/2bc  )
        float a = Vector2.Distance(B,C);
        float b = Vector2.Distance(A,C);
        float c = Vector2.Distance(B,A);
    
        double AV = Math.Acos(b/(2*c) + c/(2*b) - (a*a)/(2*b*c))*180/Math.PI;    
        

        return (float)AV;
    }

    static public float MultiplyVector(Vector2[] vectors){
        float x = 1;
        float y = 1;
        foreach(Vector2 Vector in vectors){
            x*= Vector.X;
            y*= Vector.Y;
        }
        
        
        return x+y;
    }

    public void Reset_game(){
        Game_over = false;
        Paused = true;
        _frame = 1;
        Distance_to_monster = 1;
        RT_render_2 = new RenderTarget2D(GraphicsDevice,WindowWidth_render,WindowHigth_render);
        _Map._Entities.Clear();
        Monsters.Clear();
        Fade_to_black = 1;
    }
    
     

    
}




    
