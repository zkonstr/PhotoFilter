using System.Drawing;
using PhotoFilter.Intern;

namespace PhotoFilter.Filter
{
    public static class ImageConverter
    {
        public static int[,] ToRgbMatrix(Bitmap bmp)
        {
            var imageMatrix = new int[bmp.Height, bmp.Width];
            for (var i = 0; i < bmp.Height; i++)
            for (var j = 0; j < bmp.Width; j++)
                imageMatrix[i, j] = bmp.GetPixel(j, i).ToArgb();

            return imageMatrix;
        }

        public static float[][,] ToHsvMatrices(Bitmap bmp)
        {
            var hMatrix = new float[bmp.Height, bmp.Width];
            var sMatrix = new float[bmp.Height, bmp.Width];
            var vMatrix = new float[bmp.Height, bmp.Width];
            for (var i = 0; i < bmp.Height; i++)
            for (var j = 0; j < bmp.Width; j++)
            {
                var color = bmp.GetPixel(j, i);
                hMatrix[i, j] = color.GetHue();
                sMatrix[i, j] = color.GetSaturation();
                vMatrix[i, j] = color.GetBrightness();
            }

            return new[] {hMatrix, sMatrix, vMatrix};
        }

        public static Bitmap FromRgbMatrix(int[,] imageMatrix)
        {
            var bmp = new Bitmap(imageMatrix.GetLength(1), imageMatrix.GetLength(0));
            for (var i = 0; i < bmp.Height; i++)
            for (var j = 0; j < bmp.Width; j++)
                bmp.SetPixel(j, i, Color.FromArgb(imageMatrix[i, j]));
            return bmp;
        }

        public static Bitmap FromHsvMatrices(float[,] hMatrix, float[,] sMatrix, float[,] vMatrix)
        {
            var bmp = new Bitmap(hMatrix.GetLength(1), hMatrix.GetLength(0));
            for (var i = 0; i < bmp.Height; i++)
            for (var j = 0; j < bmp.Width; j++)
                bmp.SetPixel(j, i, HsvConverter.ToRgb(hMatrix[i, j], sMatrix[i, j], vMatrix[i, j]));
            return bmp;
        }
    }
}