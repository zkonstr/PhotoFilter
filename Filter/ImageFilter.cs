using System.Drawing;
using System.Drawing.Drawing2D;

namespace PhotoFilter.Filter
{
    public class ImageFilter
    {
        public Bitmap Image { get; set; }

        public ImageFilter(Bitmap image)
        {
            Image = image;
        }

        public Bitmap Filter(int radius)
        {
            var pixelMatrices = ImageConverter.ToHsvMatrices(Image);
            float[][,] filteredMatrices = new float[3][,];
            for (var i = 0; i < pixelMatrices.Length; i++)
            {
                MatrixFilter filter = new MatrixFilter(pixelMatrices[i]);
                filteredMatrices[i] = filter.Filter(radius);
            }

            return ImageConverter.FromHsvMatrices(
                filteredMatrices[0],
                filteredMatrices[1],
                filteredMatrices[2]
            );
        }
    }
}