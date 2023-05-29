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
    public class Item_Types
    {
        public readonly EntityTypes E_type;
        public readonly bool Colecible;

        public Item_Types(EntityTypes E, bool C){
            E_type = E;
            Colecible = C;
        }

        
    }
}