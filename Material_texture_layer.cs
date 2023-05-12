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
    public class Material_texture_layer
    {
        public Texture2D _tex;
        public Texture2D[] _divided_tex;
        public bool Glow;

        public Material_texture_layer(Texture2D T, bool G){
            _tex = T;
            Glow = G;
            
            if(Game1.HD_textures){
                _divided_tex = Divide_texture2D(_tex);
            }
            else{
                _divided_tex = new Texture2D[1]{_tex};
            }
        }

        

        private Texture2D[] Divide_texture2D(Texture2D T){
            Console.WriteLine("loading: ''" +  T.Name + "''");
            
            
            int Tex_Height = T.Height;
            int Tex_Width = T.Width;
            Texture2D[] T_list = new Texture2D[Tex_Width];

            if(Tex_Width > 1){
            
                Color[] pixel_list = new Color[Tex_Width*Tex_Height];
                for(int i = 0 ; i < Tex_Width ; i++){
                    
                    T.GetData<Color>(pixel_list);
                    
                    T_list[i] = new Texture2D(Game1._graphicsdevice,1,Tex_Height);
                    
                    Color[] pixel_columm = new Color[Tex_Height];
                    
                    for(int j = 0 ; j < Tex_Height ; j++){
                        pixel_columm[j] =  pixel_list[j*Tex_Width+i];
                    }
                    T_list[i].SetData<Color>(pixel_columm);
                }
            
            }
            else{
                T_list[0] = T;
            }
            Console.WriteLine("Texture: ''" +  T.Name + "'' has loaded");

            

            return T_list;
        }


        
    }
}