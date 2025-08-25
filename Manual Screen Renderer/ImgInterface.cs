using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
                                                                                                                                                                                                                                                                                                                                                                                                                              
namespace Manual_Screen_Renderer
{
    class ImgInterface
    {
        public static System.Windows.Forms.TextBox textBox { get; set; }
        public static System.Windows.Forms.Button button { get; set; }
        public Bitmap image { get; set; }

        public ImgInterface(System.Windows.Forms.TextBox iitextBox, System.Windows.Forms.Button iibutton)
        {
            textBox = iitextBox;
            button = iibutton;
            image = null;
        }

        public bool UpdateFiles( string filePath)
        {
            try
            {
                var myBitmap = new Bitmap(filePath);
                textBox.Text = filePath;
                image = myBitmap;
                return true;
            }
            catch
            {
                textBox.Text = "";
                image = null;
                return false;
            }
        }
    }
}
