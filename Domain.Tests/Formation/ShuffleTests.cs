using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Formation;
using Xunit;

namespace Domain.Tests.Formation
{
    public class ShuffleTests
    {
        [Fact]
        public void ShouldReturnEmptyForEmptySource()
        {
            WhenShuffling(Enumerable.Empty<int>())
                .ShouldReturn(Enumerable.Empty<int>());
        }

        [Fact]
        public void ShouldReturnSingleForSingleSource()
        {
            WhenShuffling(new []{42})
                .ShouldReturn(new []{42});
        }

        [Fact]
        public void ShouldReturnSameNumberOfElements()
        {
            WhenShuffling(Enumerable.Range(1, 150))
                .ShouldReturn(x => x.Count() == 150);
        }

        [Fact]
        public void ShouldReturnSameSetOfElements()
        {
            WhenShuffling(Enumerable.Range(42, 150))
                .ShouldReturn(x => x.OrderBy(e => e).SequenceEqual(Enumerable.Range(42, 150)));
        }

        [Fact]
        public void ShouldReturnElementsInDifferentOrder()
        {
            WhenShuffling(Enumerable.Range(42, 150))
                .ShouldReturn(x => !x.SequenceEqual(Enumerable.Range(42, 150)));
        }

        [Fact]
        public void ShouldReturnDifferentSequencesWhenCalledTwice()
        {
            var source = Enumerable.Range(42, 150).ToArray();
            var firstShuffle = source.Shuffle();

            WhenShuffling(source)
                .ShouldReturn(x => !x.SequenceEqual(firstShuffle));
        }
        private ShuffleFluentTests WhenShuffling(IEnumerable<int> source)
        {
            return new ShuffleFluentTests(source);
        }
        private class ShuffleFluentTests
        {
            private readonly IEnumerable<int> _source;

            public ShuffleFluentTests(IEnumerable<int> source)
            {
                _source = source;
            }

            public void ShouldReturn(IEnumerable<int> expected)
            {
                var actual = _source.Shuffle();
                Assert.Equal(expected, actual);
            }

            public void ShouldReturn(Func<IEnumerable<int>, bool> expectation)
            {
                var actual = _source.Shuffle();
                Assert.True(expectation(actual));
            }
        }
    }
}
