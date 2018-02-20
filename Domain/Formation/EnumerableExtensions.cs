using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Formation
{
    public static class EnumerableExtensions
    {
        private static readonly Random _random = new Random();

        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new {Index = i, Element = x})
                .GroupBy(x => x.Index / chunkSize)
                .Select(g => g.Select(x => x.Element));
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var shuffle = new List<T>(source);

            for (int i = 0; i < shuffle.Count; i++)
            {
                int randomPosition = _random.Next(0, shuffle.Count - 1);
                T temp = shuffle[i];
                shuffle[i] = shuffle[randomPosition];
                shuffle[randomPosition] = temp;
            }

            return shuffle;
        }

        public static IEnumerable<T> RandomSwap<T>(this IEnumerable<T> source)
        {
            var swapped = new List<T>(source);
            if (swapped.Count < 2)
            {
                return swapped;
            }

            int randomPositionLeft = _random.Next(0, swapped.Count - 2);
            int randomPositionRight = _random.Next(randomPositionLeft + 1, swapped.Count - 1);
            T temp = swapped[randomPositionLeft];
            swapped[randomPositionLeft] = swapped[randomPositionRight];
            swapped[randomPositionRight] = temp;

            return swapped;
        }
    }
}
