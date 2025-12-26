using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;
using static Manual_Screen_Renderer.MseMath;

namespace Manual_Screen_Renderer
{
    public partial class IndexPopup : Form
    {
        public static int h = 15;//boxes should be 30x30
        public static int w = 17;
        public List<int> selections = new List<int>();
        public ColorPalette IndexPalette { get; set; }
        public int selectedColorID { get; set; }

        public IndexPopup(ColorPalette indexpalette, int selectedcolorid)
        {
            InitializeComponent();
            IndexPalette = indexpalette;//ReversePalette(indexpalette, indexpalette);
            selectedColorID = selectedcolorid;
            //pictureBox1.MouseClick += pictureBox1_MouseClick;
            Bitmap bitmap = Form1.SolidBitmap(510, 450, Color.FromArgb(255, 255, 255));
            pictureBox1.Image = bitmap;
            RefreshPaletteImg();
        }
        
        private void pictureBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Point clientPoint = PointToClient(System.Windows.Forms.Cursor.Position);
                var intX = (int)(Map(pictureBox1.Left, pictureBox1.Left+pictureBox1.Width, 0, pictureBox1.Image.Width, clientPoint.X) + 0.5d);
                var intY =  (int)(Map(pictureBox1.Top, pictureBox1.Bottom, 0, pictureBox1.Image.Height, clientPoint.Y) + 0.5d)-5;
                //Console.WriteLine("X "+clientPoint.X.ToString()+" " + pictureBox1.Left.ToString() + " " + pictureBox1.Width.ToString() + " " 
                //    + pictureBox1.Image.Width.ToString() + " " + intX.ToString());
                //lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                //lblCursorCoords.Text = pbxWorkspace.Image.HorizontalResolution.ToString();
                intY = Math.Max(Math.Min(intY, pictureBox1.Image.Height - 1), 0);
                intX = Math.Max(Math.Min(intX, pictureBox1.Image.Width - 1), 0);

                int celX = intX / (pictureBox1.Image.Width / w);
                int celY = intY / (pictureBox1.Image.Height / h);
                int colorID = GetIDFromCell(celX, celY);

                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    ToggleSelection(colorID);
                    RefreshPaletteImg();
                }
                else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if(selectedColorID == colorID)
                    {
                        selectedColorID = 0;
                    }
                    else
                    {
                        selectedColorID = colorID;
                    }
                    RefreshPaletteImg();
                }
                else
                {
                    Color color = IndexPalette.Entries[colorID];
                    if (color == Color.Transparent) color = Color.White;
                    colorDialog1.Color = color;
                    var result = colorDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Color colSelection = colorDialog1.Color;
                        IndexPalette.Entries[colorID] = colSelection;
                        Bitmap bitmap = (Bitmap)pictureBox1.Image;
                        bitmap = DrawSwatch(bitmap, celX, celY, colSelection);
                        for (int i = 0; i < selections.Count; i++)
                        {
                            int tcolorID = selections[i];
                            var (tcelX, tcelY) = GetCellFromID(tcolorID);
                            Color tcolor = IndexPalette.Entries[tcolorID];
                            if (IndexPalette.Entries[tcolorID] == Color.Transparent)
                            {
                                tcolor = Color.White;
                            }
                            bitmap = DrawSwatch(bitmap, tcelX, tcelY, tcolor);
                        }
                        selections.Clear();
                        pictureBox1.Image = bitmap;
                        RefreshPaletteImg();
                    }
                }
            }
        }

        private int GetIDFromCell(int celX, int celY)
        {
            int colorID = celY * w + celX;
            colorID = 255 - colorID;
            return colorID;
        }
        private (int,int) GetCellFromID(int colorID)
        {
            colorID = 255 - colorID;
            int celX = colorID % w;
            int celY = (int)(colorID / w);
            return (celX, celY);
        }

        private void InitializeSwatches()
        {
            for (int colorID = 1; colorID < 256; colorID++)
            {
                var (celX, celY) = GetCellFromID(colorID);
                Color color = IndexPalette.Entries[colorID];
                if(color==Color.Transparent)
                {
                    color = Color.White;
                }
                pictureBox1.Image = DrawSwatch((Bitmap)pictureBox1.Image, celX, celY, color);
            }
        }

        private void ToggleSelection(int colorID)
        {
            int match = selections.FindIndex(a => a == colorID);
            var (celX, celY) = GetCellFromID(colorID);
            if (match < 0)//no match
            {
                selections.Add(colorID);
                pictureBox1.Image = DrawOutline((Bitmap)pictureBox1.Image, celX, celY, Color.Black);
            }
            else //match
            {
                Color color = IndexPalette.Entries[colorID];
                if (color.A < 255)
                {
                    color = Color.White;
                }
                pictureBox1.Image = DrawSwatch((Bitmap)pictureBox1.Image, celX, celY, color);
                selections.RemoveAt(match);
            }
        }

        public void RefreshPaletteImg()
        {
            InitializeSwatches();
            for (int i = 0; i < selections.Count; i++)
            {
                int colorID = selections[i];
                var (celX, celY) = GetCellFromID(colorID);
                pictureBox1.Image = DrawOutline((Bitmap)pictureBox1.Image, celX, celY, Color.Black);
            }
            if (selectedColorID >= 0)
            {
                var (celX, celY) = GetCellFromID(selectedColorID);
                pictureBox1.Image = DrawOutline((Bitmap)pictureBox1.Image, celX, celY, Color.Red);
            }
        }

        private Bitmap DrawOutline(Bitmap bitmap,int celX, int celY, Color color)
        {
            int x = celX * (pictureBox1.Image.Width / w);
            int y = celY * (pictureBox1.Image.Height / h);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                System.Drawing.Pen selPen = new System.Drawing.Pen((Color)color);
                selPen.Width = 2.0F;
                g.DrawRectangle(selPen, x+2, y+2, (pictureBox1.Image.Width / w)-4, (pictureBox1.Image.Height / h)-4);
            }
            return bitmap;
        }

        private Bitmap DrawSwatch(Bitmap bitmap, int celX, int celY, Color color)
        {
            if(celX==0 && celY==0)
            {
                Console.WriteLine("swatch at cell 0,0");
            }
            if (celX == 0 && celY == 1)
            {
                Console.WriteLine("swatch at cell 0,1");
            }
            int x = celX * (pictureBox1.Image.Width / w);
            int y = celY * (pictureBox1.Image.Height / h);
            if (color.A <255)
            {
                color = Color.White;
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                SolidBrush selBrush = new SolidBrush(color);
                g.FillRectangle(selBrush, x, y, (pictureBox1.Image.Width / w), (pictureBox1.Image.Height / h));
            }
            return bitmap;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private ColorPalette ReversePalette(ColorPalette inputPalette, ColorPalette outputPalette)
        {
            //ColorPalette outputPalette = inputPalette;
            for (int i = 0; i<inputPalette.Entries.Length; i++)
            {
                outputPalette.Entries[i] = inputPalette.Entries[255-i];
            }
            return outputPalette;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnDelCol_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            for (int i = 0; i < selections.Count; i++)
            {
                int colorID = selections[i];
                var (celX, celY) = GetCellFromID(colorID);
                IndexPalette.Entries[colorID] = Color.Transparent;
                bitmap = DrawSwatch(bitmap, celX, celY, Color.White);
            }
            selections.Clear();
            pictureBox1.Image = bitmap;
            RefreshPaletteImg();
        }
    }
}
