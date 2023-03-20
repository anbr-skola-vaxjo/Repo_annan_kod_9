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
    static public SpriteBatch _spriteBatch;
    static public Texture2D pixel;

    //vaiebles
    int k = 0;
    
    //misc
    public static int MapWidth = 40;
    public static int MapHight = 40;
    public static int IndoorMapWidth = 10;
    public static int IndoorMapHight = 10;
    
    public static int CellSize = 40;
    public static int ScreenWidth = 799;
    public static int ScreenHight = 500;
    public bool indoor = true;

    static public Map _Map;
    static public Player P1;
    static public Screen _screen;
    static public List<EntityTypes> E_types = new List<EntityTypes>();

    private Color FloorColor;
    private Color RoofColor;

    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        E_types.Add(new EntityTypes(Color.Lime,new Vector2(9,10)));
        if(indoor){
            MapHight = IndoorMapHight;
            MapWidth = IndoorMapWidth;   
        }
        _Map= new Map();
        
        FloorColor = new Color(0,50,10);
        RoofColor = Color.CadetBlue;
        _Map.MAT.Add(new Material(new Color(21,24,20),Color.CadetBlue,1f,0));
        _Map.MAT.Add(new Material(new Color(10,90,80),Color.White,1f,2));
        _Map.MAT.Add(new Material(Color.Maroon,Color.Maroon,0f,0));
        _Map.MAT.Add(new Material(new Color(100,10,50),new Color(50,50,50),1f,2));
        
        if(indoor){
            _Map.MAT[0] = new Material(new Color(21,24,20),new Color(21,24,20),1f,0);
            FloorColor = new Color(30,40,40);
            RoofColor = new Color(40,40,40);
            _Map.IndoorMapCreator();
        }
        else{
            
            _Map.OutdoorMapCreator();
        }
        
        _graphics.PreferredBackBufferWidth = MapWidth * CellSize + ScreenWidth;
        if(MapHight * CellSize >= ScreenHight){
            _graphics.PreferredBackBufferHeight = MapHight * CellSize;}
        else{
            _graphics.PreferredBackBufferHeight = ScreenHight;
        }
        
        _graphics.ApplyChanges();
        P1 = new Player(_Map.MapList);
        _screen = new Screen(P1);
        P1.SetRay(30);
        
        // TODO: Add your initialization logic here

        base.Initialize();
        
    }

    protected override void LoadContent()
    {
        pixel = Content.Load<Texture2D>("Pixel");
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        //every x frames

        if(k > 100){
            _Map.OutdoorMapCreator();
            k = 0;
            
            Random rd = new Random();
            for(int i = 0 ; i < 200 ; i++){
                int X = rd.Next(1,_Map.MapList.GetLength(0)-2);
                int Y = rd.Next(1,_Map.MapList.GetLength(1)-2);
                if(_Map.MapList[X,Y].Type == 0){
                    P1.MapP = new Vector2(X+0.5f,Y+0.5f);
                    
                }
            }
        }

        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        P1.Run();
        
        TimeSpan T = new TimeSpan(0,0,1);
        
        
        
        
        
        // TODO: Add your update logic here
        //k++;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        
        GraphicsDevice.Clear(Color.DarkSlateGray);
        _spriteBatch.Begin();
        _screen.Room(FloorColor,RoofColor);
        _Map.DRAW(P1);
        
        P1.RayCast();
        _screen.DrawEntites(_Map._Entities);

        _spriteBatch.End();
        // TODO: Add your drawing code here

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


    
