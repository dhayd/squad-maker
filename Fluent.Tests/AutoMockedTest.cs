using Autofac.Extras.Moq;
using Moq;

namespace Fluent.Tests
{
    public abstract class AutoMockedTest<TSut> where TSut : class
    {
        private readonly AutoMock _autoMocker;

        protected TSut Sut { get; private set; }

        protected AutoMockedTest()
        {
            _autoMocker = AutoMock.GetLoose();
            Sut = CreateSut();
        }

        protected virtual TSut CreateSut()
        {
            return _autoMocker.Create<TSut>();
        }

        protected Mock<TDependency> MockFor<TDependency>() where TDependency : class
        {
            return _autoMocker.Mock<TDependency>();
        }

        protected void RegisterDependency<T>(T dependancy) where T : class
        {
            _autoMocker.Provide(dependancy);
        }
    }
}
