using System;
using System.Linq.Expressions;
using Moq;
using Xunit;

namespace Fluent.Tests
{
    /// <summary>
    /// Provides base for the test context classes used in white-box unit tests.
    /// Use if your system under test returns void and you want to assert on dependencies being called.
    /// </summary>
    /// <typeparam name="TSut">Type of system under test.</typeparam>
    public abstract class FluentTest<TSut> : AutoMockedTest<TSut> where TSut : class
    {
        protected Action Exec;

        public SetupStep<TDependency, TResult> And<TDependency, TResult>(Expression<Func<TDependency, TResult>> expression) where TDependency : class
        {
            return new SetupStep<TDependency, TResult>(this, expression);
        }

        public abstract FluentTest<TSut> Then();

        public void ShouldCall<TDependency>(Expression<Action<TDependency>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().Verify(expression, times);
        }

        public void ShouldCallGet<TDependency, TProperty>(Expression<Func<TDependency, TProperty>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().VerifyGet(expression, times);
        }

        public void ShouldCall<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Once());
        }

        public void ShouldNotCall<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Never());
        }

        public void ShouldCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().Verify(expression, times);
        }

        public void ShouldCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Once());
        }

        public void ShouldNotCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Never());
        }

        public void ShouldThrow<TException>() where TException : Exception
        {
            Assert.Throws<TException>(Exec);
        }

        public class SetupStep<T, TResult> where T : class
        {
            private readonly FluentTest<TSut> _parent;
            private readonly Expression<Func<T, TResult>> _expression;

            public SetupStep(FluentTest<TSut> parent, Expression<Func<T, TResult>> expression)
            {
                _parent = parent;
                _expression = expression;
            }

            public FluentTest<TSut> Returns(TResult result)
            {
                _parent.MockFor<T>().Setup(_expression).Returns(result);
                return _parent;
            }

            public FluentTest<TSut> Throws<TException>() where TException : Exception
            {
                _parent.MockFor<T>().Setup(_expression).Throws((TException)Activator.CreateInstance(typeof(TException)));
                return _parent;
            }
        }
    }

    /// <summary>
    /// Provides base for the test context classes used in black-box unit tests.
    /// Use if you want to assert on the result of your system under test.
    /// </summary>
    /// <typeparam name="TSut">Type of system under test.</typeparam>
    /// <typeparam name="TSutResult">Type of value returned from system under test.</typeparam>
    public abstract class FluentTest<TSut, TSutResult> : AutoMockedTest<TSut> where TSut : class
    {
        protected Func<TSutResult> Exec;

        public SetupStep<TDependency, TResult> And<TDependency, TResult>(Expression<Func<TDependency, TResult>> expression) where TDependency : class
        {
            return new SetupStep<TDependency, TResult>(this, expression);
        }

        public abstract FluentTest<TSut, TSutResult> Then();

        public void ShouldCall<TDependency>(Expression<Action<TDependency>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().Verify(expression, times);
        }

        public void ShouldCallGet<TDependency, TProperty>(Expression<Func<TDependency, TProperty>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().VerifyGet(expression, times);
        }

        public void ShouldCall<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Once());
        }

        public void ShouldNotCall<TDependency>(Expression<Action<TDependency>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Never());
        }

        public void ShouldCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression, Times times) where TDependency : class
        {
            Exec();
            MockFor<TDependency>().Verify(expression, times);
        }

        public void ShouldCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Once());
        }

        public void ShouldNotCall<TDependency, TDependencyResult>(Expression<Func<TDependency, TDependencyResult>> expression) where TDependency : class
        {
            ShouldCall(expression, Times.Never());
        }

        public void ShouldThrow<TException>() where TException : Exception
        {
            Assert.Throws<TException>(() => Exec());
        }

        public void ShouldReturnInstanceOf<T>()
        {
            var result = Exec();
            Assert.IsType<T>(result);
        }

        public void ShouldReturn(TSutResult expectedResult)
        {
            var result = Exec();
            Assert.Equal(expectedResult, result);
        }

        public void ShouldReturn(Func<TSutResult, bool> match)
        {
            var result = Exec();
            Assert.True(match(result));
        }

        public class SetupStep<T, TResult> where T : class
        {
            private readonly FluentTest<TSut, TSutResult> _parent;
            private readonly Expression<Func<T, TResult>> _expression;

            public SetupStep(FluentTest<TSut, TSutResult> parent, Expression<Func<T, TResult>> expression)
            {
                _parent = parent;
                _expression = expression;
            }

            public FluentTest<TSut, TSutResult> Returns(TResult result)
            {
                _parent.MockFor<T>().Setup(_expression).Returns(result);
                return _parent;
            }

            public FluentTest<TSut, TSutResult> Throws<TException>() where TException : Exception
            {
                _parent.MockFor<T>().Setup(_expression).Throws((TException)Activator.CreateInstance(typeof(TException)));
                return _parent;
            }
        }
    }
}
