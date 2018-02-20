using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Formation;
using Xunit;

namespace Domain.Tests.Formation
{
    public class RandomSwapTests
    {
        [Fact]
        public void ShouldReturnEmptyForEmptySource()
        {
            WhenSwappingRandomElements(Enumerable.Empty<int>())
                .ShouldReturn(Enumerable.Empty<int>());
        }

        [Fact]
        public void ShouldReturnSingleForSingleSource()
        {
            WhenSwappingRandomElements(new[] { 42 })
                .ShouldReturn(new[] { 42 });
        }

        [Fact]
        public void ShouldReturnSameNumberOfElements()
        {
            WhenSwappingRandomElements(Enumerable.Range(1, 150))
                .ShouldReturn(x => x.Count() == 150);
        }

        [Fact]
        public void ShouldReturnSameSetOfElements()
        {
            WhenSwappingRandomElements(Enumerable.Range(42, 150))
                .ShouldReturn(x => x.OrderBy(e => e).SequenceEqual(Enumerable.Range(42, 150)));
        }

        [Fact]
        public void ShouldOnlyDifferInTwoElemets()
        {
            WhenSwappingRandomElements(Enumerable.Range(42, 150))
                .ShouldMatchSourceElements(148);
        }

        [Fact]
        public void ShouldReturnDifferentWhenCalledTwice()
        {
            var source = Enumerable.Range(42, 350).ToArray();
            var firstSwap = source.RandomSwap();

            WhenSwappingRandomElements(source)
                .ShouldReturn(x => !x.SequenceEqual(firstSwap));
        }

        private RandomSwapFluentTest WhenSwappingRandomElements(IEnumerable<int> source)
        {
            return new RandomSwapFluentTest(source);
        }
        private class RandomSwapFluentTest
        {
            private readonly IEnumerable<int> _source;

            public RandomSwapFluentTest(IEnumerable<int> source)
            {
                _source = source;
            }

            public void ShouldReturn(IEnumerable<int> expected)
            {
                var actual = _source.RandomSwap();
                Assert.Equal(expected, actual);
            }

            public void ShouldReturn(Func<IEnumerable<int>, bool> expectation)
            {
                var actual = _source.RandomSwap();
                Assert.True(expectation(actual));
            }

            public void ShouldMatchSourceElements(int numberOfElements)
            {
                var actual = _source.RandomSwap().ToArray();
                var matches = _source
                    .Select((x, i) => new {Index = i, Element = x})
                    .Count(x => actual[x.Index] == x.Element);

                Assert.Equal(numberOfElements, matches);
            }
        }
    }
}
