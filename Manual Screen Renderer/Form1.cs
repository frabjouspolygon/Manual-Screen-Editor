using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using Manual_Screen_Renderer;

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
            //pbxWorkspace.SizeMode = PictureBoxSizeMode.AutoSize;
            //splitContainer1.Panel2.AutoScroll = true;
            pnlWorkspace.AutoScroll = true;
            pbxWorkspace.MouseWheel += pbxWorkspace_MouseWheel;
            //pbxWorkspace.
            pbxWorkspace.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            int R = 180;

            int Light = R > 90 ? 1 : 0;// 0 or 1
            R = R - 90 * Light;
            int LColor = Math.Min(Math.Max((R - 1) / 30, 0), 2);//0-2
            R = R - LColor * 30-1;
            int Depth = Math.Min(Math.Max(R, 0), 30);//0-29
            //Depth = Math.Min((int)(Depth * 8.79), 255);
            Console.WriteLine(Light);
            Console.WriteLine(LColor);
            Console.WriteLine(R);
        }

        static double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s-a1)*(b2-b1)/(a2-a1);

        private void pbxWorkspace_Click(object sender, EventArgs e)
        {
            /*Point clientPoint = PointToClient(Cursor.Position);
            var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Size.Width, clientPoint.X) + 0.5d);
            var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Size.Height, clientPoint.Y) + 0.5d);
            //lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
            Bitmap imgWorking = (Bitmap)pbxWorkspace.Image;
            Graphics graphic = Graphics.FromImage(imgWorking);
            intY = Math.Max(Math.Min(intY, imgWorking.Size.Height-1),0);
            intX = Math.Max(Math.Min(intX, imgWorking.Size.Width-1), 0);
            //imgWorking.SetPixel(intX,intY,colCursor);
            //pbxWorkspace.Image = imgWorking;
            switch(intMode)
            {
                case 0://Depth mode
                    imgDepth = imgWorking;
                    break;
                case 1://EColor mode
                    imgEColor = imgWorking;
                    break;
                case 2://Index mode
                    imgIndex = imgWorking;
                    break;
                case 3://LColor mode
                    imgLColor = imgWorking;
                    break;
                case 4://Light mode
                    imgLight = imgWorking;
                    break;
                case 5://Pipe mode
                    imgPipe = imgWorking;
                    break;
                case 6://Rainbow mode
                    imgRainbow = imgWorking;
                    break;
                case 7://Shading mode
                    imgShading = imgWorking;
                    break;
                case 8://Sky mode
                    imgSky = imgWorking;
                    break;
            }*/
            


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

        private void btnEditDepth_Click(object sender, EventArgs e)
        {
            intMode = 0;
            pbxWorkspace.Image = imgDepth;

        }

        private void btnEditEColor_Click(object sender, EventArgs e)
        {
            intMode = 1;
            pbxWorkspace.Image = imgEColor;
        }

        private void btnEditIndex_Click(object sender, EventArgs e)
        {
            intMode = 2;
            pbxWorkspace.Image = imgIndex;
        }

        private void btnEditLColor_Click(object sender, EventArgs e)
        {
            intMode = 3;
            pbxWorkspace.Image = imgLColor;
        }

        private void btnEditLight_Click(object sender, EventArgs e)
        {
            intMode = 4;
            pbxWorkspace.Image = imgLight;
        }

        private void btnEditPipe_Click(object sender, EventArgs e)
        {
            intMode = 5;
            pbxWorkspace.Image = imgPipe;
        }

        private void btnEditRainbow_Click(object sender, EventArgs e)
        {
            intMode = 6;
            pbxWorkspace.Image = imgRainbow;
        }

        private void btnEditShading_Click(object sender, EventArgs e)
        {
            intMode = 7;
            pbxWorkspace.Image = imgShading;
        }

        private void btnEditSky_Click(object sender, EventArgs e)
        {
            intMode = 8;
            pbxWorkspace.Image = imgSky;
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color colSelection = colorDialog1.Color;
            colCursor = colSelection;
            btnColorPicker.BackColor = colorDialog1.Color;
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

        private void pbxWorkspace_MouseMove(object sender, MouseEventArgs e)
        {
            if (pbxWorkspace.Image != null)
            {
                Point clientPoint = PointToClient(Cursor.Position);
                var intX = (int)(Map(splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left, splitContainer1.Left + splitContainer1.Panel2.Left + pnlWorkspace.Left + pbxWorkspace.Left + pbxWorkspace.Width, 0, pbxWorkspace.Image.Width, clientPoint.X) + 0.5d);
                var intY = (int)(Map(splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top, splitContainer1.Top + splitContainer1.Panel2.Top + pnlWorkspace.Top + pbxWorkspace.Top + pbxWorkspace.Height, 0, pbxWorkspace.Image.Height, clientPoint.Y) + 0.5d);
                lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                //lblCursorCoords.Text = pbxWorkspace.Image.HorizontalResolution.ToString();

                if (MouseButtons == MouseButtons.Left)
                {
                    Bitmap imgWorking = (Bitmap)pbxWorkspace.Image;
                    //Graphics graphic = Graphics.FromImage(imgWorking);
                    intY = Math.Max(Math.Min(intY, imgWorking.Size.Height - 1), 0);
                    intX = Math.Max(Math.Min(intX, imgWorking.Size.Width - 1), 0);
                    imgWorking.SetPixel(intX, intY, colCursor);
                    pbxWorkspace.Image = imgWorking;
                }
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
            imgIndex = new Bitmap(imgRendered.Width, imgRendered.Height, PixelFormat.Format32bppPArgb);
            int MinIndex = 0;
            for (int i = 0; i<imgRendered.Height; i++)
            {
                for (int j = 0; j< imgRendered.Width; j++)
                {
                    Color colPixel = imgRendered.GetPixel(j,i);
                    int R = colPixel.R;
                    int G = colPixel.G;
                    int B = colPixel.B;

                    if ( (R==0 || (180<R && R<255) ) || (G>31) )
                    {
                    //Error or index color
                    }
                    if (R==255 && G==255 && B==255)//if sky
                    {
                        imgSky.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        imgLight.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                        imgLColor.SetPixel(j, i, Color.FromArgb(0,0,0));
                        imgDepth.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        imgRainbow.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                        imgEColor.SetPixel(j, i, Color.FromArgb(0,0,0));
                        imgShading.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                        imgIndex.SetPixel(j, i, Color.Transparent);

                    }
                    else//not sky
                    {
                        if(R>180)
                        {
                            R = 150;
                        }
                        if(G>31)
                        {
                            G = 0;
                        }
                        imgSky.SetPixel(j,i, Color.FromArgb(0, 0, 0) );
                        int Light = R>90?1:0;// 0 or 1
                        R=R-90*Light;
                        Light = Light*255;
                        imgLight.SetPixel(j, i, Color.FromArgb(Light, Light, Light)); 
                        int LColor = Math.Min(Math.Max((R - 1) / 30, 0),2);//0-2
                        imgLColor.SetPixel(j, i, Color.FromArgb((LColor==0?1:0)*255, (LColor==1?1:0)*255, (LColor==2?1:0)*255) );
                        R=R-LColor*30 -1;
                        int Depth = Math.Min(Math.Max(R, 0),30);//0-29
                        Depth = Math.Min( (int)(Depth*8.79) ,255);
                        imgDepth.SetPixel(j, i, Color.FromArgb(Depth, Depth, Depth));
                        int Pipe = (G==8?1:0+G==9?2:0+G==10?3:0) * B==0?1:0;//0-3
                        if ((G==8 || G==9 || G==10) && B==0)
                        {
                            imgPipe.SetPixel(j, i, Color.FromArgb(G == 8 ? 255 : 0, G == 10 ? 255 : 0, G == 9 ? 255 : 0));
                        }
                        else
                        {
                            imgPipe.SetPixel(j, i, Color.FromArgb(0,0,0));
                        }
                        //imgPipe.SetPixel(j, i, Color.FromArgb(Pipe == 1 ? 255 : 0, Pipe == 2 ? 255 : 0, Pipe == 3 ? 255 : 0));
                        G =G%16; //0-15
                        int HasIndex = Math.Min(G/8,1); //0 or 1
                        HasIndex = HasIndex * (B==0?1:0);
                        G=G%8; //0-7
                        int Rainbow = G/4;//0 or 1
                        Rainbow = Math.Min(Math.Max(Rainbow, 0), 1) * 255;
                        imgRainbow.SetPixel(j, i, Color.FromArgb(Rainbow, Rainbow, Rainbow));
                        int EColor = G%4;//0-3
                        EColor = Math.Min(Math.Max(EColor, 0), 3);
                        imgEColor.SetPixel(j, i, Color.FromArgb(EColor==1||EColor==3 ? 255:0, EColor == 2 || EColor == 3 ? 255 : 0, EColor != 0 ? 255 : 0));
                        int Shading=(1-HasIndex)*(EColor>0 ? 1:0)*B; //0-255
                        imgShading.SetPixel(j, i, Color.FromArgb(Shading, Shading, Shading));

                        if (HasIndex ==1)
                        {
                            if (B<MinIndex)
                            {
                                MinIndex = B;
                            }
                            imgIndex.SetPixel(j, i, imgRendered.GetPixel(255-B,0));
                        }
                        else
                        {
                            imgIndex.SetPixel(j, i, Color.Transparent);
                        }
                    }//end not sky
                }//end for width
            }//end for height


            lblMessages.Text = "Ready";
        }
    }
}
