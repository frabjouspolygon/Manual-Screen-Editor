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
    class Class1
    {
        //public static Bitmap GetImage(string imageName)
        //{
        //    return (Bitmap)Image.FromFile(imageName + ".png");
        //}

        //public static int[,] ArrayToInt(float[,] flArray)
        //{
        //    int width = flArray.GetLength(0);
        //    int height = flArray.GetLength(1);
        //    int[,] intArray = new int[width, height];
        //    for (int i = 0; i < width; i++)
        //    {
        //        for (int j = 0; j < height; j++)
        //        {
        //            intArray[i, j] = (int)Math.Round(flArray[i, j]);
        //        }
        //    }
        //    return intArray;
        //}

        //public static Color[,,] GenerateBlackImg(int[] shape)
        //{
        //    int height = shape[0];
        //    int width = shape[1];
        //    Color[,,] imgBlack = new Color[height, width, 4];
        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            imgBlack[i, j, 0] = Color.FromArgb(0, 0, 0, 255);
        //            imgBlack[i, j, 1] = Color.FromArgb(0, 0, 0, 255);
        //            imgBlack[i, j, 2] = Color.FromArgb(0, 0, 0, 255);
        //            imgBlack[i, j, 3] = Color.FromArgb(255, 255, 255, 255);
        //        }
        //    }
        //    return imgBlack;
        //}

        //public static void DecomposeRender(string renderName)
        //{
        //    Bitmap imgRendered = GetImage(renderName);
        //    float[,,] imgRenderedArray = new float[imgRendered.Height, imgRendered.Width, 4];
        //    for (int i = 0; i < imgRendered.Height; i++)
        //    {
        //        for (int j = 0; j < imgRendered.Width; j++)
        //        {
        //            Color pixel = imgRendered.GetPixel(j, i);
        //            imgRenderedArray[i, j, 0] = pixel.R;
        //            imgRenderedArray[i, j, 1] = pixel.G;
        //            imgRenderedArray[i, j, 2] = pixel.B;
        //        }
        //    }

        //    imgRenderedArray = imgRenderedArray * 255;
        //    int[,] imgDepth = ArrayToInt(imgRenderedArray);
        //    Color[,,] imgLColor = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    Color[,,] imgLight = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    Color[,,] imgEColor = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    Color[,,] imgRainbow = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    Color[,,] imgPipe = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    int[,] imgIndex = new int[imgRendered.Height, imgRendered.Width];
        //    Color[,,] imgShading = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });
        //    Color[,,] imgSky = GenerateBlackImg(new int[] { imgRendered.Height, imgRendered.Width });

        //    Color cRed = Color.FromArgb(255, 0, 0, 255);
        //    Color cGreen = Color.FromArgb(0, 255, 0, 255);
        //    Color cBlue = Color.FromArgb(0, 0, 255, 255);
        //    Color cMagenta = Color.FromArgb(255, 0, 255, 255);
        //    Color cCyan = Color.FromArgb(0, 255, 255, 255);
        //    Color cBlack = Color.FromArgb(0, 0, 0, 255);
        //    Color cWhite = Color.FromArgb(255, 255, 255, 255);
        //    Color cClear = Color.FromArgb(0, 0, 0, 0);

        //    int h = imgRendered.Height;
        //    int w = imgRendered.Width;

        //    for (int i = 0; i < h; i++)
        //    {
        //        for (int j = 0; j < w; j++)
        //        {
        //            float r = imgRenderedArray[i, j, 0];
        //            float g = imgRenderedArray[i, j, 1];
        //            float b = imgRenderedArray[i, j, 2];
        //            if (r == 255 && g == 255 && b == 255)
        //            {
        //                imgSky[i, j, 0] = cWhite;
        //                imgLColor[i, j, 0] = cRed;
        //                imgLight[i, j, 0] = cWhite;
        //                imgDepth[i, j, 0] = cWhite;
        //                imgShading[i, j, 0] = cBlack;
        //                continue;
        //            }

        //            float r2 = r;
        //            float g2 = g;

        //            // depth related data
        //            if (r2 >= 91) // light
        //            {
        //                imgLight[i, j, 0] = cWhite;
        //                r2 -= 90;
        //            }
        //            if (r2 >= 1 && r2 <= 30) // red
        //            {
        //                imgLColor[i, j, 0] = cRed;
        //            }
        //            else if (r2 >= 31 && r2 <= 60) // green
        //            {
        //                imgLColor[i, j, 0] = cGreen;
        //                r2 -= 30;
        //            }
        //            else if (r2 >= 61 && r2 <= 90) // blue
        //            {
        //                imgLColor[i, j, 0] = cBlue;
        //                r2 -= 60;
        //            }
        //            r2 -= 1;
        //            r2 = (int)(r2 * (255 / 29));
        //            imgDepth[i, j, 0] = Color.FromArgb((int)r2, (int)r2, (int)r2, 255);

        //            // effect related data
        //            bool blnIndex = false;
        //            bool blnPipe = false;
        //            bool blnSky = false;
        //            bool blnDark = false;
        //            bool blnRainbow = false;
        //            if (g2 >= 16) // dark
        //            {
        //                blnDark = true;
        //                g2 -= 16;
        //            }
        //            if (g2 >= 8) // index or pipe
        //            {
        //                if (b == 0 && (g == 8 || g == 9 || g == 10)) // pipe
        //                {
        //                    blnPipe = true;
        //                }
        //                else // index
        //                {
        //                    blnIndex = true;
        //                }
        //                g2 -= 8;
        //            }
        //            if (g2 >= 4) // rainbow
        //            {
        //                blnRainbow = true;
        //                imgRainbow[i, j, 0] = cWhite;
        //                g2 -= 4;
        //            }

        //            // EColor
        //            if (g2 == 1) // effect color A
        //            {
        //                imgEColor[i, j, 0] = cMagenta;
        //            }
        //            else if (g2 == 2) // effect color B
        //            {
        //                imgEColor[i, j, 0] = cCyan;
        //            }
        //            else if (g2 == 3) // effect color white
        //            {
        //                imgEColor[i, j, 0] = cWhite;
        //            }
        //            else // no effect color
        //            {
        //                imgEColor[i, j, 0] = cBlack;
        //            }

        //            // Index
        //            if (blnIndex)
        //            {
        //                imgIndex[i, j] = imgRenderedArray[0, 255 - (int)b];
        //            }

        //            // pipe
        //            if (blnPipe)
        //            {
        //                if (g == 8) // layer 1
        //                {
        //                    imgPipe[i, j, 0] = cRed;
        //                }
        //                else if (g == 9) // layer 3
        //                {
        //                    imgPipe[i, j, 0] = cBlue;
        //                }
        //                else if (g == 10) // layer 2
        //                {
        //                    imgPipe[i, j, 0] = cGreen;
        //                }
        //            }

        //            // shading
        //            imgShading[i, j, 0] = Color.FromArgb((int)b, (int)b, (int)b, 255);
        //        }
        //    }

        //    SaveImage(renderName + "/depth.png", imgDepth);
        //    SaveImage(renderName + "/lcolor.png", imgLColor);
        //    SaveImage(renderName + "/light.png", imgLight);
        //    SaveImage(renderName + "/ecolor.png", imgEColor);
        //    SaveImage(renderName + "/rainbow.png", imgRainbow);
        //    SaveImage(renderName + "/pipe.png", imgPipe);
        //    SaveImage(renderName + "/index.png", imgIndex);
        //    SaveImage(renderName + "/shading.png", imgShading);
        //    SaveImage(renderName + "/sky.png", imgSky);
        //}

        //private static void SaveImage(string path, Color[,,] imageArray)
        //{
        //    int height = imageArray.GetLength(0);
        //    int width = imageArray.GetLength(1);
        //    Bitmap bitmap =
    





        }
    }
