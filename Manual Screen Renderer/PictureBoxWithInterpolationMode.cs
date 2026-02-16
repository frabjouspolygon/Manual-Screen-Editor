using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static Manual_Screen_Renderer.MseMath;
using System.Drawing.Imaging;
using System.Threading.Tasks;

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
        public int cursorRadius { get; set; }
        public bool showCursor { get; set; }
        public PointF[] selPoints { get; set; }
        public Point panPoint { get; set; }

        public PictureBoxWithInterpolationMode()
        {
            this.DoubleBuffered = true;
            this.scrollTL = new Point(0, 0);
            this.scale = 1.0f;
        }
        
        public Point WorkspacePosition(Point clientPoint)
        {
            if (this.Image != null)
            {
                var workRect = this.DisplayRectangle;
                workRect = this.RectangleToScreen(workRect);
                var intX = (int)(Map(workRect.Left, workRect.Right, 0, this.Image.Width, clientPoint.X) + 0.5d);
                var intY = (int)(Map(workRect.Top, workRect.Bottom, 0, this.Image.Height, clientPoint.Y) + 0.5d);
                intX = (int)Clamp(0, this.Image.Width - 1, intX);
                intY = (int)Clamp(0, this.Image.Height - 1, intY);
                Point outputPoint = new Point(intX, intY);
                return outputPoint;
            }
            return clientPoint;
        }
        public void SetScale(float newScale, Point d_focalPoint)
        {
            //Console.WriteLine("set scale "+this.scale + " > " + newScale);
            PointF[] newCenterPoints = { this.WorkspacePosition(d_focalPoint) };
            PointF[] centerPoints = { this.WorkspacePosition(d_focalPoint) };
            Matrix prev = new Matrix();
            prev.Scale(this.scale, this.scale);
            prev.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
            if (prev.IsInvertible)
            {
                prev.Invert();
                prev.TransformPoints(centerPoints);
                PointF target = centerPoints[0];
                Matrix next = new Matrix();
                next.Scale(newScale, newScale);
                next.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
                if (next.IsInvertible)
                {
                    next.Invert();
                    next.TransformPoints(newCenterPoints);
                    float dx = newCenterPoints[0].X - centerPoints[0].X;
                    float dy = newCenterPoints[0].Y - centerPoints[0].Y;
                    this.scrollx += dx * newScale;
                    this.scrolly += dy * newScale;
                }
            }
            this.scale = newScale;
        }
        /*public void Test1()
        {
            using (Matrix myMatrix = new Matrix())
            {
                PointF[] centerPoints = { this.WorkspacePosition(Cursor.Position) };
                PointF centerPoint = centerPoints[0];
                myMatrix.Scale(this.scale, this.scale, MatrixOrder.Append);
                myMatrix.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
                PointF[] points = { new PointF(0, 0), new PointF(this.fullImage.Width, 0), new PointF(this.fullImage.Width, this.fullImage.Height), new PointF(0, this.fullImage.Height) };
                PointF[] points3 = { new PointF(10, 20), new PointF(40, 20), new PointF(40, 80), new PointF(10, 80) };
                int brushSize = (int)(10 * this.scale);
                myMatrix.TransformPoints(points);
                myMatrix.TransformPoints(points3);
                using (Bitmap resultBitmap = new Bitmap(this.Width, this.Height))
                {
                    using (Graphics g = Graphics.FromImage(resultBitmap))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.FillRectangle(new SolidBrush(Color.Purple), new Rectangle(0, 0, this.Width, this.Height));
                        g.DrawImage(this.fullImage, new Rectangle((int)points[0].X, (int)points[0].Y, (int)points[2].X - (int)points[0].X, (int)points[2].Y - (int)points[0].Y));
                        using (Pen dashedPen = new Pen(Color.FromArgb(100, Color.Black), 2))//selection
                        {
                            dashedPen.DashStyle = DashStyle.Dash;
                            g.DrawPolygon(dashedPen, points3);
                        }
                        Point cursorPos = this.WorkspacePosition(Cursor.Position);
                        g.DrawEllipse(new Pen(Color.FromArgb(100, Color.Gray), 2f), new Rectangle(cursorPos.X - brushSize, cursorPos.Y - brushSize, brushSize * 2, brushSize * 2));
                        Image oldImage = this.Image;
                        this.Image = (Image)resultBitmap.Clone();
                        if (oldImage != null)
                        {
                            oldImage.Dispose();
                        }
                    }
                }
            }
        }*/
        public void PrintImage()
        {
            if (this.fullImage == null)
                return;
            using (Matrix myMatrix = new Matrix())
            {
                PointF[] centerPoints = { this.WorkspacePosition(Cursor.Position) };
                PointF centerPoint = centerPoints[0];
                myMatrix.Scale(this.scale, this.scale, MatrixOrder.Append);
                myMatrix.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);

                //Console.WriteLine("image? " + (this.Image != null).ToString() + " " + this.Image.GetType() );
                Console.WriteLine("fullimage? " + (this.fullImage != null).ToString() + " " + this.fullImage.GetType());
                Console.WriteLine(" w=" + ((Image)this.fullImage).Width );
                PointF[] points = { new PointF(0, 0), new PointF(this.fullImage.Width, 0), new PointF(this.fullImage.Width, this.fullImage.Height), new PointF(0, this.fullImage.Height) };
                int brushSize = (int)(this.cursorRadius * this.scale);
                myMatrix.TransformPoints(points);
                using (Bitmap resultBitmap = new Bitmap(this.Width, this.Height))
                using (Pen dashedPen = new Pen(Color.FromArgb(100, Color.Black), 2))//selection
                using (Pen CursorPen = new Pen(Color.FromArgb(100, Color.Gray), 2f))//selection
                using (Graphics g = Graphics.FromImage(resultBitmap))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
                    g.DrawImage(this.fullImage, new Rectangle((int)points[0].X, (int)points[0].Y, (int)points[2].X - (int)points[0].X, (int)points[2].Y - (int)points[0].Y));
                    if (this.selPoints != null)
                    {
                        PointF[] selPoints = this.selPoints;
                        myMatrix.TransformPoints(selPoints);
                        dashedPen.DashStyle = DashStyle.Dash;
                        g.DrawPolygon(dashedPen, selPoints);
                    }
                    if (this.showCursor)
                    {
                        Point cursorPos = this.WorkspacePosition(Cursor.Position);
                        g.DrawEllipse(CursorPen, new Rectangle(cursorPos.X - brushSize, cursorPos.Y - brushSize, brushSize * 2, brushSize * 2));
                    }
                    Image oldImage = this.Image;
                    this.Image = (Image)resultBitmap.Clone();
                    if (oldImage != null)
                    {
                        oldImage.Dispose();
                    }
                }
            }
        }
        /*public async void PrintImage()
        {
            try
            {
                Image result = await Task.Run(() => TransformImage((Image)this.fullImage.Clone()));
                Image oldImage = this.Image;
                this.Image = (Image)result.Clone();
                if (oldImage != null)
                {
                    oldImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }*/
        /*private Bitmap TransformImage(Image image)
        {
            using (Matrix myMatrix = new Matrix())
            {
                PointF[] centerPoints = { this.WorkspacePosition(Cursor.Position) };
                PointF centerPoint = centerPoints[0];
                myMatrix.Scale(this.scale, this.scale, MatrixOrder.Append);
                myMatrix.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
                PointF[] points = { new PointF(0, 0), new PointF(this.fullImage.Width, 0), new PointF(this.fullImage.Width, this.fullImage.Height), new PointF(0, this.fullImage.Height) };
                int brushSize = (int)(this.cursorRadius * this.scale);
                myMatrix.TransformPoints(points);
                using (Bitmap resultBitmap = new Bitmap(this.Width, this.Height))
                using (Pen dashedPen = new Pen(Color.FromArgb(100, Color.Black), 2))//selection
                using (Pen CursorPen = new Pen(Color.FromArgb(100, Color.Gray), 2f))//selection
                using (Graphics g = Graphics.FromImage(resultBitmap))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
                    g.DrawImage(this.fullImage, new Rectangle((int)points[0].X, (int)points[0].Y, (int)points[2].X - (int)points[0].X, (int)points[2].Y - (int)points[0].Y));
                    if (this.selPoints != null)
                    {
                        PointF[] selPoints = this.selPoints;
                        myMatrix.TransformPoints(selPoints);
                        dashedPen.DashStyle = DashStyle.Dash;
                        g.DrawPolygon(dashedPen, selPoints);
                    }
                    if (this.showCursor)
                    {
                        Point cursorPos = this.WorkspacePosition(Cursor.Position);
                        g.DrawEllipse(CursorPen, new Rectangle(cursorPos.X - brushSize, cursorPos.Y - brushSize, brushSize * 2, brushSize * 2));
                    }
                    return resultBitmap;
                }
            }
        }*/
        public Point RemapVisualToTrue(Point inputPoint)
        {
            Point[] inputPoints = { inputPoint };
            Matrix matrix = new Matrix();
            matrix.Scale(this.scale, this.scale);
            matrix.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
            if (matrix.IsInvertible)
            {
                matrix.Invert();
                matrix.TransformPoints(inputPoints);
            }
            return inputPoints[0];
        }
        public Point RemapTrueToVisual(Point inputPoint)
        {
            Point[] inputPoints = { inputPoint };
            Matrix matrix = new Matrix();
            matrix.Scale(this.scale, this.scale);
            matrix.Translate(this.scrollx, this.scrolly, MatrixOrder.Append);
            matrix.TransformPoints(inputPoints);
            return inputPoints[0];
        }
        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
        /*protected override void OnMouseWheel(MouseEventArgs e)
        {
            int scrollDir = Math.Sign(e.Delta);
            float speed = 5.0f;
            float zoomSpeed = 0.1f;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)//horiz
            {
                this.scrollx = this.scrollx + speed * scrollDir;
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)//zoom
            {
                SetScale(this.scale * (1 + scrollDir * zoomSpeed), Cursor.Position);
            }
            else//vert
            {
                this.scrolly = this.scrolly + speed * scrollDir;
            }
            PrintImage();
            base.OnMouseWheel(e);
        }*/
        /*protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && this.panPoint != null)
            {
                Point newPoint = RemapVisualToTrue(this.WorkspacePosition(Cursor.Position));
                int dx = newPoint.X - this.panPoint.X;
                int dy = newPoint.Y - this.panPoint.Y;
                this.scrollx += dx * this.scale;
                this.scrolly += dy * this.scale;
            }
            PrintImage();
            base.OnMouseMove(e);
        }*/
        /*protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.panPoint = RemapVisualToTrue(this.WorkspacePosition(Cursor.Position));
            }
            base.OnMouseDown(e);
        }*/
    }
}