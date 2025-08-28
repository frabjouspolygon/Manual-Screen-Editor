using Manual_Screen_Renderer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Media;

//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using static Manual_Screen_Renderer.CursorColors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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
        static int intMode = 0;//0-8
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
            //pbxWorkspace.SizeMode = PictureBoxSizeMode.AutoSize;
            //splitContainer1.Panel2.AutoScroll = true;
            pnlWorkspace.AutoScroll = true;
            pbxWorkspace.MouseWheel += pbxWorkspace_MouseWheel;
            //pbxWorkspace.
            pbxWorkspace.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

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
        }

        public static double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s-a1)*(b2-b1)/(a2-a1);

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

        

        public string ImageDialogue()
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:/Users/tytro/Pictures/";
                openFileDialog.Filter = "png files (*.png)|*.png";
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
            string filePath = ImageDialogue();
            Bitmap myBitmap = null;
            try
            {
                myBitmap = new Bitmap(filePath);
                //txtDepth.Text = filePath;
                //imgDepth = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                //txtDepth.Text = "";
                //imgDepth = null;
            }
            finally
            {
                txtDepth.Text = filePath;
                imgDepth = myBitmap;
            }
        }
        private void btnEColor_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtEColor.Text = filePath;
                imgEColor = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtEColor.Text = "";
                imgEColor = null;
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
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtIndex.Text = filePath;
                imgIndex = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtIndex.Text = "";
                imgIndex = null;
            }
        }

        private void btnLColor_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtLColor.Text = filePath;
                imgLColor = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtLColor.Text = "";
                imgLColor = null;
            }
        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtLight.Text = filePath;
                imgLight = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtLight.Text = "";
                imgLight = null;
            }
        }

        private void btnPipe_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtPipe.Text = filePath;
                imgPipe = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtPipe.Text = "";
                imgPipe = null;
            }
        }

        private void btnRainbow_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtRainbow.Text = filePath;
                imgRainbow = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtRainbow.Text = "";
                imgRainbow = null;
            }
        }

        private void btnShading_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtShading.Text = filePath;
                imgShading = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtShading.Text = "";
                imgShading = null;
            }
        }

        private void btnSky_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtSky.Text = filePath;
                imgSky = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtSky.Text = "";
                imgSky = null;
            }
        }

        private void btnRendered_Click(object sender, EventArgs e)
        {
            string filePath = ImageDialogue();
            try
            {
                var myBitmap = new Bitmap(filePath);
                txtRendered.Text = filePath;
                imgRendered = myBitmap;
            }
            catch
            {
                MessageBox.Show("could not read file", "error", MessageBoxButtons.OK);
                txtRendered.Text = "";
                imgRendered = null;
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
                        //if (newHeight >= pbxWorkspace.Image.Height && newWidth >= pbxWorkspace.Image.Width && height/pbxWorkspace.Image.Height< 10000 && width/pbxWorkspace.Image.Width < 10000)
                        
                        if (Math.Sign(e.Delta)>=0 && newHeight <= pbxWorkspace.Image.Height*20)
                        {
                            pbxWorkspace.Size = new Size(newWidth, newHeight);
                            pbxWorkspace.Refresh();
                        }
                        else if (Math.Sign(e.Delta)<=0 && pbxWorkspace.Image.Height/newHeight  < 4)
                        {
                            lblMessages.Text = (newHeight).ToString() + "," + (pbxWorkspace.Image.Height).ToString();
                            pbxWorkspace.Size = new Size(newWidth, newHeight);
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

        private void pbxWorkspace_Click(object sender, EventArgs e)
        {
            PaintWorkspace();
        }

        private void pbxWorkspace_MouseMove(object sender, MouseEventArgs e)
        {
            if (pbxWorkspace.Image != null)
            {
                Point clientPoint = PointToClient(Cursor.Position);
                var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
                var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
                lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                if (MouseButtons == MouseButtons.Left)
                    PaintWorkspace();
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
                    //var newfeatures = new CursorColors.Features(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky);
                    ccPaint.AddToUndoBuffer(intX, intY, features);
                    imgRendered.SetPixel(intX, intY, CursorColors.ColorRendered(tDepth, tIndexID, tEColor, tLColor, tLight, tPipe, tGrime, tShading, tSky));
                    
                    //pbxWorkspace.Image = imgWorking;
                    RefreshWorkspace();
                }
            }
        }

        private void WorkspaceSetPixel(int intX, int intY, CursorColors.Features features)
        {
            //var (intX, intY, features) = (act.X, act.Y, act.Components);
            imgRendered.SetPixel(intX, intY, CursorColors.ColorRendered(features));
            RefreshWorkspace();
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
                    pbxWorkspace.Image = imgRendered;
                    break;
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {

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
            lblMessages.Text = "Decomposing rendered screen into components.";
            imgDepth = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgEColor = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgLColor = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgLight = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgPipe = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgRainbow = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgShading = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgSky = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppRgb);
            imgIndex = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format8bppIndexed);

            for (int j = Math.Min(255, imgRendered.Width); j > 0; j--)
            {
                ccPaint.IndexPalette.Entries[j] = imgRendered.GetPixel(j, 0);
            }
            for (int i = 0; i<imgRendered.Height; i++)
            {
                for (int j = 0; j< imgRendered.Width; j++)
                {
                    Color colPixel = imgRendered.GetPixel(j,i);
                    CursorColors.Features features = CursorColors.FeaturesRendered(imgRendered.GetPixel(j, i));
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
                    if(tIndexID != 0)
                    {
                        ccPaint.IndexPalette.Entries[tIndexID] = imgRendered.GetPixel(255- tIndexID, 0);
                        imgIndex.SetPixel(j, i, ccPaint.IndexPalette.Entries[tIndexID]);
                    }
                    else
                    {
                        imgIndex.SetPixel(j, i, Color.Transparent);
                    }
                    imgSky.SetPixel(j, i, ccPaint.ColorSky());
                    imgLight.SetPixel(j, i, ccPaint.ColorLight());
                    imgLColor.SetPixel(j, i, ccPaint.ColorLColor());
                    imgDepth.SetPixel(j, i, ccPaint.ColorDepth());
                    imgRainbow.SetPixel(j, i, ccPaint.ColorGrime());
                    imgEColor.SetPixel(j, i, ccPaint.ColorEColor());
                    imgShading.SetPixel(j, i, ccPaint.ColorShading());
                }//end for width
            }//end for height


            lblMessages.Text = "Ready";
        }

        private void btnPickIndex_Click(object sender, EventArgs e)
        {
            //IndexPopup stuff = new IndexPopup(ccPaint.IndexPalette);
            //stuff.Show();

            using (var form = new IndexPopup(ccPaint.IndexPalette,ccPaint.IndexID))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ColorPalette val = form.IndexPalette;
                    int val2 = form.selectedColorID;
                    ccPaint.IndexPalette = val;
                    imgIndex.Palette = val;
                    ccPaint.IndexID = val2;
                    btnPickIndex.BackColor = ccPaint.IndexPalette.Entries[ccPaint.IndexID];
                    Console.WriteLine("IndexID "+val2.ToString());
                    toolTip.SetToolTip(btnPickIndex, val2.ToString());
                }
            }

            //colorDialog1.ShowDialog();
            //Color colSelection = colorDialog1.Color;
            //ccPaint.Index = colSelection;
            //btnPickIndex.BackColor = colSelection;
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
            Color L1 = Color.FromArgb(255, 0, 0);
            Color L2 = Color.FromArgb(0, 255, 0);
            Color L3 = Color.FromArgb(0, 0, 255);
            Color Off = Color.FromArgb(0, 0, 0);
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
                var folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderName = folderBrowserDialog1.SelectedPath;
                    List<Image> images = new List<Image> { imgDepth, imgEColor, imgIndex, imgLColor, imgLight, imgPipe, imgRainbow, imgShading, imgSky };
                    List<string> subnames = new List<string> { "_depth", "_ecolor", "_index", "_lcolor", "_light", "_pipe", "_grime", "_shading", "_sky" };
                    for (int i = 0; i < images.Count; i++)
                    {
                        images[i].Save(strFilePath + @"\" + strFileName + subnames[i] + ".png", ImageFormat.Png);
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

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CursorColors.BufferAction act = ccPaint.undoBuffer[ccPaint.undoBuffer.Count - 1];
            var (x, y, features) = (act.X, act.Y, act.Components);
            CursorColors.Features features2 = CursorColors.FeaturesRendered(imgRendered.GetPixel(x, y));
            var oldState = new CursorColors.BufferAction(x,y, features2);
            ccPaint.AddToRedoBuffer(oldState);
            WorkspaceSetPixel(x,y,features);
            ccPaint.RemoveFromUndoBuffer();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CursorColors.BufferAction act = ccPaint.redoBuffer[ccPaint.redoBuffer.Count - 1];
            var (x, y, features) = (act.X, act.Y, act.Components);
            CursorColors.Features features2 = CursorColors.FeaturesRendered(imgRendered.GetPixel(x, y));
            var oldState = new CursorColors.BufferAction(x, y, features2);
            //ccPaint.AddToUndoBuffer(oldState);
            //WorkspaceSetPixel(x, y, features2);
            //ccPaint.RemoveFromRedoBuffer();
        }
    }
}
