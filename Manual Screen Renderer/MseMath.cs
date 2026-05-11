using System;
                                                                                                                                                                                                                                                                                                                                                                                                                              
namespace Manual_Screen_Renderer
{
    class MseMath
    {
        public MseMath()
        {
        }

        //(min_i, max_i, min_f, max_f, value_i)
        public static double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        //(min, max, value) inclusive
        public static double Clamp(double min, double max, double a) => Math.Min(max, Math.Max(min, a));
        public static double Clamp(float min, float max, float a) => Math.Min(max, Math.Max(min, a));
        public static int Mix(int over, int back, double factor)
        {
            factor = Clamp(0, 1, factor);
            return (int)(over * factor + back * (1d - factor));
        }

        /*public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            return val;
        }*/

        public static float Lerp(float start, float end, float t)
        {
            return start + (end - start) * (float)Clamp(t, 0f, 1f);
        }

        public static System.Drawing.Color ColorLerp(System.Drawing.Color a, System.Drawing.Color b, float t)
        {
            t = Math.Max(0, Math.Min(1, t));
            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int b_val = (int)(a.B + (b.B - a.B) * t);
            int alpha = (int)(a.A + (b.A - a.A) * t);
            return System.Drawing.Color.FromArgb(alpha, r, g, b_val);
        }


        public static System.Drawing.Color MultCols(System.Drawing.Color color1, System.Drawing.Color color2)
        {
            return System.Drawing.Color.FromArgb(
                (int)((color1.A/255 * color2.A/255)*255),
                (int)((color1.R / 255 * color2.R / 255) * 255),
                (int)((color1.G / 255 * color2.G / 255) * 255),
                (int)((color1.B / 255 * color2.B / 255) * 255)
            );
        }

        public static System.Drawing.Color MultCols(System.Drawing.Color color1, float factor)
        {
            return System.Drawing.Color.FromArgb(
                (int)Clamp(color1.A * factor,0,255),
                (int)Clamp(color1.R * factor, 0, 255),
                (int)Clamp(color1.G * factor, 0, 255),
                (int)Clamp(color1.B * factor, 0, 255)
            );
        }
    }


}
