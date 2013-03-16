using System.Drawing;

namespace Color_Helper
{
    class ColorHelper
    {
        public int GetCOLORFromRGBValue(char cColor, int nRGBValue)
        {
            int returnValue = -1;
            switch (cColor)
            {
                case 'R':
                    returnValue = nRGBValue & 0xFF;
                    break;
                case 'G':
                    returnValue = nRGBValue >> 8 & 0xFF;
                    break;
                case 'B':
                    returnValue = nRGBValue >> 16 & 0xFF;
                    break;
                default:
                    returnValue = -1;
                    break;
            }
            return returnValue;
        }

        public Image GetCircleBitmap(Size size, Color col)
        {
            Bitmap bmp2 = new Bitmap(size.Width, size.Height);

            using (Bitmap bmp = new Bitmap(size.Width, size.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(col);
                }

                using (TextureBrush t = new TextureBrush(bmp))
                {
                    using (Graphics g = Graphics.FromImage(bmp2))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        g.FillEllipse(t, 0, 0, bmp2.Width, bmp2.Height);
                    }
                }
            }

            return bmp2;
        }

    }
}
