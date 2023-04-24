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
    //graphics
    static public GraphicsDeviceManager _graphics;
    static public GraphicsDevice _graphicsdevice;
    static public SpriteBatch _spriteBatch;
    static public Texture2D pixel;
    static public Texture2D detail;
    static public Texture2D Wallpaper1;
    static public Texture2D Entety1;
    static public Texture2D Entety1_glow;
    static public Texture2D Light1; 
    static public Texture2D Kompas1; 
    static public SpriteFont font;
    


    //vaiebles
    int k = 0;
    public static double FPS = 0;
    private double FPS_median = 0;
    private int  FPS_counter = 0;
    private int  FPS_counter_max = 10;

    //misc
    public static int MapWidth = 40;
    public static int MapHight = 40;
    public static int IndoorMapWidth = 20;
    public static int IndoorMapHight = 20;
    public static int MazeMapWidth = 100;
    public static int MazeMapHight = 100;
    public int _frame = 1;
    
    public static int CellSize;

    public static int WindowWidth = 1366; //1366
    public static int WindowHigth = 768; //768

    public static int ScreenWidth; 
    public static int ScreenHight; 

    private int MapScreenWidth = 100;

    public static Color UI_color1 = new Color(5,10,10);
    public static Color UI_color2 = new Color(10,15,15);

    static public float Player_Height_offset = -0.4f;
    static public float Height_offset = Player_Height_offset;
    static public float Gravety = 0.009f;
    static public float Wall_hight = 3f;
    
    public int Location = 3;
    public bool Lights_on = true;
    Random rd = new Random();

    private int blink_counter = 0;
    private int blink_max = 100;
    private int blink_time_conter = 0;

    static public Map _Map;
    static public Player P1;
    static public Screen _screen;
    static public List<EntityTypes> E_types = new List<EntityTypes>();
    static public Maze_Perlin_noize _Perlin;
    public int First_frame = 10;
    static public bool Mouse_turn = false;
    private bool Mouse_turn_fullscreen = true;

    private Color FloorColor;
    private Color RoofColor;
    private bool isPressed = false;
    private int loading_screen_s = 150;

  

    public Kompas _kompas;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        
        //this.Window.IsBorderless = true;

        _graphics.PreferredBackBufferWidth = loading_screen_s;
        
        _graphics.PreferredBackBufferHeight = loading_screen_s;
        
        _graphics.ApplyChanges();

        
        
        // TODO: Add your initialization logic here

        base.Initialize();
        
    }

    protected override void LoadContent()
    {
        font = Content.Load<SpriteFont>("Font1");

        detail = Content.Load<Texture2D>("Tex_1");     
        Wallpaper1 = Content.Load<Texture2D>("Old_Walpaper_01");       
        Light1 = Content.Load<Texture2D>("Lampa1");
        pixel = Content.Load<Texture2D>("Pixel");
        Entety1 = Content.Load<Texture2D>("Monster_all_parts");
        Entety1_glow = Content.Load<Texture2D>("Eyes");
        Kompas1 = Content.Load<Texture2D>("Kompas");
        
        //Sentinal1
        //Old_Walpaper_01
       
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _graphicsdevice = GraphicsDevice;

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        
        if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        
        //first frame
        if(_frame == First_frame){
            
            
            CellSize = (int)((float)MapScreenWidth / (float)MapWidth);

            if(Location==2 ){
                MapHight = IndoorMapHight;
                MapWidth = IndoorMapWidth;   
            }
            else if(Location == 3){
                MapHight = MazeMapHight;
                MapWidth = MazeMapWidth;  
            }

            ScreenWidth = WindowWidth-MapWidth*CellSize; 
            ScreenHight = (int)(WindowHigth*((float)(WindowWidth-MapWidth*CellSize)/(float)WindowWidth));
            

            E_types.Add(new EntityTypes(Color.White, Entety1, Entety1_glow,new Vector2(0.6f,1f)));
            E_types.Add(new EntityTypes(Color.Red, pixel, pixel,new Vector2(0.9f,1f)));
            
            _Perlin = new Maze_Perlin_noize((int)(MapWidth/2f),(int)(MapHight/2f),CellSize);

            _Map= new Map();
            
            

            FloorColor = new Color(0,50,10);
            RoofColor = Color.CadetBlue;
            _Map.MAT.Add(new Material(new Color(21,24,20),Color.CadetBlue,1f, 1f,pixel));
            _Map.MAT.Add(new Material(new Color(10,90,80),new Color(10,70,50),1f, 1f,detail));
            _Map.MAT.Add(new Material(Color.Maroon,Color.Maroon,0f, 0f,pixel));
            _Map.MAT.Add(new Material(new Color(100,10,50),new Color(50,50,50),1f, 1f,detail));
            
            if(Location == 2 ){
                _Map.MAT[0] = new Material(new Color(21,24,20),new Color(21,24,20),1f, 1f,pixel);
                
                FloorColor = new Color(30,40,40);
                RoofColor = new Color(40,40,40);
                _Map.IndoorMapCreator();
            }
            else if(Location == 3){
                _Map.MAT[0] = new Material(new Color(16,18,15),new Color(16,18,15),1f, 1f,pixel);
                _Map.MAT[1] = new Material(new Color(110,100,70),Color.DarkGray,1f, 1f,Game1.Wallpaper1);
                FloorColor = new Color(30,40,40);
                RoofColor = new Color(40,40,40);
                _Map.Maze_Creator();
            }
            else{
                
                _Map.OutdoorMapCreator();
            }
            
            
            P1 = new Player(Keys.W , Keys.A , Keys.S , Keys.D , Keys.E , Keys.Q);
            _screen = new Screen(P1);
            P1.SetRay(30);
            

            _kompas = new Kompas(Kompas1, (int)(100*((float)(WindowWidth-MapWidth*CellSize)/(float)WindowWidth)), P1);
            _graphics.PreferredBackBufferWidth = WindowWidth;
        
            _graphics.PreferredBackBufferHeight = WindowHigth;

            
            
        
            _graphics.ApplyChanges();

            
            
            
        }
        if(_frame < First_frame*4){
                _frame++;
            }

        
        if(_frame > First_frame){

            
            if(_frame < First_frame*2){
                this.Window.Position = new Point(-10,-30);
                
            }
            

            if( Keyboard.GetState().IsKeyDown(Keys.Home) && isPressed == false){
                if(_graphics.IsFullScreen == false){//to fullscreen


                    _graphics.IsFullScreen = true;
                    IsMouseVisible = false;
                    if(Mouse_turn_fullscreen){
                        
                        Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
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
                isPressed = true;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Home) == false){
                isPressed = false;
            }
                
            P1.Run();

            

            //every x frames
            
            if(k > 5){
                
                k = 0;
                
                if(Mouse_turn == true){
                    Mouse.SetPosition((int)(WindowWidth*0.5),(int)(WindowHigth*0.5));
                }
                
            }
            k++;
        }
        TimeSpan T = new TimeSpan(0,0,1);
        
        
        
        
        
        // TODO: Add your update logic here
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        FPS += Math.Round(1f/ gameTime.ElapsedGameTime.TotalSeconds);
        if(FPS_counter >= FPS_counter_max){
            FPS_median = FPS/FPS_counter;
            FPS = 0;
            FPS_counter = 0;
        }
        
        Vector2 FPS_pos = new Vector2(MapWidth * CellSize + 10, 0);
        GraphicsDevice.Clear(UI_color1);


        _spriteBatch.Begin();
        
        if(_frame > First_frame){

            _screen.Room();
            
            
            P1.RayCast();
            _screen.DrawEntites(_Map._Entities);
            _screen.Draw_Floor_Que();
            _screen.Draw_Que();
            

            if(Lights_on == false){
                Rectangle Screen = new Rectangle(MapWidth * CellSize, 0, ScreenWidth, ScreenHight);
                Color _light = new Color(255,255,255,250);
                if(blink_counter < blink_max && blink_time_conter <= 0){
                    
                    
                    _spriteBatch.Draw(Light1,Screen,_light);

                }
                else{
                    if(blink_time_conter <= 0){
                        blink_max = rd.Next(1, 500);
                        blink_counter = 0;
                        blink_time_conter = rd.Next(0, 10);
                    }
                    else{
                        blink_time_conter--;
                    }
                    

                    _spriteBatch.Draw(pixel,Screen,new Color((byte)0,(byte)0,(byte)0,_light.A));
                    
                }
            }

            _Map.DRAW(P1);
            _kompas.DRAW();
            _spriteBatch.DrawString(font,"FPS: " + FPS_median,FPS_pos,Color.Cyan);
            


            blink_counter++;
        }
        else{
            _spriteBatch.Draw(Wallpaper1, new Rectangle(0,0,loading_screen_s,loading_screen_s), Color.White);
            _spriteBatch.Draw(Kompas1, new Rectangle(0,0,loading_screen_s,loading_screen_s), Color.White);
        }
        

        FPS_counter++;
        _spriteBatch.End();
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

    

    
}


    
