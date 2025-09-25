using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Manual_Screen_Renderer
{
    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }
        public PictureBoxWithInterpolationMode()
        {
            this.DoubleBuffered = true;
        }
        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
    }
}