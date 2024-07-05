using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TPL_PS2_EXTRACT
{
    public class SimpleBitmap
    {
        public int Width { get => width; }
        public int Height { get => height; }
        public byte[] BitmapData { get => bitmapData; }

        private int width;
        private int height;
        private byte[] bitmapData;
       
        public SimpleBitmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            bitmapData = new byte[width * height * 4];
        }

        public Color GetPixel(int x, int y)
        {
            int index = (y * 4 * width) + (x * 4); 
            return Color.FromArgb(bitmapData[index + 3], bitmapData[index + 2], bitmapData[index + 1], bitmapData[index + 0]);
        }

        public void SetPixel(int x, int y, Color color)
        {
            int index = (y * 4 * width) + (x * 4);
            bitmapData[index + 0] = color.B;
            bitmapData[index + 1] = color.G;
            bitmapData[index + 2] = color.R;
            bitmapData[index + 3] = color.A;
        }
    }

}
