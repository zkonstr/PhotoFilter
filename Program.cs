using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PhotoFilter.Filter;

namespace PhotoFilter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Out.WriteLine("The path is not specified");
                Environment.Exit(-1);
            }

            if (Path.GetExtension(args[0]) == "bmp")
            {
                Console.Out.WriteLine("Not a bmp!");
                Environment.Exit(-1);
            }
            var strFileName = args[0];
            Console.Out.WriteLine(strFileName);
            var bitmap = new Bitmap(strFileName);
            var imageFilter = new ImageFilterParallel(bitmap);
            Console.Out.WriteLine("Image loaded");            
            int radius = args.Length >= 2 ? Int32.Parse(args[1]) : 1;
            int threadsCount = args.Length >= 3 ? Int32.Parse(args[2]) : 1;
            Console.WriteLine("Filtering image...");
            var time = DateTime.Now;
            var bitmapFiltered = imageFilter.Filter(radius, threadsCount);
            Console.Out.WriteLine("Filtered in " + (DateTime.Now - time).Milliseconds + " ms.");
            bitmapFiltered.Save(@"C:\Users\zkons\Pictures\fromBitmap.bmp", ImageFormat.Bmp);
        }
    }
}