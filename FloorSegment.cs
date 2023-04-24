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
    public class FloorSegment
    {
        public int s_Where;
        public int H;
        public Vector2 MapP;
        public Vector2 Precise_MapP;
        public Color fC; // floor Color
        public Color rC; // roof Color

        public FloorSegment(int w, int h, Vector2 M, Vector2 MM){
            s_Where = w;
            H = h;
            MapP = M;
            Precise_MapP = MM;
            fC = Game1._Map.MapList[(int)(M.X),(int)(M.Y)].FlorOrWall;
            rC = Game1._Map.MapList[(int)(M.X),(int)(M.Y)].Roof;
        }
    }
}