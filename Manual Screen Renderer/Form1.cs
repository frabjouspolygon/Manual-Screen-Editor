using Manual_Screen_Renderer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
//using System.Windows.Media;

//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using static Manual_Screen_Renderer.CursorColors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
//using MediaColor = System.Windows.Media.Color;
//using Color = System.Drawing.Color;

namespace Manual_Screen_Renderer
{
    public partial class Form1 : Form
    {
        static ImgInterface iiDepth = null;
        static ImgInterface iiEColor = null;
        static ImgInterface iiIndex = null;
        static ImgInterface iiLColor = null;
        static ImgInterface iiLight = null;
        static ImgInterface iiPipe = null;
        static ImgInterface iiRainbow = null;
        static ImgInterface iiShading = null;
        static ImgInterface iiSky = null;
        static ImgInterface iiRendered = null;
        static Bitmap imgDepth = null;
        static Bitmap imgEColor = null;
        static Bitmap imgIndex = null;
        static Bitmap imgLColor = null;
        static Bitmap imgLight = null;
        static Bitmap imgPipe = null;
        static Bitmap imgRainbow = null;
        static Bitmap imgShading = null;
        static Bitmap imgSky = null;
        static Bitmap imgRendered = null;
        static Bitmap imgPreview = null;
        //static Bitmap imgPalette = null;
        //static Bitmap imgGrimeMask = null;
        static int intMode = 9;//0-9
        static Color colCursor = Color.FromArgb(0,0,0,0);
        static CursorColors ccPaint = null;
        static bool blnDepth = false;
        static bool blnEColor = false;
        static bool blnIndex = false;
        static bool blnLColor = false;
        static bool blnLight = false;
        static bool blnPipe = false;
        static bool blnRainbow = false;
        static bool blnShading = false;
        static bool blnSky = false;
        static bool blnRendered = false;
        static string strFileName = null;
        static string strFilePath = null;
        static bool pickerMode = false;
        static bool paletteMode = false;
        static bool changed = true;
        public Point lastCursor = new Point();
        //static Color colA = Color.FromArgb( 255, 0, 255);
        //static Color colB = Color.FromArgb(0, 255, 255);
        public Form1()
        {
            InitializeComponent();
            iiDepth = new ImgInterface(txtDepth, btnDepth);
            iiEColor = new ImgInterface(txtEColor, btnEColor);
            iiIndex = new ImgInterface(txtIndex, btnIndex);
            iiLColor = new ImgInterface(txtLColor, btnLColor);
            iiLight = new ImgInterface(txtLight, btnLight);
            iiPipe = new ImgInterface(txtPipe, btnPipe);
            iiRainbow = new ImgInterface(txtRainbow, btnRainbow);
            iiShading = new ImgInterface(txtShading, btnShading);
            iiSky = new ImgInterface(txtSky, btnSky);
            iiRendered = new ImgInterface(txtRendered, btnRendered);
            ccPaint = new CursorColors();
            imgDepth = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgEColor = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            //imgIndex = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0, 0));
            imgIndex = new Bitmap(1400, 800, PixelFormat.Format8bppIndexed);
            //ccPaint.imgPalette = SolidBitmap(32, 8, Color.FromArgb(0, 0, 0));
            ccPaint.IndexPalette = imgIndex.Palette;
            for (int i = 0; i < 256; i++)
            {
                ccPaint.IndexPalette.Entries[i] = Color.Transparent;
            }
            imgIndex.Palette = ccPaint.IndexPalette;
            ccPaint.IndexID = 255;
            imgLColor = SolidBitmap(1400, 800, Color.FromArgb(255, 0, 0)); ;
            imgLight = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgPipe = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgRainbow = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgShading = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgSky = SolidBitmap(1400, 800, Color.FromArgb(0, 0, 0));
            imgRendered = SolidBitmap(1400, 800, Color.FromArgb(1, 0, 0));
            imgPreview = SolidBitmap(1400, 800, Color.FromArgb(1, 0, 0));
            //imgGrimeMask = new Bitmap(Properties.Resources.GrimeMask);
            //pbxWorkspace.SizeMode = PictureBoxSizeMode.AutoSize;
            //splitContainer1.Panel2.AutoScroll = true;
            pnlWorkspace.AutoScroll = true;
            pbxWorkspace.MouseWheel += pbxWorkspace_MouseWheel;
            //pbxWorkspace.
            pbxWorkspace.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            lastCursor = Cursor.Position;
            this.DoubleBuffered = true;
            int R = 180;
            ccPaint.EColor = CursorColors.NoEffectColor;
            int Light = R > 90 ? 1 : 0;// 0 or 1
            R = R - 90 * Light;
            int LColor = Math.Min(Math.Max((R - 1) / 30, 0), 2);//0-2
            R = R - LColor * 30-1;
            int Depth = Math.Min(Math.Max(R, 0), 30);//0-29
            //Depth = Math.Min((int)(Depth * 8.79), 255);
            Console.WriteLine(Light);
            Console.WriteLine(LColor);
            Console.WriteLine(R);
            Console.WriteLine(imgIndex.Palette.Entries[0].A);
            RefreshWorkspace();
        }

        public static double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s-a1)*(b2-b1)/(a2-a1);
        public static double Clamp(double min, double max, double a) => Math.Min(max,Math.Max(min,a));

        public static List<Point> GetBresenhamLine(Point p0, Point p1)
        {
            int x0 = p0.X;
            int y0 = p0.Y;
            int x1 = p1.X;
            int y1 = p1.Y;
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;
            var points = new List<Point>();
            while (true)
            {
                points.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err = err - dy;
                    x0 = x0 + sx;
                }
                if (e2 < dx)
                {
                    err = err + dx;
                    y0 = y0 + sy;
                }
            }
            return points;
        }

        public static Bitmap SolidBitmap(int width, int height, Color colFill)
        {
            Bitmap Bmp = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(colFill))
            {
                gfx.FillRectangle(brush, 0, 0, width, height);
            }
            
            return Bmp;
        }

        public static Image ConvertToIndexed(Bitmap oldbmp)
        {
            var bmp8bpp = Grayscale.CommonAlgorithms.BT709.Apply(oldbmp);
            return bmp8bpp;
            /*using (var ms = new MemoryStream())
            {
                oldbmp.Save(ms, ImageFormat.Gif);
                ms.Position = 0;
                return Image.FromStream(ms);
            }*/
        }
        public static Color Blend(Color color, Color backColor, double amount)
        {
            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }
        public static Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            return b.Clone(r, b.PixelFormat);
        }

        public string ImageDialogue()
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Filter = "png files (*.png)|*.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }//end using
            return filePath;
        }

        public (string,string,string) ImageDialogue2()
        {
            var filePath = string.Empty;
            var fileName = string.Empty;
            var fileFull = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Filter = "png files (*.png)|*.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    filePath = Path.GetDirectoryName(openFileDialog.FileName);
                    fileFull = openFileDialog.FileName;
                }
            }//end using
            return (filePath, fileName,fileFull);
        }

        public string ImageDialogueFiltered(string strLayer)
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Filter = "png files (*"+strLayer+".png)|*"+strLayer+".png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }//end using
            return filePath;
        }

        private void btnDepth_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_depth");// ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtDepth.Text = filePath;
                imgDepth = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtDepth.Text = "";
                //imgDepth = null;
            }
        }
        private void btnEColor_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_ecolor");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtEColor.Text = filePath;
                imgEColor = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtEColor.Text = "";
                //imgEColor = null;
            }
        }
        private void txtEColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                if (!iiEColor.UpdateFiles(txtEColor.Text))
                {
                    MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                }
            }
        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_index");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                LoadIndexFromRGBBitmap5(myBitmap);
                //imgIndex = (Bitmap)ConvertPixelformat(ref myBitmap);
                //imgIndex = (Bitmap)ConvertToIndexed(myBitmap);
                //Console.WriteLine("converted");
                //imgIndex = new Bitmap(myBitmap.Width, myBitmap.Height, PixelFormat.Format8bppIndexed);
                //using (Graphics gr = Graphics.FromImage(imgIndex))
                //{
                //    gr.DrawImage(myBitmap, new Rectangle(0, 0, imgIndex.Width, imgIndex.Height));
                //}
                txtIndex.Text = filePath;
                //LoadIndexFromRGBBitmap2();
                //LoadIndexFromRGBBitmap3();
                Console.WriteLine("loaded index");
                RefreshWorkspace();
                Console.WriteLine("refresh");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtIndex.Text = "";
                //imgIndex = null;
            }
        }


        private void LoadIndexFromRGBBitmap()
        {
            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
                ccPaint.IndexPalette.Entries[i] = Color.Transparent;//empty out the index palette
            int slot = 1;
            for (int i = 0; i < imgRendered.Height; i++)
            {
                for (int j = 0; j < imgRendered.Width; j++)
                {
                    Color color = imgIndex.Palette.Entries[GetPixelIndexedBitmap(imgIndex, j, i)];
                    Console.WriteLine(i.ToString() + ", " + j.ToString());
                    if (slot < 255 && color.A == 255)
                    {
                        bool claimed = false;
                        for (int k = 0; k < ccPaint.IndexPalette.Entries.Length; k++)
                        {
                            if (color == ccPaint.IndexPalette.Entries[k])
                                claimed = true;
                        }
                        if (!claimed)
                        {
                            ccPaint.IndexPalette.Entries[slot] = color;
                            slot++;
                        }

                    }
                }
            }
        }

        private void LoadIndexFromRGBBitmap2()
        {
            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
                ccPaint.IndexPalette.Entries[i] = Color.Transparent;//empty out the index palette
            List<int> taken = new List<int>();
            Size s = imgIndex.Size;
            Rectangle rect = new Rectangle(Point.Empty, s);
            BitmapData bmpData = imgIndex.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            int size1 = bmpData.Stride * bmpData.Height;
            byte[] data2 = new byte[bmpData.Stride * s.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data2, 0, data2.Length);
            int slot = 1;
            for (int i = 0; i < s.Height; i++)
            {
                for (int j = 0; j < s.Width; j++)
                {
                    int idx = data2[i * bmpData.Stride + j];
                    //Color color = imgIndex.Palette.Entries[idx];
                    //Console.WriteLine(i.ToString() + ", " + j.ToString());
                    if (slot < 255)// && color.A == 255)
                    {
                        bool claimed = false;
                        for (int k = 0; k < taken.Count; k++)
                        {
                            if (idx == taken[k])
                            {
                                claimed = true;
                                k = taken.Count;
                            }

                        }
                        /*for (int k = 0; k < ccPaint.IndexPalette.Entries.Length; k++)
                        {
                            if (color == ccPaint.IndexPalette.Entries[k])
                                claimed = true;
                        }*/
                        if (!claimed)
                        {
                            taken.Add(idx);
                            ccPaint.IndexPalette.Entries[slot] = imgIndex.Palette.Entries[idx];
                            slot++;
                        }
                    }
                    else if (slot > 255)
                    {
                        break;
                    }
                }
            }
            imgIndex.UnlockBits(bmpData);
        }

        private unsafe void LoadIndexFromRGBBitmap4(Bitmap imgInput)
        {
            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
            {
                ccPaint.IndexPalette.Entries[i] = Color.Transparent;//empty out the index palette
                imgIndex.Palette.Entries[i] = Color.Transparent;
            }
            Size s = imgInput.Size;
            PixelFormat fmt = imgIndex.PixelFormat;
            byte bpp = (byte)4;
            Rectangle rect = new Rectangle(Point.Empty, s);
            BitmapData bmpData0 = imgInput.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData1 = imgIndex.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            //BitmapData bmpData = imgIndex.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            int size0 = bmpData0.Stride * bmpData0.Height;

            int size1 = bmpData1.Stride * bmpData1.Height;
            byte[] data0 = new byte[size0*4];
            byte[] data1 = new byte[bmpData1.Stride * s.Height];
            Console.WriteLine("length " + data1.Length.ToString());
            Console.WriteLine((1400*800).ToString());
            
            int slot = 1;
            //byte* line = (byte*)bmpData1.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(bmpData0.Scan0, data0, 0, size0);
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data1, 0, data1.Length);

            for (int i = 0; i < s.Height; i++)
            {

                Console.WriteLine(i.ToString() + " slot: " + slot.ToString());
                //line += bmpData1.Stride;
                for (int j = 0; j < s.Width; j++)
                {
                    int index = i * bmpData0.Stride + j * bpp;
                    Color c = Color.FromArgb(data0[index + 3], data0[index + 2], data0[index + 1], data0[index]);
                    
                    if (slot < 255 && c.A > 0)
                    {
                        bool claimed = false;
                        for (int k = 0; k < ccPaint.IndexPalette.Entries.Length; k++)
                        {
                            if (c == ccPaint.IndexPalette.Entries[k])
                            {
                                claimed = true;
                                break;
                                //k = ccPaint.IndexPalette.Entries.Length-1;
                            }
                        }
                        if (!claimed)
                        {
                            //data2[y * bmpData2.Stride + x];
                            Console.WriteLine(c.R.ToString() + " " + c.G.ToString() + " " + c.B.ToString() + " " + c.A.ToString());
                            data1[i * bmpData1.Stride + j] = (byte)slot;
                            //line[j] = (byte)slot;
                            imgIndex.Palette.Entries[slot] = c;
                            ccPaint.IndexPalette.Entries[slot] = c;
                            //data1[index + 0] = c.B;
                            //data1[index + 1] = c.G;
                            //data1[index + 2] = c.R;
                            //data1[index + 3] = 255;
                            slot++;
                        }
                    }
                    else if (slot > 255)
                    {
                        break;
                    }
                }
            }

            //imgIndex.UnlockBits(bmpData1);
            //System.Runtime.InteropServices.Marshal.Copy(data2, 0, bmpData1.Scan0, data2.Length);
            imgInput.UnlockBits(bmpData0);
            imgIndex.UnlockBits(bmpData1);
        }


        private void LoadIndexFromRGBBitmap3()
        {
            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
                ccPaint.IndexPalette.Entries[i] = imgIndex.Palette.Entries[i];
        }

        private void LoadIndexFromRGBBitmap5(Bitmap imgInput)
        {
            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
            {
                ccPaint.IndexPalette.Entries[i] = Color.Transparent;//empty out the index palette
                imgIndex.Palette.Entries[i] = Color.Transparent;
            }
                
            int slot = 255;
            for (int i = 0; i < imgInput.Height; i++)
            {
                Console.WriteLine(i.ToString() + ", " + slot.ToString());
                for (int j = 0; j < imgInput.Width; j++)
                {
                    Color color = imgInput.GetPixel(j,i);// imgIndex.Palette.Entries[GetPixelIndexedBitmap(imgIndex, j, i)];
                    
                    if (slot > 0 && color.A == 255)
                    {
                        bool claimed = false;
                        for (int k = 0; k < ccPaint.IndexPalette.Entries.Length; k++)
                        {
                            if (color == ccPaint.IndexPalette.Entries[k])
                            {
                                claimed = true;
                                break;
                            }
                        }
                        if (!claimed)
                        {
                            //imgIndex.Palette.Entries[slot] = color;
                            imgIndex = CursorColors.SetPixelIndexedBitmap(imgIndex, slot, j, i);
                            ccPaint.IndexPalette.Entries[slot] = color;
                            slot--;
                        }
                        

                    }
                    if (color.A == 255)
                    {
                        imgIndex = CursorColors.SetPixelIndexedBitmap(imgIndex, ccPaint.IndexColorID(color), j, i);
                    }
                }
            }

            for (int i = 0; i < ccPaint.IndexPalette.Entries.Length; i++)
            {
                imgIndex.Palette.Entries[i] = ccPaint.IndexPalette.Entries[i];
            }
        }

        private void btnLColor_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_lcolor");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtLColor.Text = filePath;
                imgLColor = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtLColor.Text = "";
                //imgLColor = null;
            }
        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_light");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtLight.Text = filePath;
                imgLight = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtLight.Text = "";
                //imgLight = null;
            }
        }

        private void btnPipe_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_pipe");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtPipe.Text = filePath;
                imgPipe = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtPipe.Text = "";
                //imgPipe = null;
            }
        }

        private void btnRainbow_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_grime");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtRainbow.Text = filePath;
                imgRainbow = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtRainbow.Text = "";
                //imgRainbow = null;
            }
        }

        private void btnShading_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_shading");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtShading.Text = filePath;
                imgShading = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtShading.Text = "";
                //imgShading = null;
            }
        }

        private void btnSky_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogueFiltered("_sky");//ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                txtSky.Text = filePath;
                imgSky = myBitmap;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtSky.Text = "";
                //imgSky = null;
            }
        }

        private void btnRendered_Click(object sender, EventArgs e)
        {
            //string filePath = ImageDialogue();
            var (filePath, fileName, fileFull) = ImageDialogue2();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(fileFull);
                txtRendered.Text = filePath;
                imgRendered = myBitmap;
                strFileName = fileName;
                strFilePath = filePath;
                RefreshWorkspace();
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtRendered.Text = "";
                //imgRendered = null;
            }
        }


        private void LayerButtons(Button button)
        {
            int mode;
            bool toggle;
            if (button == btnEditDepth){mode = 0;toggle = blnDepth;}
            else if (button == btnEditEColor){mode = 1;toggle = blnEColor;}
            else if (button == btnEditIndex){mode = 2;toggle = blnIndex;}
            else if (button == btnEditLColor){mode = 3;toggle = blnLColor;}
            else if (button == btnEditLight){mode = 4;toggle = blnLight;}
            else if (button == btnEditPipe){mode = 5;toggle = blnPipe;}
            else if (button == btnEditRainbow){mode = 6;toggle = blnRainbow;}
            else if (button == btnEditShading){mode = 7;toggle = blnShading;}
            else if (button == btnEditSky){mode = 8;toggle = blnSky;}
            else{mode = 9;toggle = blnRendered;}

            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if (mode != 9)
                {
                    if (toggle)
                    {
                        toggle = false;
                        button.BackColor = Color.White;
                    }
                    else
                    {
                        toggle = true;
                        button.BackColor = Color.LightGray;
                    }
                    if (button == btnEditDepth) { blnDepth = toggle; }
                    else if (button == btnEditEColor) { blnEColor = toggle; }
                    else if (button == btnEditIndex) { blnIndex = toggle; }
                    else if (button == btnEditLColor) { blnLColor = toggle; }
                    else if (button == btnEditLight) { blnLight = toggle; }
                    else if (button == btnEditPipe) { blnPipe = toggle; }
                    else if (button == btnEditRainbow) { blnRainbow = toggle; }
                    else if (button == btnEditShading) { blnShading = toggle; }
                    else if (button == btnEditSky) { blnSky = toggle; }
                }
            }
            else
            {
                if (intMode != mode)
                {
                    intMode = mode;
                    RefreshWorkspace();
                    List<Button> buttons = new List<Button> { btnEditDepth, btnEditEColor, btnEditIndex, btnEditLColor, btnEditLight, btnEditPipe, btnEditRainbow, btnEditShading, btnEditSky, btnShowRendered };
                    foreach (Button btn in buttons)
                    {
                        btn.FlatAppearance.BorderSize = 0;
                    }
                    button.FlatAppearance.BorderSize = 1;
                }
            }
        }

        private void btnEditDepth_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditDepth);
        }

        private void btnEditEColor_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditEColor);
        }

        private void btnEditIndex_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditIndex);
        }

        private void btnEditLColor_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditLColor);
        }

        private void btnEditLight_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditLight);
        }

        private void btnEditPipe_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditPipe);
        }

        private void btnEditRainbow_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditRainbow);
        }

        private void btnEditShading_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditShading);
        }

        private void btnEditSky_Click(object sender, EventArgs e)
        {
            LayerButtons(btnEditSky);
        }

        private void btnShowRendered_Click(object sender, EventArgs e)
        {
            LayerButtons(btnShowRendered);
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            pickerMode = true;
            btnColorPicker.FlatAppearance.BorderColor = Color.Blue;
            //colorDialog1.ShowDialog();
            //Color colSelection = colorDialog1.Color;
            //colCursor = colSelection;
            //btnColorPicker.BackColor = colorDialog1.Color;
        }

        private void pbxWorkspace_MouseWheel(object sender, MouseEventArgs e)
        {
            if((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (pbxWorkspace.Image != null)
                {
                    pbxWorkspace.Focus();
                    try
                    {
                        int height = pbxWorkspace.Size.Height;
                        int width = pbxWorkspace.Size.Width;
                        int factor = (int)(width/50) * Math.Sign(e.Delta);
                        int newHeight = height + factor;
                        int newWidth = newHeight * pbxWorkspace.Image.Width / pbxWorkspace.Image.Height;
                        if ((Math.Sign(e.Delta)>=0 && newHeight <= pbxWorkspace.Image.Height*20 )|| (Math.Sign(e.Delta) <= 0 && pbxWorkspace.Image.Height / newHeight < 4))
                        {
                            float wi = pbxWorkspace.Width;
                            float hi = pbxWorkspace.Height;
                            float tpi = pbxWorkspace.PointToClient(Cursor.Position).X;
                            float tui = pbxWorkspace.PointToClient(Cursor.Position).Y;
                            float tw = pnlWorkspace.PointToClient(Cursor.Position).X;
                            float th = pnlWorkspace.PointToClient(Cursor.Position).Y;
                            pbxWorkspace.SuspendLayout();
                            pnlWorkspace.SuspendLayout();
                            pbxWorkspace.Size = new Size(newWidth, newHeight);
                            float wf=pbxWorkspace.Width;
                            int df=(int)Math.Abs(wf*(tpi/wi-tw/wf));
                            float hf = pbxWorkspace.Height;
                            int kf = (int)Math.Abs(hf * (tui / hi - th / hf));
                            pnlWorkspace.AutoScrollPosition = new Point(df, kf);
                            pnlWorkspace.ResumeLayout();
                            pbxWorkspace.ResumeLayout();
                            pbxWorkspace.Refresh();
                        }
                    }
                    catch { }
                }
            }
            
        }

        private void pbxWorkspace_MouseEnter(object sender, EventArgs e)
        {
            
        }
        private void pbxWorkspace_MouseUp(object sender, MouseEventArgs e)
        {
            ReleasePaint();
        }
        private void pbxWorkspace_MouseLeave(object sender, EventArgs e)
        {
            ReleasePaint();
        }
        private void ReleasePaint()
        {
            ccPaint.AddToUndoBuffer(new List<BufferAction>(ccPaint.workingBuffer));
            ccPaint.workingBuffer = new List<BufferAction>();
            lastCursor = new Point(-1,-1);
        }

        private Point WorkspacePosition(Point clientPoint)
        {
            //Point clientPoint = Cursor.Position;//PointToClient(Cursor.Position);
            var workRect = pbxWorkspace.DisplayRectangle;//ClientRectangle;
            
            workRect = pbxWorkspace.RectangleToScreen(workRect);
            var intX = (int)(Map(workRect.Left,workRect.Right,0,pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
            var intY = (int)(Map(workRect.Top, workRect.Bottom, 0,pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
            intX = (int)Clamp(0, pbxWorkspace.Image.Width-1, intX);
            intY = (int)Clamp(0, pbxWorkspace.Image.Height-1, intY);
            /*dynamic myvar = pbxWorkspace;
            Type type = myvar.GetType();
            System.Reflection.PropertyInfo property = type.GetProperty("Parent");
            while (property != null)
            {
                type = myvar.GetType();
                property = type.GetProperty("Parent");
                dynamic myvar = property.GetValue;
                Type type = myvar.GetType();
                property = type.GetProperty("Parent");
            }
            if (property != null)
            {
                property.SetValue(obj, 123);
            }
            if (myvar.GetType is PictureBoxWithInterpolationMode)
            {

            }
            var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, 
                splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 
                0, 
                pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
            var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
            */
            Point outputPoint = new Point(intX, intY);
            return outputPoint;
        }

        private void pbxWorkspace_Click(object sender, EventArgs e)
        {
            lastCursor = Cursor.Position;
            UseTool(); //PaintWorkspace();
        }

        private void pbxWorkspace_MouseMove(object sender, MouseEventArgs e)
        {
            if (pbxWorkspace.Image != null)
            {
                //Point clientPoint = PointToClient(Cursor.Position);
                //var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
                //var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
                Point workPoint = WorkspacePosition(Cursor.Position);
                int intX = workPoint.X, intY = workPoint.Y;
                lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                if (MouseButtons == MouseButtons.Left)
                    UseTool();
            }
        }

        private void UseTool()
        {
            //Point clientPoint = PointToClient(Cursor.Position);
            //var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
            //var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
            Point workPoint = WorkspacePosition(Cursor.Position);
            int intX = workPoint.X, intY = workPoint.Y;
            if (pickerMode)
            {
                ToolPicker(intX, intY);
                return;
            }
            PenPaint(intX, intY);
        }

        private void ToolPicker(int intX, int intY)
        {
            CursorColors.Features features = CursorColors.FeaturesRendered(imgRendered.GetPixel(intX, intY));
            int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
            int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
            int tSky = features.ThisSky;
            ccPaint.Depth = tDepth;
            ccPaint.IndexID = tIndexID;
            ccPaint.EColor = tEColor;
            ccPaint.LColor = tLColor;
            ccPaint.Light = tLight;
            ccPaint.Pipe = tPipe;
            ccPaint.Grime = tGrime;
            ccPaint.Shading = tShading;
            ccPaint.Sky = tSky;
            pickerMode = false;
            nudDepth.Value = ccPaint.Depth + 1;
            btnPickIndex.BackColor = ccPaint.IndexPalette.Entries[ccPaint.IndexID];
            btnPickEColor.BackColor = ccPaint.ColorEColor();
            btnPickLColor.BackColor = ccPaint.ColorLColor();
            btnPickLight.BackColor = ccPaint.ColorLight();
            btnPickPipe.BackColor = ccPaint.ColorPipe();
            btnPickRainbow.BackColor = ccPaint.ColorGrime();
            btnPickShading.BackColor = ccPaint.ColorShading();
            btnPickSky.BackColor = ccPaint.ColorSky();
            btnColorPicker.FlatAppearance.BorderColor = Color.Black;
        }

        private void PenPaint(int intX, int intY)
        {
            if (pbxWorkspace.Image != null)
            {
                if (lastCursor.X == -1)
                {
                    lastCursor = Cursor.Position;
                }
                //Point clientPoint = PointToClient(Cursor.Position);
                //var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
                //var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
                int r = ccPaint.PenSize;
                List<Point> interp = GetBresenhamLine(WorkspacePosition(Cursor.Position), WorkspacePosition(lastCursor));
                for (int t = 0; t< interp.Count; t++)
                {
                    int cx = interp[t].X;
                    int cy = interp[t].Y;
                    for (int x = 1 - r; x < r; x++)
                    {
                        int ry = (int)Math.Max(Math.Sqrt(Math.Abs(x * x - r * r)), 1);
                        for (int y = 1 - ry; y < ry; y++)
                        {
                            int px = cx + x;
                            int py = cy + y;
                            if ((px >= 0 && px < pbxWorkspace.Image.Width) && (py >= 0 && py < pbxWorkspace.Image.Height))
                            {
                                //Console.WriteLine("got a point,Paint Pixel");
                                PaintPixel(px, py);
                            }
                        }
                    }
                }
                
                
            }
            //PaintWorkspace();
            //PaintPixel(intX, intY);
            lastCursor = Cursor.Position;
        }

        private void PaintPixel(int intX, int intY)
        {
            //Console.WriteLine("Paint Pixel");
            CursorColors.Features features = CursorColors.FeaturesRendered(imgRendered.GetPixel(intX, intY));
            int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
            int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
            int tSky = features.ThisSky;
            if (tDepth > nudMaxLayer.Value - 1 || tDepth < nudMinLayer.Value - 1)
                return;
            if (blnDepth) { tDepth = ccPaint.Depth; }
            if (blnEColor) {  tEColor = ccPaint.EColor; }
            if (blnIndex) { tIndexID = ccPaint.IndexID; }
            if (blnLColor) { tLColor = ccPaint.LColor; }
            if (blnLight) { tLight = ccPaint.Light; }
            if (blnPipe) { tPipe = ccPaint.Pipe; }
            if (blnRainbow) { tGrime = ccPaint.Grime; }
            if (blnShading) { tShading = ccPaint.Shading; }
            if (blnSky) { tSky = ccPaint.Sky; }
            var newfeatures = new CursorColors.Features(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
            if (features != newfeatures)
            {
                ccPaint.workingBuffer.Add(new BufferAction(intX, intY, features));
                //Console.WriteLine("WorkspaceSetPixel");
                //Console.WriteLine(newfeatures.ThisDepth);
                WorkspaceSetPixel(intX, intY, newfeatures);
            }
        }

        private void PaintWorkspace()
        {
            if (pbxWorkspace.Image != null)
            {
                Point clientPoint = PointToClient(Cursor.Position);
                var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
                var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
                //lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                //lblCursorCoords.Text = pbxWorkspace.Image.HorizontalResolution.ToString();

                if (true) // MouseButtons == MouseButtons.Left)
                {
                    Bitmap imgWorking = (Bitmap)pbxWorkspace.Image;
                    //Graphics graphic = Graphics.FromImage(imgWorking);
                    intY = Math.Max(Math.Min(intY, imgWorking.Size.Height - 1 + 2), 0);
                    intX = Math.Max(Math.Min(intX, imgWorking.Size.Width - 1 + 2), 0);
                    Console.WriteLine(imgRendered.GetPixel(intX, intY));
                    //imgWorking.SetPixel(intX, intY, colCursor);
                    CursorColors.Features features = CursorColors.FeaturesRendered(imgRendered.GetPixel(intX, intY));
                    int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
                    int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
                    int tSky = features.ThisSky;
                    

                    if (pickerMode)
                    {
                        ccPaint.Depth = tDepth;
                        ccPaint.IndexID = tIndexID;
                        ccPaint.EColor = tEColor;
                        ccPaint.LColor = tLColor;
                        ccPaint.Light = tLight;
                        ccPaint.Pipe = tPipe;
                        ccPaint.Grime = tGrime;
                        ccPaint.Shading = tShading;
                        ccPaint.Sky = tSky;
                        pickerMode = false;
                        nudDepth.Value = ccPaint.Depth + 1;
                        btnPickIndex.BackColor = ccPaint.IndexPalette.Entries[ccPaint.IndexID];
                        btnPickEColor.BackColor = ccPaint.ColorEColor();
                        btnPickLColor.BackColor = ccPaint.ColorLColor();
                        btnPickLight.BackColor = ccPaint.ColorLight();
                        btnPickPipe.BackColor = ccPaint.ColorPipe();
                        btnPickRainbow.BackColor = ccPaint.ColorGrime();
                        btnPickShading.BackColor = ccPaint.ColorShading();
                        btnPickSky.BackColor = ccPaint.ColorSky();
                        btnColorPicker.FlatAppearance.BorderColor = Color.Black;
                        return;
                    }
                    if (tDepth > nudMaxLayer.Value - 1 || tDepth < nudMinLayer.Value - 1)
                        return;

                    if (blnDepth) { imgDepth.SetPixel(intX, intY, ccPaint.ColorDepth()); tDepth = ccPaint.Depth; }
                    if (blnEColor) { imgEColor.SetPixel(intX, intY, ccPaint.ColorEColor()); tEColor = ccPaint.EColor; }
                    //if (blnIndex) {imgIndex.SetPixel(intX, intY, ccPaint.Index); tIndex = ccPaint.Index; }
                    if (blnIndex) { imgIndex = CursorColors.SetPixelIndexedBitmap(imgIndex, ccPaint.IndexID, intX, intY); tIndexID = ccPaint.IndexID; }
                    if (blnLColor) { imgLColor.SetPixel(intX, intY, ccPaint.ColorLColor()); tLColor = ccPaint.LColor; }
                    if (blnLight) { imgLight.SetPixel(intX, intY, ccPaint.ColorLight()); tLight = ccPaint.Light; }
                    if (blnPipe) { imgPipe.SetPixel(intX, intY, ccPaint.ColorPipe()); tPipe = ccPaint.Pipe; }
                    if (blnRainbow) { imgRainbow.SetPixel(intX, intY, ccPaint.ColorGrime()); tGrime = ccPaint.Grime; }
                    if (blnShading) { imgShading.SetPixel(intX, intY, ccPaint.ColorShading()); tShading = ccPaint.Shading; }
                    if (blnSky) { imgSky.SetPixel(intX, intY, ccPaint.ColorSky()); tSky = ccPaint.Sky; }
                    //decompose original rendered pixel and update with only what is enabled
                    //Console.WriteLine("Index " + tIndex.ToString());
                    var newfeatures = new CursorColors.Features(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
                    if (features != newfeatures)
                    {
                        ccPaint.workingBuffer.Add(new BufferAction(intX, intY, features));
                        //ccPaint.AddToUndoBuffer(new BufferAction(intX, intY, features));
                    }
                    Color colRend = CursorColors.ColorRendered(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
                    imgRendered.SetPixel(intX, intY, colRend);
                    imgPreview.SetPixel(intX, intY, colRend);
                    //pbxWorkspace.Image = imgWorking;
                    RefreshWorkspace();
                }
            }
        }

        private void WorkspaceSetPixel(int intX, int intY, CursorColors.Features features)//sets a pixel in all images regardless of layer selection
        {
            changed = true;
            imgDepth.SetPixel(intX, intY, CursorColors.ToDepth(features.ThisDepth));
            imgEColor.SetPixel(intX, intY, CursorColors.ToEColor(features.ThisEColor));
            imgIndex = CursorColors.SetPixelIndexedBitmap(imgIndex, features.ThisIndexID, intX, intY);
            imgLColor.SetPixel(intX, intY, CursorColors.ToLColor(features.ThisLColor));
            imgLight.SetPixel(intX, intY, CursorColors.ToLight(features.ThisLight));
            imgPipe.SetPixel(intX, intY, CursorColors.ToPipe(features.ThisPipe));
            imgRainbow.SetPixel(intX, intY, CursorColors.ToGrime(features.ThisGrime));
            imgShading.SetPixel(intX, intY, CursorColors.ToShading(features.ThisSky));
            imgSky.SetPixel(intX, intY, CursorColors.ToSky(features.ThisSky));
            Color colRend = CursorColors.ColorRendered(features);
            imgRendered.SetPixel(intX, intY, colRend);
            imgPreview.SetPixel(intX, intY, colRend);
            RefreshWorkspace();
        }

        private void FastCompose()
        {
            int w = imgRendered.Width;
            int h = imgRendered.Height;

            Size s = imgDepth.Size;
            PixelFormat fmt = imgDepth.PixelFormat;
            // we need the bit depth and we assume either 32bppArgb or 24bppRgb !
            byte bpp = (byte)4;//(fmt == PixelFormat.Format32bppArgb ? 4 : 3);
            // lock the bits and prepare the loop
            Rectangle rect = new Rectangle(Point.Empty, s);
            BitmapData bmpData0 = imgDepth.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData1 = imgEColor.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData2 = imgIndex.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData bmpData3 = imgLColor.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData4 = imgLight.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData5 = imgPipe.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData6 = imgRainbow.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData7 = imgShading.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData8 = imgSky.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            BitmapData bmpData9 = imgRendered.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            int size1 = bmpData9.Stride * bmpData9.Height;
            int size2 = bmpData2.Stride * bmpData2.Height;
            byte[] data0 = new byte[size1];
            byte[] data1 = new byte[size1];
            byte[] data2 = new byte[bmpData2.Stride * s.Height];
            //byte[] data2 = new byte[size2];
            byte[] data3 = new byte[size1];
            byte[] data4 = new byte[size1];
            byte[] data5 = new byte[size1];
            byte[] data6 = new byte[size1];
            byte[] data7 = new byte[size1];
            byte[] data8 = new byte[size1];
            byte[] data9 = new byte[size1];
            System.Runtime.InteropServices.Marshal.Copy(bmpData0.Scan0, data0, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data1, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData2.Scan0, data2, 0, data2.Length);
            System.Runtime.InteropServices.Marshal.Copy(bmpData3.Scan0, data3, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData4.Scan0, data4, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData5.Scan0, data5, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData6.Scan0, data6, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData7.Scan0, data7, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData8.Scan0, data8, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData9.Scan0, data9, 0, size1);
            // loops
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    
                    // calculate the index
                    int index = y * bmpData1.Stride + x * bpp;
                    //int index = y * bmpData1.Stride + x * bpp;
                    // get the color
                    Color c = Color.FromArgb(data1[index + 3], data1[index + 2], data1[index + 1], data1[index]);//bpp == 4 ? data[index + 3] : 255,data[index + 2], data[index + 1], data[index]);
                    Color c0 = Color.FromArgb(data0[index + 3], data0[index + 2], data0[index + 1], data0[index]);
                    Color c1 = Color.FromArgb(data1[index + 3], data1[index + 2], data1[index + 1], data1[index]);
                    //Color c2 = Color.FromArgb(data2[index + 3]);
                    //int c2 = data2[]
                    int idx = data2[y * bmpData2.Stride + x];
                    Color c3 = Color.FromArgb(data3[index + 3], data3[index + 2], data3[index + 1], data3[index]);
                    Color c4 = Color.FromArgb(data4[index + 3], data4[index + 2], data4[index + 1], data4[index]);
                    Color c5 = Color.FromArgb(data5[index + 3], data5[index + 2], data5[index + 1], data5[index]);
                    Color c6 = Color.FromArgb(data6[index + 3], data6[index + 2], data6[index + 1], data6[index]);
                    Color c7 = Color.FromArgb(data7[index + 3], data7[index + 2], data7[index + 1], data7[index]);
                    Color c8 = Color.FromArgb(data8[index + 3], data8[index + 2], data8[index + 1], data8[index]);
                    Color c9 = ColorRendered(FeaturesFromColors(c0, idx, c1,c3,c4,c5,c6,c7,c8));
                    //if (x == 900 && y == 100)
                    //    Console.WriteLine("compose at " + x.ToString() + " ," + y.ToString()+" "+ data8[index + 3].ToString());
                    // process it
                    //c = hueChanger(c, 2);
                    // set the channels from the new color
                    data9[index + 0] = c9.B;
                    data9[index + 1] = c9.G;
                    data9[index + 2] = c9.R;
                    data9[index + 3] = c9.A;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(data9, 0, bmpData9.Scan0, data9.Length);
            imgDepth.UnlockBits(bmpData0);
            imgEColor.UnlockBits(bmpData1);
            imgIndex.UnlockBits(bmpData2);
            imgLColor.UnlockBits(bmpData3);
            imgLight.UnlockBits(bmpData4);
            imgPipe.UnlockBits(bmpData5);
            imgRainbow.UnlockBits(bmpData6);
            imgShading.UnlockBits(bmpData7);
            imgSky.UnlockBits(bmpData8);
            imgRendered.UnlockBits(bmpData9);
            //Console.WriteLine("Done Composing");
            MakePreview();
        }

        private void FastDecompose()
        {
            int w = imgRendered.Width;
            int h = imgRendered.Height;

            Size s = imgDepth.Size;
            PixelFormat fmt = imgDepth.PixelFormat;
            // we need the bit depth and we assume either 32bppArgb or 24bppRgb !
            byte bpp = (byte)4;//(fmt == PixelFormat.Format32bppArgb ? 4 : 3);
            // lock the bits and prepare the loop
            Rectangle rect = new Rectangle(Point.Empty, s);
            BitmapData bmpData0 = imgDepth.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData1 = imgEColor.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData2 = imgIndex.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            BitmapData bmpData3 = imgLColor.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData4 = imgLight.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData5 = imgPipe.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData6 = imgRainbow.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData7 = imgShading.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData8 = imgSky.LockBits(rect, ImageLockMode.ReadWrite, fmt);
            BitmapData bmpData9 = imgRendered.LockBits(rect, ImageLockMode.ReadOnly, fmt);
            int size1 = bmpData9.Stride * bmpData9.Height;
            int size2 = bmpData2.Stride * bmpData2.Height;
            byte[] data0 = new byte[size1];
            byte[] data1 = new byte[size1];
            byte[] data2 = new byte[bmpData2.Stride * s.Height];
            byte[] data3 = new byte[size1];
            byte[] data4 = new byte[size1];
            byte[] data5 = new byte[size1];
            byte[] data6 = new byte[size1];
            byte[] data7 = new byte[size1];
            byte[] data8 = new byte[size1];
            byte[] data9 = new byte[size1];
            System.Runtime.InteropServices.Marshal.Copy(bmpData0.Scan0, data0, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, data1, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData2.Scan0, data2, 0, data2.Length);
            System.Runtime.InteropServices.Marshal.Copy(bmpData3.Scan0, data3, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData4.Scan0, data4, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData5.Scan0, data5, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData6.Scan0, data6, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData7.Scan0, data7, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData8.Scan0, data8, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmpData9.Scan0, data9, 0, size1);
            // loops
            for (int y = 0; y < s.Height; y++)
            {
                for (int x = 0; x < s.Width; x++)
                {
                    // calculate the index
                    int index = y * bmpData1.Stride + x * bpp;
                    //int index = y * bmpData1.Stride + x * bpp;
                    // get the color
                    Color c9 = Color.FromArgb(data9[index + 3], data9[index + 2], data9[index + 1], data9[index]);
                    CursorColors.Features features = CursorColors.FeaturesRendered(c9);
                    int tDepth = features.ThisDepth; int tIndexID = features.ThisIndexID; int tEColor = features.ThisEColor; int tLColor = features.ThisLColor;
                    int tLight = features.ThisLight; int tPipe = features.ThisPipe; int tGrime = features.ThisGrime; int tShading = features.ThisShading;
                    int tSky = features.ThisSky;

                    //ccPaint.Depth = tDepth;
                    //ccPaint.IndexID = tIndexID;
                    //ccPaint.EColor = tEColor;
                    //ccPaint.LColor = tLColor;
                    //ccPaint.Light = tLight;
                    //ccPaint.Pipe = tPipe;
                    //ccPaint.Grime = tGrime;
                    //ccPaint.Shading = tShading;
                    //ccPaint.Sky = tSky;
                    if (tIndexID != 0)
                    {
                        ccPaint.IndexPalette.Entries[tIndexID] = Color.FromArgb(data9[(255 - tIndexID) * bpp + 3], data9[(255 - tIndexID) * bpp + 2], data9[(255 - tIndexID) * bpp + 1], data9[(255 - tIndexID) * bpp]);
                    }
                    Color c0 = CursorColors.ToDepth(tDepth);//Color.FromArgb(data0[index + 3], data0[index + 2], data0[index + 1], data0[index]);
                    Color c1 = CursorColors.ToEColor(tEColor);//Color.FromArgb(data1[index + 3], data1[index + 2], data1[index + 1], data1[index]);
                    //int idx = data2[y * bmpData2.Stride + x];
                    Color c3 = CursorColors.ToLColor(tLColor);//Color.FromArgb(data3[index + 3], data3[index + 2], data3[index + 1], data3[index]);
                    Color c4 = CursorColors.ToLight(tLight);//Color.FromArgb(data4[index + 3], data4[index + 2], data4[index + 1], data4[index]);
                    Color c5 = CursorColors.ToPipe(tPipe);//Color.FromArgb(data5[index + 3], data5[index + 2], data5[index + 1], data5[index]);
                    Color c6 = CursorColors.ToGrime(tGrime);//Color.FromArgb(data6[index + 3], data6[index + 2], data6[index + 1], data6[index]);
                    Color c7 = CursorColors.ToShading(tShading);//Color.FromArgb(data7[index + 3], data7[index + 2], data7[index + 1], data7[index]);
                    Color c8 = CursorColors.ToSky(tSky);//Color.FromArgb(data8[index + 3], data8[index + 2], data8[index + 1], data8[index]);

                    //Color c9 = ColorRendered(FeaturesFromColors(c0, idx, c1, c3, c4, c5, c6, c7, c8));
                    data0[index + 0] = c0.B;data0[index + 1] = c0.G;data0[index + 2] = c0.R;data0[index + 3] = c0.A;
                    data1[index + 0] = c1.B; data1[index + 1] = c1.G; data1[index + 2] = c1.R; data1[index + 3] = c1.A;
                    data2[y * bmpData2.Stride + x] = (byte)tIndexID;
                    data3[index + 0] = c3.B; data3[index + 1] = c3.G; data3[index + 2] = c3.R; data3[index + 3] = c3.A;
                    data4[index + 0] = c4.B; data4[index + 1] = c4.G; data4[index + 2] = c4.R; data4[index + 3] = c4.A;
                    data5[index + 0] = c5.B; data5[index + 1] = c5.G; data5[index + 2] = c5.R; data5[index + 3] = c5.A;
                    data6[index + 0] = c6.B; data6[index + 1] = c6.G; data6[index + 2] = c6.R; data6[index + 3] = c6.A;
                    data7[index + 0] = c7.B; data7[index + 1] = c7.G; data7[index + 2] = c7.R; data7[index + 3] = c7.A;
                    data8[index + 0] = c8.B; data8[index + 1] = c8.G; data8[index + 2] = c8.R; data8[index + 3] = c8.A;
                    //data9[index + 0] = c9.B;
                    //data9[index + 1] = c9.G;
                    //data9[index + 2] = c9.R;
                    //data9[index + 3] = c9.A;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(data0, 0, bmpData0.Scan0, data0.Length);
            System.Runtime.InteropServices.Marshal.Copy(data1, 0, bmpData1.Scan0, data1.Length);
            System.Runtime.InteropServices.Marshal.Copy(data2, 0, bmpData2.Scan0, data2.Length);
            System.Runtime.InteropServices.Marshal.Copy(data3, 0, bmpData3.Scan0, data3.Length);
            System.Runtime.InteropServices.Marshal.Copy(data4, 0, bmpData4.Scan0, data4.Length);
            System.Runtime.InteropServices.Marshal.Copy(data5, 0, bmpData5.Scan0, data5.Length);
            System.Runtime.InteropServices.Marshal.Copy(data6, 0, bmpData6.Scan0, data6.Length);
            System.Runtime.InteropServices.Marshal.Copy(data7, 0, bmpData7.Scan0, data7.Length);
            System.Runtime.InteropServices.Marshal.Copy(data8, 0, bmpData8.Scan0, data8.Length);
            imgDepth.UnlockBits(bmpData0);
            imgEColor.UnlockBits(bmpData1);
            imgIndex.UnlockBits(bmpData2);
            imgLColor.UnlockBits(bmpData3);
            imgLight.UnlockBits(bmpData4);
            imgPipe.UnlockBits(bmpData5);
            imgRainbow.UnlockBits(bmpData6);
            imgShading.UnlockBits(bmpData7);
            imgSky.UnlockBits(bmpData8);
            imgRendered.UnlockBits(bmpData9);
            //Console.WriteLine("Done Composing");
            MakePreview();
        }

        private void RenderFromComponenets()
        {
            for (int i = 0; i < imgRendered.Height; i++)
            {
                for (int j = 0; j < imgRendered.Width; j++)
                {
                    Color colPixel = imgRendered.GetPixel(j, i);
                    CursorColors.Features features = FeaturesFromColors(
                        imgDepth.GetPixel(j, i),
                        GetPixelIndexedBitmap(imgIndex, j, i),
                        imgEColor.GetPixel(j, i),
                        imgLColor.GetPixel(j, i),
                        imgLight.GetPixel(j, i),
                        imgPipe.GetPixel(j, i),
                        imgRainbow.GetPixel(j, i),
                        imgShading.GetPixel(j, i),
                        imgSky.GetPixel(j, i)
                    );
                    Console.WriteLine("got features from " + j.ToString() + ", " + i.ToString());
                    imgRendered.SetPixel(j, i, CursorColors.ColorRendered(features));
                }
            }
        }

        private void MakePreview()
        {
            for (int y = 0; y < imgRendered.Height; y++)
            {
                for (int x = 0; x < imgRendered.Width; x++)
                {
                    Color c = ccPaint.PreviewPixel(imgRendered.GetPixel(x, y), x, y);
                    imgPreview.SetPixel(x, y, c);
                }
            }
        }

        private void RefreshWorkspace()
        {
            switch (intMode)
            {
                case 0:
                    pbxWorkspace.Image = imgDepth;
                    break;
                case 1:
                    pbxWorkspace.Image = imgEColor;
                    break;
                case 2:
                    pbxWorkspace.Image = imgIndex;
                    break;
                case 3:
                    pbxWorkspace.Image = imgLColor;
                    break;
                case 4:
                    pbxWorkspace.Image = imgLight;
                    break;
                case 5:
                    pbxWorkspace.Image = imgPipe;
                    break;
                case 6:
                    pbxWorkspace.Image = imgRainbow;
                    break;
                case 7:
                    pbxWorkspace.Image = imgShading;
                    break;
                case 8:
                    pbxWorkspace.Image = imgSky;
                    break;
                case 9:
                    if (paletteMode)
                    {
                        if (true)
                        {
                            //MakePreview();
                            changed = false;
                        }
                        pbxWorkspace.Image = imgPreview;
                    }
                    else
                    {
                        pbxWorkspace.Image = imgRendered;
                    }
                    break;
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            FastCompose();
            //RenderFromComponenets();
            RefreshWorkspace();
        }

        private void btnDecompose_Click(object sender, EventArgs e)
        {
            if (imgRendered == null)
            {
                return;
            }
            if (imgDepth !=null || imgEColor != null || imgIndex != null || imgLColor != null || imgLight != null || imgPipe != null || imgRainbow != null || imgShading != null || imgSky != null)
            {
                DialogResult dialogResult = MessageBox.Show("Overwrite components?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            tlblMessages.Text = "Decomposing rendered screen into components.";
            FastDecompose();
            imgIndex.Palette = ccPaint.IndexPalette;
            MakePreview();
            RefreshWorkspace();
            tlblMessages.Text = "Ready";
        }

        private void btnPickIndex_Click(object sender, EventArgs e)
        {
            using (var form = new IndexPopup(ccPaint.IndexPalette,ccPaint.IndexID))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ColorPalette cPal = form.IndexPalette;
                    int selID = form.selectedColorID;
                    ccPaint.IndexPalette = cPal;
                    imgIndex.Palette = cPal;
                    ccPaint.IndexID = selID;
                    btnPickIndex.BackColor = ccPaint.IndexPalette.Entries[ccPaint.IndexID];
                    //Console.WriteLine("IndexID "+selID.ToString());
                    toolTip.SetToolTip(btnPickIndex, selID.ToString());
                    MakePreview();
                }
            }
        }

        private void btnPickEColor_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.EColor;//btnPickEColor.BackColor;
            Color EffectA = Color.FromArgb(255, 0, 255);
            Color EffectB = Color.FromArgb(0, 255, 255);
            Color EffectC = Color.FromArgb(255, 255, 255);
            Color Off = Color.FromArgb(0, 0, 0);
            if (colInitial == CursorColors.EffectColorA)
            {
                btnPickEColor.BackColor = EffectB;
                ccPaint.EColor = CursorColors.EffectColorB;
                toolTip.SetToolTip(btnPickEColor, "Effect Color B");
            }
            else if (colInitial == CursorColors.EffectColorB)
            {
                btnPickEColor.BackColor = EffectC;
                ccPaint.EColor = CursorColors.EffectColorC;
                toolTip.SetToolTip(btnPickEColor, "Effect Color Batfly Hive");
            }
            else if (colInitial == CursorColors.EffectColorC)
            {
                btnPickEColor.BackColor = Off;
                ccPaint.EColor = CursorColors.NoEffectColor;
                toolTip.SetToolTip(btnPickEColor, "Effect Color Off");
            }
            else if (colInitial == CursorColors.NoEffectColor)
            {
                btnPickEColor.BackColor = EffectA;
                ccPaint.EColor = CursorColors.EffectColorA;
                toolTip.SetToolTip(btnPickEColor, "Effect Color A");
            }
            else
            {
                btnPickEColor.BackColor = Off;
                ccPaint.EColor = CursorColors.NoEffectColor;
                toolTip.SetToolTip(btnPickEColor, "Effect Color Off");
            }
        }

        private void btnPickLColor_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.LColor;//btnPickLColor.BackColor;
            Color Light = Color.FromArgb(0, 0, 255);
            Color Neutral = Color.FromArgb(0, 255, 0);
            Color Dark = Color.FromArgb(255, 0, 0);
            if (colInitial == CursorColors.GeometryLight)
            {
                btnPickLColor.BackColor = Neutral;
                ccPaint.LColor = CursorColors.GeometryNeutral;
                toolTip.SetToolTip(btnPickLColor, "Neutral");
            }
            else if (colInitial == CursorColors.GeometryNeutral)
            {
                btnPickLColor.BackColor = Dark;
                ccPaint.LColor = CursorColors.GeometryDark;
                toolTip.SetToolTip(btnPickLColor, "Dark");
            }
            else if (colInitial == CursorColors.GeometryDark)
            {
                btnPickLColor.BackColor = Light;
                ccPaint.LColor = CursorColors.GeometryLight;
                toolTip.SetToolTip(btnPickLColor, "Light");
            }
            else
            {
                btnPickLColor.BackColor = Neutral;
                ccPaint.LColor = CursorColors.GeometryNeutral;
                toolTip.SetToolTip(btnPickLColor, "Neutral");
            }
        }

        private void btnPickLight_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.Light;//btnPickLight.BackColor;
            Color Light = Color.FromArgb(255, 255, 255);
            Color Dark = Color.FromArgb(0, 0, 0);
            if (colInitial == CursorColors.LightOn)
            {
                ccPaint.Light = CursorColors.LightOff;
                btnPickLight.BackColor = Dark;
                toolTip.SetToolTip(btnPickLight, "Shadow");
            }
            else if (colInitial == CursorColors.LightOff)
            {
                btnPickLight.BackColor = Light;
                ccPaint.Light = CursorColors.LightOn;
                toolTip.SetToolTip(btnPickLight, "Sunlight");
            }
            else
            {
                btnPickLight.BackColor = Light;
                ccPaint.Light = CursorColors.LightOn;
                toolTip.SetToolTip(btnPickLight, "Sunlight");

            }
        }

        private void btnPickPipe_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.Pipe;//btnPickPipe.BackColor;
            Color L1 = CursorColors.ToPipe(CursorColors.PipeL1);
            Color L2 = CursorColors.ToPipe(CursorColors.PipeL2);
            Color L3 = CursorColors.ToPipe(CursorColors.PipeL3);
            Color Off = CursorColors.ToPipe(CursorColors.NoPipe);
            if (colInitial == CursorColors.PipeL1)
            {
                ccPaint.Pipe = CursorColors.PipeL2;
                btnPickPipe.BackColor = L2;
                toolTip.SetToolTip(btnPickPipe, "Pipe Layer 2");
            }
            else if (colInitial == CursorColors.PipeL2)
            {
                ccPaint.Pipe = CursorColors.PipeL3;
                btnPickPipe.BackColor = L3;
                toolTip.SetToolTip(btnPickPipe, "Pipe Layer 3");
            }
            else if (colInitial == CursorColors.PipeL3)
            {
                ccPaint.Pipe = CursorColors.NoPipe;
                btnPickPipe.BackColor = Off;
                toolTip.SetToolTip(btnPickPipe, "No Pipes");
            }
            else if (colInitial == CursorColors.NoPipe)
            {
                ccPaint.Pipe = CursorColors.PipeL1;
                btnPickPipe.BackColor = L1;
                toolTip.SetToolTip(btnPickPipe, "Pipe Layer 1");
            }
            else
            {
                ccPaint.Pipe = CursorColors.NoPipe;
                btnPickPipe.BackColor = Off;
                toolTip.SetToolTip(btnPickPipe, "No Pipes");
            }
        }

        private void btnPickRainbow_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.Grime;//btnPickRainbow.BackColor;
            Color On = Color.FromArgb(255, 255, 255);
            Color Off = Color.FromArgb(0, 0, 0);
            if (colInitial == CursorColors.GrimeOn)
            {
                ccPaint.Grime = CursorColors.GrimeOff;
                btnPickRainbow.BackColor = Off;
                toolTip.SetToolTip(btnPickRainbow, "Grime Off");
            }
            else if (colInitial == CursorColors.GrimeOff)
            {
                ccPaint.Grime = CursorColors.GrimeOn;
                btnPickRainbow.BackColor = On;
                toolTip.SetToolTip(btnPickRainbow, "Grime On");
            }
            else
            {
                ccPaint.Grime = CursorColors.GrimeOff;
                btnPickRainbow.BackColor = Off;
                toolTip.SetToolTip(btnPickRainbow, "Grime Off");
            }
        }

        private void btnPickShading_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color colSelection = colorDialog1.Color;
            ccPaint.Shading = (int)(colSelection.GetBrightness()*255);
            btnPickShading.BackColor = Color.FromArgb(ccPaint.Shading, ccPaint.Shading, ccPaint.Shading);
            colorDialog1.Color = btnPickShading.BackColor;
            toolTip.SetToolTip(btnPickShading, ccPaint.Shading.ToString());
        }

        private void btnPickSky_Click(object sender, EventArgs e)
        {
            int colInitial = ccPaint.Sky;//btnPickSky.BackColor;
            Color Sky = Color.FromArgb(255, 255, 255);
            Color Geometry = Color.FromArgb(0, 0, 0);
            if (colInitial == CursorColors.SkyOn)
            {
                ccPaint.Sky = CursorColors.SkyOff;
                btnPickSky.BackColor = Geometry;
                toolTip.SetToolTip(btnPickSky, "Geometry");
            }
            else if (colInitial == CursorColors.SkyOff)
            {
                ccPaint.Sky = CursorColors.SkyOn;
                btnPickSky.BackColor = Sky;
                toolTip.SetToolTip(btnPickSky, "Sky");
            }
            else
            {
                ccPaint.Sky = CursorColors.SkyOff;
                btnPickSky.BackColor = Geometry;
                toolTip.SetToolTip(btnPickSky, "Geometry");
            }
        }

        private void btnPickDepth_Click(object sender, EventArgs e)
        {

        }

        private void nudDepth_ValueChanged(object sender, EventArgs e)
        {
            ccPaint.Depth = (int)(nudDepth.Value - 1);
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            ccPaint.IndexID = 0;
            toolTip.SetToolTip(btnPickIndex, "0");
        }

        private Bitmap StampIndexes(Bitmap bitmap)
        {
            for (int i = Math.Min(255, bitmap.Width); i > 0; i--)
            {
                if (ccPaint.IndexPalette.Entries[i] != Color.Transparent)
                {
                    bitmap.SetPixel(i, 0, ccPaint.IndexPalette.Entries[i]);
                }
            }
            return bitmap;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(strFileName != null && strFilePath != null)
            {
                StampIndexes(imgRendered).Save(strFilePath+@"\"+strFileName+".png", ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("No file name", "error", MessageBoxButtons.OK);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StampIndexes(imgRendered).Save(sfd.FileName, ImageFormat.Png);
                strFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                strFilePath = Path.GetDirectoryName(sfd.FileName);
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void saveACopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StampIndexes(imgRendered).Save(sfd.FileName, ImageFormat.Png);
            }
        }

        private void exportLayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(strFileName != null && strFilePath != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Images|*.png";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //StampIndexes(imgRendered).Save(sfd.FileName, ImageFormat.Png);
                    //string folderName = strFilePath;//folderBrowserDialog1.SelectedPath;
                    List<Image> images = new List<Image> { imgDepth, imgEColor, imgIndex, imgLColor, imgLight, imgPipe, imgRainbow, imgShading, imgSky };
                    List<string> subnames = new List<string> { "_depth", "_ecolor", "_index", "_lcolor", "_light", "_pipe", "_grime", "_shading", "_sky" };
                    for (int i = 0; i < images.Count; i++)
                    {
                        string nameOut = strFilePath + @"\" + strFileName + subnames[i] + ".png";
                        images[i].Save(nameOut, ImageFormat.Png);
                        Console.WriteLine(nameOut);
                    }
                }
            }
        }

        private void nudMinLayer_ValueChanged(object sender, EventArgs e)
        {
            if(nudMinLayer.Value > nudMaxLayer.Value)
            {
                nudMinLayer.Value=nudMaxLayer.Value;
            }
        }

        private void nudMaxLayer_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxLayer.Value < nudMinLayer.Value)
            {
                nudMaxLayer.Value = nudMinLayer.Value;
            }
        }

        /*protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                Undo();
                //MessageBox.Show("What the Ctrl+F?");
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.Z))
            {
                Redo();
                //MessageBox.Show("What the Ctrl+F?");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }*/

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            if (ccPaint.undoBuffer.Count > 0)
            {
                var act = ccPaint.undoBuffer[ccPaint.undoBuffer.Count - 1];
                var oldState = ApplyBufferActs(act);
                ccPaint.AddToRedoBuffer(oldState);
                ccPaint.RemoveFromUndoBuffer();
            }
        }


        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void Redo()
        {
            if(ccPaint.redoBuffer.Count > 0)
            {
                var act = ccPaint.redoBuffer[ccPaint.redoBuffer.Count - 1];
                var oldState = ApplyBufferActs(act);
                ccPaint.AddToUndoBuffer(oldState);
                ccPaint.RemoveFromRedoBuffer();
            }
        }

        private List<CursorColors.BufferAction> ApplyBufferActs(List<CursorColors.BufferAction>  act)
        {
            var oldState = new List<CursorColors.BufferAction>(act);
            for (int i = 0; i < act.Count; i++)
            {
                var (x, y, features) = (act[i].X, act[i].Y, act[i].Components);
                CursorColors.Features features2 = CursorColors.FeaturesRendered(imgRendered.GetPixel(x, y));
                oldState[i] = new CursorColors.BufferAction(x, y, features2);
                WorkspaceSetPixel(x, y, features);
            }
            return oldState;
        }

        private void paletteToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (paletteToolStripMenuItem.Checked)
            {
                paletteMode = true;
                RefreshWorkspace();
            }
            else
            {
                paletteMode = false;
                RefreshWorkspace();
            }
        }

        private void setPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                if(myBitmap.Width == 32 && (myBitmap.Height == 16 || myBitmap.Height == 8))
                {
                    if(myBitmap.Height == 8)
                    {
                        //myBitmap = myBitmap;
                    }
                    else
                    {
                        myBitmap = cropAtRect(myBitmap, new Rectangle(0, 0, 32, 8));
                    }
                    //Console.WriteLine("a");
                    //imgPalette = new Bitmap(32, 8, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    //Console.WriteLine(myBitmap.PixelFormat);
                    ccPaint.imgPalette = myBitmap;//(Bitmap)ConvertToIndexed(myBitmap);
                    //using (Graphics gr = Graphics.FromImage(imgPalette))
                    //{
                    //Console.WriteLine(PixelFormat);
                    //gr.DrawImage(myBitmap, new Rectangle(0, 0, 32, 8));
                    //}
                }
                else
                {
                    MessageBox.Show("wrong resolution", "error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
            }
        }

        private void effectAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                ccPaint.colA = colorDialog1.Color;
                changed = true;
            }
        }

        private void effectBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                ccPaint.colB = colorDialog1.Color;
                changed = true;
            }
        }

        private void nudPenSize_ValueChanged(object sender, EventArgs e)
        {
            ccPaint.PenSize = (int)nudPenSize.Value;
        }
    }
}
