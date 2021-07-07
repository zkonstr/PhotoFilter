using System.Drawing;

namespace PhotoFilter.Filter
{
    public static class ImageConverter
    {
        public static int[,] ToRgbMatrix(Bitmap bpm)
        {
            var imageMatrix = new int[bpm.Height, bpm.Width];
            for (int i = 0; i < bpm.Height; i++)
            {
                for (int j = 0; j < bpm.Width; j++)
                {
                    imageMatrix[i, j] = bpm.GetPixel(j, i).ToArgb();
                }
            }

            return imageMatrix;
        }

        public static Bitmap FromRgbMatrix(int[,] imageMatrix)
        {
            Bitmap bmp = new Bitmap(imageMatrix.GetLength(1), imageMatrix.GetLength(0));
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    imageMatrix[i, j] = bmp.GetPixel(j, i).ToArgb();
                    bmp.SetPixel(j, i, Color.FromArgb(imageMatrix[i, j]));
                }
            }

            return bmp;
        }
    }
}