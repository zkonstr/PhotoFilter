using System;
using System.Drawing;
using System.Drawing.Imaging;
using PhotoFilter.Filter;

namespace PhotoFilter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var strFileName = @"C:\Users\zkons\Pictures\toBitmap.bmp";
            var bitmap = new Bitmap(strFileName);
            var imageFilter = new ImageFilterParallel(bitmap);
            var time = DateTime.Now;
            var bitmapFiltered = imageFilter.Filter( 5, 1);
            Console.Out.WriteLine("Filtered in " + (DateTime.Now - time).Milliseconds + " ms.");
            bitmapFiltered.Save(@"C:\Users\zkons\Pictures\fromBitmap.bmp", ImageFormat.Bmp);
        }
    }
}