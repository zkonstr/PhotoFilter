using System.Drawing;

namespace PhotoFilter.Filter
{
    public class ImageFilterParallel
    {
        public ImageFilterParallel(Bitmap image)
        {
            Image = image;
        }

        public Bitmap Image { get; set; }

        public Bitmap Filter(int radius, int threadsCount)
        {
            var pixelMatrices = ImageConverter.ToHsvMatrices(Image);
            var filteredMatrices = new float[3][,];
            for (var i = 0; i < pixelMatrices.Length; i++)
            {
                var filter = new MatrixFilterParallel(pixelMatrices[i], threadsCount);
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