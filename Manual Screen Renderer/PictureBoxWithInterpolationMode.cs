using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static Manual_Screen_Renderer.MseMath;
using System.Drawing.Imaging;

namespace Manual_Screen_Renderer
{
    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }
        public Image fullImage { get; set; }
        public Point scrollTL { get; set; }//file-pixel displacements between top left corners of fullImage and the bounds before scale
        public float scale { get; set; }//percent scale final/original
        public float scrollx { get; set; }//scrollbar x percent, 0=showing left side of image
        public float scrolly { get; set; }//scrollbar y percent, 0=showing top of image
        public PictureBoxWithInterpolationMode()
        {
            this.DoubleBuffered = true;
            this.scrollTL = new Point(0, 0);
            scale = 1.0f;
        }

        public Point WorkspacePosition(Point clientPoint)
        {
            var workRect = this.DisplayRectangle;//ClientRectangle;
            workRect = this.RectangleToScreen(workRect);
            var intX = (int)(Map(workRect.Left, workRect.Right, 0, this.Image.Width, clientPoint.X) + 0.5d);
            var intY = (int)(Map(workRect.Top, workRect.Bottom, 0, this.Image.Height, clientPoint.Y) + 0.5d);
            intX = (int)Clamp(0, this.Image.Width - 1, intX);
            intY = (int)Clamp(0, this.Image.Height - 1, intY);
            Point outputPoint = new Point(intX, intY);
            return outputPoint;
        }

        public void SetScale(float newScale, Point d_focalPoint)
        {
            Point f_focalPoint = MapClientToFull(d_focalPoint);
            //get og center coord of fullimg
            //set new scroll to keep it
            this.scale = newScale;
            
            scrollTL = MapClientToFull(new Point(0, 0));
        }

        private Point GetScrollBR()
        {
            return new Point(this.scrollTL.X+this.Image.Width-this.fullImage.Width, this.scrollTL.Y + this.Image.Height - this.fullImage.Height);
        }

        public void PrintImage()
        {
            if (this.scale <= 0)
            {
                this.scale = 1;
            }
            if (this.fullImage == null)
            {
                this.fullImage = new Bitmap(1400,800);
            }
            if (this.Image == null)
            {
                this.Image = new Bitmap(1400, 800);
            }
            //TransformImage();


            int dw = this.Image.Width;//display width in terms of screen pixels
            int dh = this.Image.Height;//display height
            int fw = this.fullImage.Width;//png width
            int fh = this.fullImage.Height;//png height
            int fw_d = (int)(fw / this.scale);//interpolated png width in terms of screen pixels
            int fh_d = (int)(fh / this.scale);
            int dw_f = (int)(dw * this.scale);//display width in terms of png pixels
            int dh_f = (int)(dh * this.scale);
            int offsetLeft = (int)(this.scrollx * (fw - dw_f));//png pixels from left edge of png to left edge of display
            int offsetRight = (int)((1 - this.scrollx) * (fw - dw_f));//png pixels from right edge of png to right edge of display
            int offsetTop = (int)(this.scrolly * (fh - dh_f));//png pixels from top edge of png to top edge of display
            int offsetBottom = (int)((1 - this.scrolly) * (fh - dh_f));//png pixels from bottom edge of png to bottom edge of display
            int readMinX = offsetLeft;//png pixels
            int readMaxX = fw - offsetRight;
            int readMinY = offsetTop;
            int readMaxY = fh - offsetBottom;




            Bitmap resultBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(resultBitmap))
            {
                // Optional: Improve quality settings (HighQuality is often a good default)
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.None;

                // 3. Fill the new canvas with a background color
                g.FillRectangle(new SolidBrush(Color.Purple), new Rectangle(0, 0, this.Width, this.Height));

                // 4. Calculate scaling factor to maintain aspect ratio (fit within the target dimensions)
                //float scale = Math.Min((float)this.Width / this.fullImage.Width, (float)this.Height / this.fullImage.Height);
                int scaledWidth = (int)(this.fullImage.Width / this.scale);
                int scaledHeight = (int)(this.fullImage.Height / this.scale);

                // 5. Calculate offset (x, y coordinates) to center the image on the canvas
                int offsetX = offsetLeft;// - this.scrollTL.X;// (this.Width - scaledWidth) / 2;
                int offsetY = offsetTop;// - this.scrollTL.Y; //(this.Height - scaledHeight) / 2;

                // 6. Draw the source image onto the new canvas using the calculated scale and offset
                // The destination rectangle defines the final position and size of the drawn image
                g.DrawImage(this.fullImage, new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight));
            }
            this.Image = resultBitmap;
        }

        private void TransformImage()
        {
            int dw = this.Image.Width;//display width in terms of screen pixels
            int dh = this.Image.Height;//display height
            int fw = this.fullImage.Width;//png width
            int fh = this.fullImage.Height;//png height
            int fw_d = (int)(fw / this.scale);//interpolated png width in terms of screen pixels
            int fh_d = (int)(fh / this.scale);
            int dw_f = (int)(dw * this.scale);//display width in terms of png pixels
            int dh_f = (int)(dh * this.scale);
            int offsetLeft = (int)(this.scrollx * (fw - dw_f));//png pixels from left edge of png to left edge of display
            int offsetRight = (int)((1 - this.scrollx) * (fw - dw_f));//png pixels from right edge of png to right edge of display
            int offsetTop = (int)(this.scrolly * (fh - dh_f));//png pixels from top edge of png to top edge of display
            int offsetBottom = (int)((1 - this.scrolly) * (fh - dh_f));//png pixels from bottom edge of png to bottom edge of display
            int readMinX = offsetLeft;//png pixels
            int readMaxX = fw - offsetRight;
            int readMinY = offsetTop;
            int readMaxY = fh - offsetBottom;
            Bitmap imgInput = (Bitmap)this.fullImage.Clone();
            Bitmap imgOutput = (Bitmap)this.Image.Clone();
            Size s0 = imgInput.Size;
            Size s1 = imgOutput.Size;
            PixelFormat fmt = PixelFormat.Format32bppArgb;// bitmap.PixelFormat;
            byte bpp = (byte)4;//(fmt == PixelFormat.Format32bppArgb ? 4 : 3);
            // lock the bits and prepare the loop
            Rectangle rect0 = new Rectangle(Point.Empty, s0);
            Rectangle rect1 = new Rectangle(Point.Empty, s1);
            BitmapData bmpData0 = imgInput.LockBits(rect0, ImageLockMode.ReadOnly, fmt);
            
            BitmapData bmpData1 = imgOutput.LockBits(rect1, ImageLockMode.ReadOnly, fmt);
            int size0 = bmpData0.Stride * bmpData0.Height;
            int size1 = bmpData1.Stride * bmpData1.Height;
            byte[] data0 = new byte[size0];
            byte[] data1 = new byte[size1];
            System.Runtime.InteropServices.Marshal.Copy(bmpData0.Scan0, data0, 0, size1);
            // loops
            for (int y = 0; y < s1.Height; y++)
            {
                for (int x = 0; x < s1.Width; x++)
                {
                    int y0 = (int)Math.Round(Map(0, s1.Height, readMinY, readMaxY, y));
                    int x0 = (int)Math.Round(Map(0, s1.Width, readMinX, readMaxX, x));
                    int index0 = y0 * bmpData0.Stride + x0 * bpp;
                    int index1 = y * bmpData1.Stride + x * bpp;
                    /*Console.WriteLine("fh="+fh + ",dh_f=" + dh_f + ",scrollY=" + this.scrolly);
                    Console.WriteLine("offT="+offsetTop + ", offB=" + offsetBottom);
                    Console.WriteLine("rMinY="+readMinY + ",rMaxY=" + readMaxY );
                    Console.WriteLine("y0="+y0.ToString() +", x0="+ x0.ToString() + ", index1=" + index1.ToString()+", stride="+ bmpData0.Stride);
                    Console.WriteLine("y0~" + 0 + ", x0~" + 0 + ", index1~" + 0 + ", stride~" + bmpData0.Stride);
                    Console.WriteLine(index0);*/
                    data1[index1 + 0] = data0[index0 + 0];//B
                    data1[index1 + 1] = data0[index0 + 1];//G
                    data1[index1 + 2] = data0[index0 + 2];//R
                    data1[index1 + 3] = data0[index0 + 3];//A
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(data1, 0, bmpData1.Scan0, data1.Length);
            imgInput.UnlockBits(bmpData0); 
            imgOutput.UnlockBits(bmpData1);
            this.Image = imgOutput;
        }



        public Point MapClientToFull(Point clientPoint)
        {
            /// <summary>
            /// Gets the location of the pixel of interest from the source image from Cursor.Position
            /// </summary>
            /// <param name="clientPoint">The global cursor position from Cursor.Position</param>
            /// <returns>The coordinate point to manipulate the picture at.</returns>
            //Point scrollBR = GetScrollBR();
            int dw = this.Image.Width;//display width in terms of screen pixels
            int dh = this.Image.Height;//display height
            int fw = this.fullImage.Width;//png width
            int fh = this.fullImage.Height;//png height
            int fw_d = (int)(fw / this.scale);//interpolated png width in terms of screen pixels
            int fh_d = (int)(fh / this.scale);
            int dw_f = (int)(dw * this.scale);//display width in terms of png pixels
            int dh_f = (int)(dh * this.scale);
            int offsetLeft = (int)(this.scrollx * (fw - dw_f));//png pixels from left edge of png to left edge of display
            int offsetRight = (int)((1-this.scrollx) * (fw - dw_f));//png pixels from right edge of png to right edge of display
            int offsetTop = (int)(this.scrolly * (fh-dh_f));//png pixels from top edge of png to top edge of display
            int offsetBottom = (int)((1-this.scrolly) * (fh - dh_f));//png pixels from bottom edge of png to bottom edge of display
            var workRect = this.DisplayRectangle;//ClientRectangle;
            workRect = this.RectangleToScreen(workRect);
            clientPoint = new Point((int)Clamp(0, this.Image.Width - 1, clientPoint.X), (int)Clamp(0, this.Image.Height - 1, clientPoint.Y));
            var intX = (int)(Map(workRect.Left, workRect.Right, offsetLeft, offsetRight, clientPoint.X) + 0.5d);
            //var intX = (int)((clientPoint.X / dw) * (fw - this.scrollTL.X + scrollBR.X) + this.scrollTL.X);
            //var intY = (int)((clientPoint.Y / dh) * (fh - this.scrollTL.Y + scrollBR.Y) + this.scrollTL.Y);
            //(int)(Map(workRect.Left, workRect.Right, scrollTL.X, scrollBR.X, clientPoint.X) + 0.5d);
            var intY = (int)(Map(workRect.Top, workRect.Bottom, offsetTop, offsetBottom, clientPoint.Y) + 0.5d);
            intX = (int)Clamp(0, this.fullImage.Width - 1, intX);
            intY = (int)Clamp(0, this.fullImage.Height - 1, intY);
            Point outputPoint = new Point(intX, intY);
            return outputPoint;
        }

        public Point MapDisplayToFull(Point displayPoint)
        {
            var BRX = (int)((this.fullImage.Width - scrollTL.X) * scale);
            var BRY = (int)((this.fullImage.Height - scrollTL.Y) * scale);
            Point scrollBR = new Point(BRX, BRY);
            var workRect = this.DisplayRectangle;//ClientRectangle;
            workRect = this.RectangleToScreen(workRect);
            var intX = (int)(Map(workRect.Left, workRect.Right, scrollTL.X, scrollBR.X, displayPoint.X) + 0.5d);
            var intY = (int)(Map(workRect.Top, workRect.Bottom, scrollTL.Y, scrollBR.Y, displayPoint.Y) + 0.5d);
            intX = (int)Clamp(0, this.fullImage.Width - 1, intX);
            intY = (int)Clamp(0, this.fullImage.Height - 1, intY);
            Point outputPoint = new Point(intX, intY);
            return outputPoint;
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
        /*
        protected override void OnResize(EventArgs e)
        {
            if (this.Image != null)
            {
                float screenDpi = this.DeviceDpi;
                Bitmap dpiAwareBitmap = new Bitmap(this.Image.Width, this.Image.Height);
                dpiAwareBitmap.SetResolution(screenDpi, screenDpi);
                if (this.Image != null)
                {
                    this.Image.Dispose();
                }
                this.Image = dpiAwareBitmap;
            }
            base.OnResize(e);
        }
        
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int scrollDir = Math.Sign(e.Delta);
            float speed = 0.1f;
            float zoomSpeed = 0.1f;
            if ( (Control.ModifierKeys & Keys.Shift) == Keys.Shift)//horiz
            {
                this.scrollx = this.scrollx + speed*scrollDir;
            }
            else if ( (Control.ModifierKeys & Keys.Control) == Keys.Control)//zoom
            {
                SetScale(this.scale * (1+scrollDir*zoomSpeed), Cursor.Position);
            }
            else//vert
            {
                this.scrolly = this.scrolly + speed * scrollDir;
            }
            base.OnMouseWheel(e);
        }
        */
    }
}