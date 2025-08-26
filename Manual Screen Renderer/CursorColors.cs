using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
                                                                                                                                                                                                                                                                                                                                                                                                                              
namespace Manual_Screen_Renderer
{
    class CursorColors
    {
        public static int NoEffectColor = 0;
        public static int EffectColorA = 1;
        public static int EffectColorB = 2;
        public static int EffectColorC = 3;
        public static int GeometryDark = 0;
        public static int GeometryNeutral = 1;
        public static int GeometryLight = 2;
        public static int LightOff = 0;
        public static int LightOn = 1;
        public static int NoPipe = 0;
        public static int PipeL1 = 1;
        public static int PipeL2 = 2;
        public static int PipeL3 = 3;
        public static int GrimeOff = 0;
        public static int GrimeOn = 1;
        public static int SkyOff = 0;
        public static int SkyOn = 1;

        public int depth { get; set; }
        public int EColor { get; set; }
        public Color index { get; set; }
        public int LColor { get; set; }

        public int Light { get; set; }
        public int Pipe { get; set; }
        public int Grime { get; set; }
        public int Shading { get; set; }
        public int Sky { get; set; }


        public CursorColors()
        {
            
        }
    }
}
