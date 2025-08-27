using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
                                                                                                                                                                                                                                                                                                                                                                                                                              
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

        public int Depth { get; set; }//0-29
        public int EColor { get; set; }//0-3
        public Color Index { get; set; }//a:0?1,r,g,b
        public int IndexID { get; set; }//a:0?1,r,g,b
        public int LColor { get; set; }//0-2
        public int Light { get; set; }//0?1
        public int Pipe { get; set; }//0-3
        public int Grime { get; set; }//0?1
        public int Shading { get; set; }//0-255
        public int Sky { get; set; }//0?1
        //public List<Color> IndexPalette { get; set; }

        public ColorPalette IndexPalette { get; set; }
        public CursorColors()
        {
            //IndexPalette = new List<Color>(256);
        }

        public Color ColorDepth()
        {
            int val = (int)(Depth * 8.8);
            return Color.FromArgb(val, val, val);
        }
        public Color ColorEColor()
        {
            if(EColor==EffectColorA)
            {
                return Color.FromArgb(255, 0, 255);
            }
            else if (EColor == EffectColorB)
            {
                return Color.FromArgb(0, 255, 255);
            }
            else if (EColor == EffectColorC)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public Color ColorLColor()
        {
            if (LColor == GeometryDark)
            {
                return Color.FromArgb(255, 0, 0);
            }
            else if (LColor == GeometryNeutral)
            {
                return Color.FromArgb(0, 255, 0);
            }
            else if (LColor == GeometryLight)
            {
                return Color.FromArgb(0, 0, 255);
            }
            return Color.FromArgb(255, 0, 0);
        }
        public Color ColorLight()
        {
            if (Light == LightOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public Color ColorPipe()
        {
            if (Pipe == PipeL1)
            {
                return Color.FromArgb(255, 0, 0);
            }
            else if (Pipe == PipeL2)
            {
                return Color.FromArgb(0, 255, 0);
            }
            else if (Pipe == PipeL3)
            {
                return Color.FromArgb(0, 0, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public Color ColorGrime()
        {
            if (Grime == GrimeOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public Color ColorShading()
        {
            return Color.FromArgb(Shading, Shading, Shading);
        }
        public Color ColorSky()
        {
            if (Sky == SkyOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ColorRendered(int tDepth, Color tIndex, int tEColor, int tLColor, int tLight, int tPipe, int tGrime, int tShading, int tSky)
        {
            int valRed = 0;
            int valGreen = 0;
            int valBlue = 0;
            int useIndex = tIndex.A == 255 ? 1 : 0;
            
            if (tSky==1)
                return Color.FromArgb(255, 255, 255);
            else
            {
                if (useIndex==1)
                {
                    //find color from list
                    valBlue = 255;//for now just say it's the 1st color. Come back to finish this code later
                }
                else
                {
                    valBlue = tShading;
                }
                valRed = 1+tDepth + tLColor * 30 + tLight * 90;
                valGreen = tEColor + tGrime * 4 + useIndex * 8 + tLight * 16;
                return Color.FromArgb(valRed, valGreen, valBlue);
            }
        }

        public struct Features
        {
            public Features(int tDepth, Color tIndex, int tEColor, int tLColor, int tLight, int tPipe, int tGrime, int tShading, int tSky)
            {
                ThisDepth = tDepth;
                ThisIndex = tIndex;
                ThisEColor = tEColor;
                ThisLColor = tLColor;
                ThisLight = tLight;
                ThisPipe = tPipe;
                ThisGrime = tGrime;
                ThisShading = tShading;
                ThisSky = tSky;
            }

            public int ThisDepth { get; }
            public Color ThisIndex { get; }
            public int ThisEColor { get; }
            public int ThisLColor { get; }
            public int ThisLight { get; }
            public int ThisPipe { get; }
            public int ThisGrime { get; }
            public int ThisShading { get; }
            public int ThisSky { get; }

            public override string ToString() => $"({ThisDepth}, {ThisIndex}, {ThisEColor}, {ThisLColor}, {ThisLight}, {ThisPipe}, {ThisGrime}, {ThisShading}, {ThisSky})";
        }

        public int IndexColorID(Color colInput)
        {
            //return IndexPalette.FindIndex(a => a == colInput);
            return Array.FindIndex(IndexPalette.Entries, a => a == colInput);
        }

        public void AddIndexColor(Color colInput, int slot = -1)
        {
            int id = IndexColorID(colInput);
            if ( id < 0)
            {
                if (slot == -1 || slot >255)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        if (IndexPalette.Entries[i] == null)
                        {
                            IndexPalette.Entries[i] = colInput;
                            break;
                        }
                    }
                }
                else
                {
                    IndexPalette.Entries[slot] = colInput;
                }
            }
        }

        public void RemoveIndexColor( int slot)
        {
            if (slot >= 0 && slot <=255 )
            {
                IndexPalette.Entries[slot] = Color.Transparent;
            }
        }

        public void ReplaceIndexColor(Bitmap image,int initial, int final)
        {
            if (initial >= 0 && initial <= 255 && final >= 0 && final <= 255 && image != null)
            {
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        if (GetPixelIndexedBitmap(image,j, i) != initial)
                        {
                            continue;
                        }
                        


                        Color colPixel = image.GetPixel(j, i);
                        int R = colPixel.R;
                        int G = colPixel.G;
                        int B = colPixel.B;

                    }
                }
            }
        }


        public static unsafe Bitmap SetPixelIndexedBitmap(Bitmap bitmap, int slot,int x, int y)
        {
            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte* line = (byte*)data.Scan0;
            for (int i = 0; i < y+1; i++)// scanning through the lines
            {
                line += data.Stride;
            }
            line[x] = (byte)slot;
            bitmap.UnlockBits(data);
            return bitmap;
        }

        public static unsafe void ChangeColorIndexedBitmap(Bitmap bitmap, int from, int to)
        {
            if (from == to)
                return;
            BitmapData data = bitmap.LockBits( new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte* line = (byte*)data.Scan0;
            for (int y = 0; y < data.Height; y++)// scanning through the lines
            {
                for (int x = 0; x < data.Width; x++)// scanning through the pixels within the line
                {
                    if (line[x] == from)
                    line[x] = (byte)to;
                }
                line += data.Stride;
            }
            bitmap.UnlockBits(data);
        }

        public static int GetPixelIndexedBitmap(Bitmap bmp, int x, int y)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            byte[] pixels = new byte[bmpData.Stride * bmp.Height];
            System.Runtime.InteropServices.Marshal.Copy(ptr, pixels, 0, pixels.Length);
            int idx = pixels[y * bmpData.Stride + x];
            bmp.UnlockBits(bmpData);
            return idx;
        }

        public static Features FeaturesRendered(Color tRendered)
        {
            
            int R = tRendered.R;
            int G = tRendered.G;
            int B = tRendered.B;
            int tDepth; Color tIndex; int tEColor; int tLColor; int tLight; int tPipe; int tGrime; int tShading; int tSky;

            if (R == 255 && G == 255 && B == 255)//if sky
            {
                tDepth = 30;
                tIndex = Color.Transparent;
                tEColor = 0;
                tLColor = 0; 
                tLight = 0; 
                tPipe = 0; 
                tGrime=0; 
                tShading=0;
                tSky = 1;
            }
            else//not sky
            {
                if (R > 180)
                    R = 150;
                else if (R < 1)
                    R = 1;
                if (G > 31)
                    G = 0;
                tSky = 0;
                

                tLight = R > 90 ? 1 : 0;// 0 or 1
                R = R - 90 * tLight;
                tLColor = Math.Min(Math.Max((R - 1) / 30, 0), 2);//0-2
                R = R - tLColor * 30-1;
                tDepth = R;//(int)(R * 8.8);//0-29
                tPipe = (G == 8 ? 1 : 0 + G == 9 ? 2 : 0 + G == 10 ? 3 : 0) * B == 0 ? 1 : 0;//0-3 the B==0 is for of index colors, change this later to only index
                
                G = G % 16; //0-15
                int HasIndex = Math.Min(G / 8, 1); //0 or 1
                HasIndex = HasIndex * (B == 0 ? 1 : 0);
                G = G % 8; //0-7
                tGrime = G / 4;//0 or 1
                tEColor = G % 4;//0-3
                tShading = (1 - HasIndex) * (tEColor > 0 ? 1 : 0) * B; //0-255
                tIndex = Color.Transparent;//for now no index support
            }//end not sky
            var output = new Features(tDepth, tIndex, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
            return output;
        }


    }
}
