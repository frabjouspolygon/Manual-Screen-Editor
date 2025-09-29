using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace Manual_Screen_Renderer
{
    public class ClickThroughPBX : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }
        public ClickThroughPBX()
        {
            this.DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                m.Result = new IntPtr(-1);
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
    }
}