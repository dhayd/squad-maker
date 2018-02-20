using System;
using System.Linq;
using System.Linq.Expressions;
using Moq;

namespace Fluent.Tests
{
    public static class MockExtensions
    {
        public static Mock<T>[] MocksOf<T>(this int count) where T : class
        {
            return Enumerable.Range(0, count).Select(x => new Mock<T>()).ToArray();
        }

        public static T[] Objects<T>(this Mock<T>[] mocks) where T : class
        {
            return mocks.Select(x => x.Object).ToArray();
        }

        public static T[] ObjectsOf<T>(this int count) where T : class
        {
            return MocksOf<T>(count).Objects();
        }

        public static void VerifyAll<T>(this Mock<T>[] mocks, Expression<Action<T>> expression, Func<Times> times)
            where T : class
        {
            foreach (var mock in mocks)
            {
                mock.Verify(expression, times);
            }
        }
    }
}
