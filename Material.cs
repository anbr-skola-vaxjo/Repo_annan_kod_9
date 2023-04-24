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
    public class Material
    {
        readonly public  Color FlorOrWall = Color.Magenta;
        readonly public Color Roof = Color.Azure;
        readonly public Texture2D _Tex;
        readonly public Texture2D[] Tex_list;
        readonly public float Variation = 1.0f;
        readonly public float Softnes = 1.0f;

        
        
        
        public Material(Color F, Color R, float S, float V, Texture2D T){
            Console.WriteLine("loading: ''" +  T.Name + "''");
            FlorOrWall = F;
            Roof = R;
            Softnes = S;
            Variation = V;
            _Tex = T;

            int Tex_Height = _Tex.Height;
            int Tex_Width = _Tex.Width;

            Tex_list = new Texture2D[Tex_Width];
            if(Tex_Width != 1){
                Color[] pixel_list = new Color[Tex_Width*Tex_Height];
                for(int i = 0 ; i < Tex_Width ; i++){
                    
                    _Tex.GetData<Color>(pixel_list);
                    
                    Tex_list[i] = new Texture2D(Game1._graphicsdevice,1,Tex_Height);
                    Color[] pixel_columm = new Color[Tex_Height];
                    for(int j = 0 ; j < Tex_Height ; j++){
                        pixel_columm[j] =  pixel_list[j*Tex_Width+i];
                    }
                    Tex_list[i].SetData<Color>(pixel_columm);
                }
                
            }
            Console.WriteLine("Texture: ''" +  _Tex.Name + "'' has loaded");

        }
    }
}