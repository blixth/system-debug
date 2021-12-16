using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SystemDebug
{
    public static class SystemDebugCollector
    {
        public static void GatherDebugInformation(string debugPath)
        {
            var screenLeft = SystemInformation.VirtualScreen.Left;
            var screenTop = SystemInformation.VirtualScreen.Top;
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            try
            {
                using (var bmp = new Bitmap(screenWidth, screenHeight))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                    }

                    var date = DateTime.UtcNow;
                    var filename =
                        $"{date.Year}-{date.Month}-{date.Day} {date.Hour}_{date.Minute}_{date.Second}_{date.Millisecond}.png";


                    bmp.Save($"{debugPath}\\{filename}", ImageFormat.Png);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}