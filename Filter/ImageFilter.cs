using System.Drawing;

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
            var pixelMatrix = ImageConverter.ToRgbMatrix(Image);
            MatrixFilter filter = new MatrixFilter(pixelMatrix);
            var filtered = filter.Filter(radius);
            return ImageConverter.FromRgbMatrix(filtered);
        }
    }
}