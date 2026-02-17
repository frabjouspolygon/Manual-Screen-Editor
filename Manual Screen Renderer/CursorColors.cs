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
        public static int NoEffectColorD = 4;
        public static int EffectColorAD = 5;
        public static int EffectColorBD = 6;
        public static int EffectColorCD = 7;
        public static int GeometryDark = 0;
        public static int GeometryNeutral = 1;
        public static int GeometryLight = 2;
        public static int LightOff = 0;
        public static int LightOn = 1;
        public static int NoPipe = 0;
        public static int PipeL1 = 1;
        public static int PipeL2 = 3;
        public static int PipeL3 = 2;
        public static int GrimeOff = 0;
        public static int GrimeOn = 1;
        public static int SkyOff = 0;
        public static int SkyOn = 1;

        public static Dictionary<int, string> dictLblEColor = new Dictionary<int, string>()
        {
            {0, "Effect Color Off"},
            {1, "Effect Color A"},
            {2, "Effect Color B"},
            {3, "Effect Color Batfly Hive"},
            {4, "Effect Color Off Dark"},
            {5, "Effect Color A Dark"},
            {6, "Effect Color B Dark"},
            {7, "Effect Color Batfly Hive Dark"}
        };
        public static Dictionary<int, string> dictLblLColor = new Dictionary<int, string>()
        {
            {0, "Dark"},
            {1, "Neutral"},
            {2, "Light"}
        };
        public static Dictionary<int, string> dictLblLight = new Dictionary<int, string>()
        {
            {0, "Shadow"},
            {1, "Sunlight"}
        };
        public static Dictionary<int, string> dictLblPipe = new Dictionary<int, string>()
        {
            {0, "No Shortcuts"},
            {1, "Shortcut Layer 1"},
            {2, "Shortcut Layer 2"},
            {3, "Shortcut Layer 3"}
        };
        public static Dictionary<int, string> dictLblGrime = new Dictionary<int, string>()
        {
            {0, "Grime Off"},
            {1, "Grime On"}
        };
        public static Dictionary<int, string> dictLblSky = new Dictionary<int, string>()
        {
            {0, "Geometry"},
            {1, "Sky"}
        };
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
        public double GrimeAlpha { get; set; }
        public bool AllowDarkE {  get; set; }
        public struct ECol
        {
            public ECol(WeatherECol dry, WeatherECol wet)
            {Wet = wet;Dry = dry;}
            public WeatherECol Wet { get; }
            public WeatherECol Dry { get; }
            public override bool Equals(Object obj)
            {return obj is ECol && Equals((ECol)obj);}
            public bool Equals(ECol other)
            {return Wet == other.Wet && Dry == other.Dry;}
            public static bool operator ==(ECol lhs, ECol rhs)
            {return lhs.Equals(rhs);}
            public static bool operator !=(ECol lhs, ECol rhs)
            {return !lhs.Equals(rhs);}
            public override string ToString() => $"({Wet}, {Dry})";
            public struct WeatherECol
            {
                public WeatherECol(ShadeECol sun, ShadeECol shadow)
                {Sun = sun;Shadow = shadow;}
                public ShadeECol Sun { get; }
                public ShadeECol Shadow { get; }
                public override bool Equals(Object obj)
                {return obj is WeatherECol && Equals((WeatherECol)obj);}
                public bool Equals(WeatherECol other)
                {return Sun == other.Sun && Shadow == other.Shadow;}
                public static bool operator ==(WeatherECol lhs, WeatherECol rhs)
                {return lhs.Equals(rhs);}
                public static bool operator !=(WeatherECol lhs, WeatherECol rhs)
                {return !lhs.Equals(rhs);}
                public override string ToString() => $"({Sun}, {Shadow})";
                public struct ShadeECol
                {
                    public ShadeECol(Color light, Color dark)
                    {
                        Light = light;
                        Dark = dark;
                    }

                    public Color Light { get; }
                    public Color Dark { get; }

                    public override bool Equals(Object obj)
                    {
                        return obj is ShadeECol && Equals((ShadeECol)obj);
                    }
                    public bool Equals(ShadeECol other)
                    {
                        return Light == other.Light && Dark == other.Dark;
                    }

                    public static bool operator ==(ShadeECol lhs, ShadeECol rhs)
                    {
                        return lhs.Equals(rhs);
                    }

                    public static bool operator !=(ShadeECol lhs, ShadeECol rhs)
                    {
                        return !lhs.Equals(rhs);
                    }
                    public override string ToString() => $"({Light}, {Dark})";
                }
            }
        }


        public List<ECol> BaseECols { get; set; }
        //public List<Color> IndexPalette { get; set; }

        public List<List<CursorColors.BufferAction>> undoBuffer = new List<List<CursorColors.BufferAction>>();
        public List<List<CursorColors.BufferAction>> redoBuffer = new List<List<CursorColors.BufferAction>>();
        public List<CursorColors.BufferAction> workingBuffer = new List<CursorColors.BufferAction>();
        public Bitmap imgPalette { get; set; }
        public Bitmap imgGrimeMask { get; set; }
        public Color colA { get; set; }
        public Color colB { get; set; }

        public int icolA { get; set; }
        public int icolB { get; set; }

        public bool Rain { get; set; }

        public ColorPalette IndexPalette { get; set; }
        public int PenSize { get; set; }
        public int PenAlpha { get; set; }
        public CursorColors()
        {
            //IndexPalette = new List<Color>(256);
            imgPalette = new Bitmap(Properties.Resources.palette0);//Form1.SolidBitmap(32, 16, Color.FromArgb(0, 0, 0));
            imgGrimeMask = new Bitmap(Properties.Resources.GrimeMask);
            colA = Color.FromArgb(255, 0, 255);
            colB = Color.FromArgb(0, 255, 255);
            icolA = 0;
            icolB = 0;
            BaseECols = new List<ECol>();
            PopulateBaseECols();
            PenSize = 1;
            PenAlpha = 255;
            Rain = false;
            AllowDarkE = false;
            GrimeAlpha = 1.0;
        }

        public void PopulateBaseECols()
        {
            Bitmap cols = new Bitmap(Properties.Resources.effectcolors);
            for (int i = 0;i<22; i++)
            {
                BaseECols.Add(new ECol(
                    new ECol.WeatherECol(new ECol.WeatherECol.ShadeECol(cols.GetPixel(2*i,0), cols.GetPixel(2*i, 1)), new ECol.WeatherECol.ShadeECol(cols.GetPixel(2*i+1, 0), cols.GetPixel(2*i+1, 1))),
                    new ECol.WeatherECol(new ECol.WeatherECol.ShadeECol(cols.GetPixel(2*i, 2), cols.GetPixel(2*i, 3)), new ECol.WeatherECol.ShadeECol(cols.GetPixel(2*i+1,2), cols.GetPixel(2*i+1,3)))));
            }
        }

        public static Color ToDepth(int tDepth)
        {
            int val = (int)(tDepth * 8.8);
            return Color.FromArgb(val, val, val);
        }

        public static Color ToEColor(int tEColor)
        {
            if (tEColor == NoEffectColor)
            {
                return Color.FromArgb(0, 0, 0);
            }
            else if(tEColor == EffectColorA)
            {
                return Color.FromArgb(255, 0, 255);
            }
            else if (tEColor == EffectColorB)
            {
                return Color.FromArgb(0, 255, 255);
            }
            else if (tEColor == EffectColorC)
            {
                return Color.FromArgb(255, 255, 255);
            }
            else if (tEColor == NoEffectColorD)
            {
                return Color.FromArgb(0, 0, 0); //return Color.FromArgb(50, 50, 50);
            }
            else if (tEColor == EffectColorAD)
            {
                return Color.FromArgb(255, 0, 255); //return Color.FromArgb(150, 0, 150);
            }
            else if (tEColor == EffectColorBD)
            {
                return Color.FromArgb(0, 255, 255); //return Color.FromArgb(0, 150, 150);
            }
            else if (tEColor == EffectColorCD)
            {
                return Color.FromArgb(255, 255, 255); //return Color.FromArgb(150, 150, 150);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ToLColor(int tLColor)
        {
            if (tLColor == GeometryDark)
            {
                return Color.FromArgb(255, 0, 0);
            }
            else if (tLColor == GeometryNeutral)
            {
                return Color.FromArgb(0, 255, 0);
            }
            else if (tLColor == GeometryLight)
            {
                return Color.FromArgb(0, 0, 255);
            }
            return Color.FromArgb(255, 0, 0);
        }
        public static Color ToLight(int tLight)
        {
            if (tLight == LightOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ToPipe(int tPipe)
        {
            if (tPipe == PipeL1)
            {
                return Color.FromArgb(255, 0, 0);
            }
            else if (tPipe == PipeL2)
            {
                return Color.FromArgb(0, 255, 0);
            }
            else if (tPipe == PipeL3)
            {
                return Color.FromArgb(0, 0, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ToGrime(int tGrime)
        {
            if (tGrime == GrimeOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ToShading(int tShading)
        {
            return Color.FromArgb(tShading, tShading, tShading);
        }
        public static Color ToSky(int tSky)
        {
            if (tSky == SkyOn)
            {
                return Color.FromArgb(255, 255, 255);
            }
            return Color.FromArgb(0, 0, 0);
        }

        public Color ColorDepth()
        {
            int val = (int)(Depth * 8.8);
            val = Math.Min(val, 255);
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
            else if (EColor == NoEffectColorD)
            {
                return Color.FromArgb(50, 50, 50);
            }
            else if (EColor == EffectColorAD)
            {
                return Color.FromArgb(150, 0, 150);
            }
            else if (EColor == EffectColorBD)
            {
                return Color.FromArgb(0, 150, 150);
            }
            else if (EColor == EffectColorCD)
            {
                return Color.FromArgb(150, 150, 150);
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
                return ToPipe(PipeL1);
            }
            else if (Pipe == PipeL2)
            {
                return ToPipe(PipeL2);
            }
            else if (Pipe == PipeL3)
            {
                return ToPipe(PipeL3);
            }
            return ToPipe(NoPipe);
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

        

        public struct BufferAction
        {
            public BufferAction(int x, int y, CursorColors.Features components)
            {
                X = x;
                Y = y;
                Components = components;
            }
            public int X { get; }
            public int Y { get; }
            public CursorColors.Features Components { get; }
            public override string ToString() => $"({X}, {Y}, {Components})";
        }
        public void AddToUndoBuffer(List<BufferAction> act)
        {
            if (undoBuffer.Count >= 100)
            {
                undoBuffer.RemoveAt(0);
                undoBuffer.Add(act);
            }
            else
            {
                undoBuffer.Add(act);
            }
        }
        public void RemoveFromUndoBuffer()
        {
            undoBuffer.RemoveAt(undoBuffer.Count - 1);
        }
        public void AddToRedoBuffer(List<BufferAction> act)
        {
            if (redoBuffer.Count >= 100)
            {
                redoBuffer.RemoveAt(0);
                redoBuffer.Add(act);
            }
            else
            {
                redoBuffer.Add(act);
            }
        }

        public void RemoveFromRedoBuffer()
        {
            redoBuffer.RemoveAt(redoBuffer.Count - 1);
        }

        public static Color ColorRendered(Features features)
        {
            int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
            int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
            int tSky = features.ThisSky;
            return ColorRendered(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
        }

        public static Color ColorRendered(int tDepth, int tIndexID, int tEColor, int tLColor, int tLight, int tPipe, int tGrime, int tShading, int tSky)
        {
            int valRed = 0;
            int valGreen = 0;
            int valBlue = 0;
            int useIndex = tIndexID == 0 ? 0 : 1;
            
            if (tSky==1)
                return Color.FromArgb(255, 255, 255);
            else
            {
                if (useIndex==1)
                {
                    valBlue = tIndexID;
                }
                else
                {
                    valBlue = tShading* (tEColor==0 ? 0:1);
                }
                valRed = 1+tDepth + tLColor * 30 + tLight * 90;
                valGreen = (tEColor%4) + tGrime * 4 + useIndex * 8 + (int)(tEColor/4) * 16;
                if(tPipe==PipeL1)
                {
                    valGreen = 7 + PipeL1;
                }
                if (tPipe == PipeL2)
                {
                    valGreen = 7 + PipeL2;
                }
                if (tPipe == PipeL3)
                {
                    valGreen = 7 + PipeL3;
                }
                return Color.FromArgb(valRed, valGreen, valBlue);
            }
        }

        public struct Features
        {
            public Features(int tDepth, int tIndexID, int tEColor, int tLColor, int tLight, int tPipe, int tGrime, int tShading, int tSky)
            {
                ThisDepth = tDepth;
                //ThisIndex = tIndex;
                ThisIndexID = tIndexID;
                ThisEColor = tEColor;
                ThisLColor = tLColor;
                ThisLight = tLight;
                ThisPipe = tPipe;
                ThisGrime = tGrime;
                ThisShading = tShading;
                ThisSky = tSky;
            }

            public int ThisDepth { get; set; }
            //public Color ThisIndex { get; }
            public int ThisIndexID { get; set; }//none is 0, 1st is 255
            public int ThisEColor { get; set; }
            public int ThisLColor { get; set; }
            public int ThisLight { get; set; }
            public int ThisPipe { get; set; }
            public int ThisGrime { get; set; }
            public int ThisShading { get; set; }
            public int ThisSky { get; set; }

            public override bool Equals(Object obj)
            {
                return obj is Features && Equals((Features)obj);
            }
            public bool Equals(Features other)
            {
                return ThisDepth == other.ThisDepth && ThisIndexID == other.ThisIndexID && ThisEColor == other.ThisEColor
                     && ThisLColor == other.ThisLColor && ThisLight == other.ThisLight && ThisPipe == other.ThisPipe
                      && ThisGrime == other.ThisGrime && ThisShading == other.ThisShading && ThisSky == other.ThisSky;
            }

            public static bool operator ==(Features lhs, Features rhs)
            {
                return lhs.Equals(rhs);
            }

            public static bool operator !=(Features lhs, Features rhs)
            {
                return !lhs.Equals(rhs);
            }
            public override string ToString() => $"({ThisDepth}, {ThisIndexID}, {ThisEColor}, {ThisLColor}, {ThisLight}, {ThisPipe}, {ThisGrime}, {ThisShading}, {ThisSky})";
        }

        

        public int IndexColorID(Color colInput)
        {
            //return IndexPalette.FindIndex(a => a == colInput);
            return Array.FindIndex(IndexPalette.Entries, a => a == colInput);
        }

        public Color PreviewPixel(Color colRend,int x, int y)
        {
            CursorColors.Features features = CursorColors.FeaturesRendered(colRend);
            Color c = Color.Black;
            int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
            int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
            int tSky = features.ThisSky;
            int tR = this.Rain ? 8 : 0;
            if (tSky == 1)
            {
                c = imgPalette.GetPixel(0, tR);
            }
            else if (tPipe > 0)
            {
                if (tPipe == PipeL1)
                {
                    c = imgPalette.GetPixel(10, tR);
                }
                else if (tPipe == PipeL2)
                {
                    c = imgPalette.GetPixel(11, tR);
                }
                else if(tPipe == PipeL3)
                {
                    c = imgPalette.GetPixel(12, tR);
                }
            }
            else if (tIndexID > 0)
            {
                c = IndexPalette.Entries[tIndexID];
            }
            else
            {
                c = imgPalette.GetPixel(tDepth, 2 + (2 - tLColor) + 3 * (1 - tLight)+ tR);

                if (tEColor != 0 && tEColor !=4)
                {
                    ECol ThisECol = new ECol();
                    if (tEColor == EffectColorA || tEColor == EffectColorAD)
                        ThisECol = BaseECols[icolA];
                    else if (tEColor == EffectColorB || tEColor == EffectColorBD)
                        ThisECol = BaseECols[icolB];
                    else if (tEColor == EffectColorC || tEColor == EffectColorCD)
                        ThisECol = BaseECols[9];
                    ECol.WeatherECol ThisEColW = new ECol.WeatherECol();
                    if (!Rain)
                        ThisEColW = ThisECol.Dry;
                    else
                        ThisEColW = ThisECol.Wet;
                    ECol.WeatherECol.ShadeECol ThisEColS = new ECol.WeatherECol.ShadeECol();
                    if (tLight == LightOn)
                        ThisEColS = ThisEColW.Sun;
                    else
                        ThisEColS = ThisEColW.Shadow;
                    Color cOut = new Color();
                    if ((int)(tEColor/4.0) == 0)
                        cOut = ThisEColS.Light;
                    else
                        cOut = ThisEColS.Dark;
                    c = Form1.Blend(cOut, c, (double)tShading / 255);
                }
                if (tGrime > 0)
                {
                    int a = Math.Min((int)Math.Round(imgGrimeMask.GetPixel(x%imgGrimeMask.Width, y%imgGrimeMask.Height).GetBrightness() * 31), 31);
                    c = Form1.Blend(imgPalette.GetPixel(a, 1), c, GrimeAlpha*0.1);
                }
            }
            return c;
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
            int tDepth; Color tIndex; int tIndexID; int tEColor; int tLColor; int tLight; int tPipe; int tGrime; int tShading; int tSky;

            if (R == 255 && G == 255 && B == 255)//if sky
            {
                tDepth = 29;
                tIndex = Color.Transparent;
                tIndexID = 0;
                tEColor = 0;
                tLColor = 0;
                tLight = 0; 
                tPipe = NoPipe; 
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
                //tPipe = (G == 8 ? 1 : 0 + G == 9 ? 2 : 0 + G == 10 ? 3 : 0) * B == 0 ? 1 : 0;//0-3 the B==0 is for of index colors, change this later to only index
                if (G == 7+PipeL1 && B == 0) tPipe = PipeL1;
                else if (G == 7 + PipeL2 && B == 0) tPipe = PipeL2;
                else if (G == 7 + PipeL3 && B == 0) tPipe = PipeL3;
                else tPipe = NoPipe;
                tEColor = 4*(int)(G / 16.0);
                G = G % 16; //0-15
                int HasIndex = Math.Min(G / 8, 1); //0 or 1
                HasIndex = HasIndex * ( (B != 0 && tPipe == NoPipe) ? 1 : 0);
                G = G % 8; //0-7
                tGrime = G / 4;//0 or 1
                tEColor = tEColor+G % 4;//0-3
                tShading = (1 - HasIndex) * (tEColor > 0 ? 1 : 0) * B; //0-255
                if(HasIndex>0)
                {
                    tIndexID = B;
                }
                else
                {
                    tIndexID = 0;
                }
                if(tPipe!=NoPipe)
                {
                    tEColor = 0;
                    tIndexID = 0;
                    tGrime = 0;
                    tShading = 0;
                }
                
            }//end not sky
            var output = new Features(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
            return output;
        }

        public static Features FeaturesFromColors(Color tDepth, int tIndexID, Color tEColor, Color tLColor, Color tLight, Color tPipe, Color tGrime, Color tShading, Color tSky)
        {
            var output = new CursorColors.Features(
                (int)(tDepth.R / 8.79),
                tIndexID,
                //(tEColor== ToEColor(EffectColorA) ? EffectColorA : 0)+ (tEColor == ToEColor(EffectColorB) ? EffectColorB : 0)
                //+ (tEColor == ToEColor(EffectColorC) ? EffectColorC : 0) + (tEColor == ToEColor(NoEffectColorD) ? NoEffectColorD : 0) 
                //+ (tEColor == ToEColor(EffectColorAD) ? EffectColorAD : 0) + (tEColor == ToEColor(EffectColorBD) ? EffectColorBD : 0) 
                //+ (tEColor == ToEColor(EffectColorCD) ? EffectColorCD : 0),
                ((tEColor == ToEColor(EffectColorA) || tEColor == ToEColor(EffectColorAD)) ? EffectColorA : 0)
                + ((tEColor == ToEColor(EffectColorB) || tEColor == ToEColor(EffectColorBD)) ? EffectColorB : 0)
                + ((tEColor == ToEColor(EffectColorC) || tEColor == ToEColor(EffectColorCD)) ? EffectColorC : 0)
                + ((tEColor == ToEColor(NoEffectColor) || tEColor == ToEColor(NoEffectColorD)) ? NoEffectColor : 0),
                (tLColor == ToLColor(GeometryNeutral) ? GeometryNeutral : 0) + (tLColor == ToLColor(GeometryLight) ? GeometryLight : 0),
                (int)(tLight.R / 255),
                (tPipe == ToPipe(PipeL1) ? PipeL1 : 0) + (tPipe == ToPipe(PipeL2) ? PipeL2 : 0) + (tPipe == ToPipe(PipeL3) ? PipeL3 : 0),
                (int)(tGrime.R / 255),
                (int)(tShading.R),
                (int)(tSky.R / 255)
            );
            return output;
        }

    }
}
