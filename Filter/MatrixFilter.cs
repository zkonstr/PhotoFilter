using System.Collections.Generic;

namespace PhotoFilter.Filter
{
    public class MatrixFilter
    {
        public MatrixFilter(float[,] matrix)
        {
            Matrix = matrix;
        }

        public float[,] Matrix { get; set; }

        public float[,] Filter(int radius)
        {
            var filtered = new float[Matrix.GetLength(0), Matrix.GetLength(1)];
            for (var i = 0; i < Matrix.GetLength(0); i++)
            for (var j = 0; j < Matrix.GetLength(1); j++)
                filtered[i, j] = DoFilter(i, j, radius);

            return filtered;
        }

        private float DoFilter(int x, int y, int radius)
        {
            var neighbors = new List<float>((radius + 2) * (radius + 2));
            for (var i = x - radius; i <= x + radius; i++)
            for (var j = y - radius; j <= y + radius; j++)
            {
                if (i < 0 || j < 0 || i >= Matrix.GetLength(0) || j >= Matrix.GetLength(1)) continue;

                neighbors.Add(Matrix[i, j]);
            }

            neighbors.Sort();
            if (neighbors.Count % 2 == 0) return neighbors[(neighbors.Count / 2 - 1 + neighbors.Count / 2) / 2];

            return neighbors[neighbors.Count / 2];
        }
    }
}