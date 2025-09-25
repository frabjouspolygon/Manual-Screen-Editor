using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Manual_Screen_Renderer
{
    public class ScrollingPanel : Panel
    {

        public ScrollingPanel()
        {
            this.DoubleBuffered = true;
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.VScroll && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)//horiz
            {
                this.VScroll = false;
                base.OnMouseWheel(e);
                this.VScroll = true;
            }
            else if (this.VScroll && (Control.ModifierKeys & Keys.Control) == Keys.Control)//zoom
            {
                if (this.HScroll && this.VScroll)
                {
                    this.VScroll = false;
                    this.HScroll = false;
                    base.OnMouseWheel(e);
                    this.VScroll = true;
                    this.HScroll = true;
                }
                else if (this.VScroll)
                {
                    this.VScroll = false;
                    base.OnMouseWheel(e);
                    this.VScroll = true;
                }
                else if (this.HScroll)
                {
                    this.HScroll = false;
                    base.OnMouseWheel(e);
                    this.HScroll = true;
                }
                else
                {
                    base.OnMouseWheel(e);
                }
                
            }
            else//vert
            {
                base.OnMouseWheel(e);
            }
        }
    }
}