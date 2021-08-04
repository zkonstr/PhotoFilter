using System;
using System.Collections.Generic;
using System.Threading;

namespace PhotoFilter.Filter
{
    internal class FilterThread
    {
        private readonly int _beginIndex;
        private readonly int _endIndex;
        private readonly float[,] _matrix;
        private readonly int _radius;

        public FilterThread(int beginIndex, int endIndex, int radius, float[,] matrix)
        {
            _beginIndex = beginIndex;
            _endIndex = endIndex;
            Filtered = new float[endIndex - beginIndex];
            _radius = radius;
            _matrix = matrix;
        }

        public float[] Filtered { get; }

        private float DoFilter(float[,] matrix, int x, int y)
        {
            var neighbors = new List<float>((_radius + 2) * (_radius + 2));
            for (var i = x - _radius; i <= x + _radius; i++)
            for (var j = y - _radius; j <= y + _radius; j++)
            {
                if (i < 0 || j < 0 || i >= matrix.GetLength(0) || j >= matrix.GetLength(1)) continue;

                neighbors.Add(matrix[i, j]);
            }

            neighbors.Sort();
            if (neighbors.Count % 2 == 0) return neighbors[(neighbors.Count / 2 - 1 + neighbors.Count / 2) / 2];

            return neighbors[neighbors.Count / 2];
        }

        public void Filter()
        {
            for (var k = _beginIndex; k < _endIndex; k++)
            {
                var i = k / _matrix.GetLength(1);
                var j = k % _matrix.GetLength(1);
                Filtered[k - _beginIndex] = DoFilter(_matrix, i, j);
            }
        }
    }

    public class MatrixFilterParallel
    {
        private readonly int _threadsCount;

        public MatrixFilterParallel(float[,] matrix, int threadsCount)
        {
            Matrix = matrix;
            _threadsCount = threadsCount;
        }

        public float[,] Matrix { get; set; }

        public float[,] Filter(int radius)
        {
            var threads = new FilterThread[_threadsCount];
            for (var i = 0; i < threads.Length; i++)
            {
                var cellsPerThread = Matrix.Length / _threadsCount;
                threads[i] = new FilterThread(cellsPerThread * i,
                    Math.Min(cellsPerThread * (i + 1), Matrix.Length),
                    radius, Matrix
                );
                var thread = new Thread(threads[i].Filter);
                thread.Start();
                thread.Join();
            }

            var filtered = new float[Matrix.GetLength(0), Matrix.GetLength(1)];
            var k = 0;
            foreach (var thread in threads)
            foreach (var item in thread.Filtered)
            {
                var i = k / Matrix.GetLength(1);
                var j = k % Matrix.GetLength(1);
                filtered[i, j] = item;
                k++;
            }

            return filtered;
        }
    }
}