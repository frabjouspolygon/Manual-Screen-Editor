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

        public static int Mix(int over, int back, double factor)
        {
            factor = Clamp(0, 1, factor);
            return (int)(over * factor + back * (1d - factor));
        }
    }


}
