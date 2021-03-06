using System.Drawing;

namespace PhotoFilter.Filter
{
    public class ImageFilter
    {
        public ImageFilter(Bitmap image)
        {
            Image = image;
        }

        public Bitmap Image { get; set; }

        public Bitmap Filter(int radius)
        {
            var pixelMatrices = ImageConverter.ToHsvMatrices(Image);
            var filteredMatrices = new float[3][,];
            for (var i = 0; i < pixelMatrices.Length; i++)
            {
                var filter = new MatrixFilter(pixelMatrices[i]);
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