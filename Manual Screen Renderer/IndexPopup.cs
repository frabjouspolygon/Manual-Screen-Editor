using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace Manual_Screen_Renderer
{
    public partial class IndexPopup : Form
    {
        public static int h = 15;//boxes should be 30x30
        public static int w = 17;
        public List<int> selections = new List<int>();
        private ColorPalette IndexPalette { get; set; }
        public IndexPopup(ColorPalette indexpalette)
        {
            InitializeComponent();
            IndexPalette = indexpalette;
            //pictureBox1.MouseClick += pictureBox1_MouseClick;
            Bitmap bitmap = Form1.SolidBitmap(510, 450, Color.FromArgb(255, 255, 255));
            pictureBox1.Image = bitmap;
            InitializeSwatches();
        }
        
        private void pictureBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Point clientPoint = PointToClient(System.Windows.Forms.Cursor.Position);
                var intX = (int)(Form1.Map(pictureBox1.Left, pictureBox1.Left+pictureBox1.Width, 0, pictureBox1.Image.Width, clientPoint.X) + 0.5d);
                var intY =  (int)(Form1.Map(pictureBox1.Top, pictureBox1.Top + pictureBox1.Height, 0, pictureBox1.Image.Height, clientPoint.Y) + 0.5d);
                Console.WriteLine("X "+clientPoint.X.ToString()+" " + pictureBox1.Left.ToString() + " " + pictureBox1.Width.ToString() + " " + pictureBox1.Image.Width.ToString() + " " + intX.ToString());
                //lblCursorCoords.Text = "(" + intX.ToString() + "," + intY.ToString() + ")";
                //lblCursorCoords.Text = pbxWorkspace.Image.HorizontalResolution.ToString();
                int celX = intX / (pictureBox1.Image.Width / w);
                int celY = intY / (pictureBox1.Image.Height / h);

                intY = Math.Max(Math.Min(intY, pictureBox1.Image.Height - 1), 0);
                intX = Math.Max(Math.Min(intX, pictureBox1.Image.Width - 1), 0);
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    ToggleSelection(celY * w + celX);
                    //pictureBox1.Image = DrawOutline((Bitmap)pictureBox1.Image, celX, celY);
                    //Bitmap imgWorking = (Bitmap)pictureBox1.Image;
                    //selections.Add(celY * w + celX);
                }
            }
        }

        private void InitializeSwatches()
        {
            for (int colorID = 0; colorID < 256-1; colorID++)
            {
                int celX = colorID % w;
                int celY = (int)(colorID / w);
                pictureBox1.Image = DrawSwatch((Bitmap)pictureBox1.Image, celX, celY, IndexPalette.Entries[colorID]);
            }
        }

        private void ToggleSelection(int colorID)
        {
            int match = selections.FindIndex(a => a == colorID);
            int celX = colorID % w;
            int celY = (int)(colorID / w);
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

        private Bitmap DrawOutline(Bitmap bitmap,int celX, int celY, Color color)
        {
            int x = celX * (pictureBox1.Image.Width / w);
            int y = celY * (pictureBox1.Image.Height / h);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Pen selPen = new Pen(color);
                selPen.Width = 2.0F;
                g.DrawRectangle(selPen, x+2, y+2, (pictureBox1.Image.Width / w)-4, (pictureBox1.Image.Height / h)-4);
            }
            return bitmap;
        }

        private Bitmap DrawSwatch(Bitmap bitmap, int celX, int celY, Color color)
        {
            int x = celX * (pictureBox1.Image.Width / w);
            int y = celY * (pictureBox1.Image.Height / h);
            if (color.A <255)
            {
                color = Color.Transparent;
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                SolidBrush selBrush = new SolidBrush(color);
                g.FillRectangle(selBrush, x, y, (pictureBox1.Image.Width / w), (pictureBox1.Image.Height / h));
            }
            return bitmap;
        }

    }
}
