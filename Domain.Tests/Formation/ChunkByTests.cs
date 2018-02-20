using System.Collections.Generic;
using System.Linq;
using Domain.Formation;
using Xunit;

namespace Domain.Tests.Formation
{
    public class ChunkByTests
    {
        [Fact]
        public void ShouldReturnEmptyForEmpty()
        {
            WhenChunking(Enumerable.Empty<int>()).By(5).ShouldReturn(Enumerable.Empty<IEnumerable<int>>());
        }

        [Fact]
        public void ShouldReturnSingleIncompleteWhenChunkSizeLesserThanSourceCount()
        {
            WhenChunking(new []{1,2,3}).By(5).ShouldReturn(new []{new []{1,2,3}});
        }

        [Fact]
        public void ShouldReturnSingleCompleteWhenChunkSizeEqualsSourceCount()
        {
            WhenChunking(Enumerable.Range(1,5)).By(5).ShouldReturn(new []{Enumerable.Range(1,5)});
        }

        [Fact]
        public void ShouldReturnTwoChunksWhenChunkSizeIs4AndSourceCountIs6()
        {
            WhenChunking(Enumerable.Range(1,6)).By(4).ShouldReturn(new []{new []{1,2,3,4}, new []{5,6}});
        }

        [Fact]
        public void ShouldReturnFourChunksWhenChunkSizeIs3AndSourceCountIs11()
        {
            WhenChunking(Enumerable.Range(1,11)).By(3).ShouldReturn(new []
            {
                new []{1,2,3},
                new []{4,5,6},
                new []{7,8,9},
                new []{10,11}
            });
        }

        private ChunkByFluentTests WhenChunking(IEnumerable<int> source)
        {
            return new ChunkByFluentTests(source);
        }
        private class ChunkByFluentTests
        {
            private readonly IEnumerable<int> _source;
            private int _chunkSize;

            public ChunkByFluentTests(IEnumerable<int> source)
            {
                _source = source;
            }

            public ChunkByFluentTests By(int chunkSize)
            {
                _chunkSize = chunkSize;
                return this;
            }

            public void ShouldReturn(IEnumerable<IEnumerable<int>> expected)
            {
                var actual = _source.ChunkBy(_chunkSize);
                Assert.Equal(expected, actual);
            }
        }
    }
}
