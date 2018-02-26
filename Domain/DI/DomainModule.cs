using Autofac;
using Domain.Formation;
using Domain.Serialization;

namespace Domain.DI
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayersRepository>().As<IPlayersRepository>();

            builder.RegisterType<HillClimbingSquadMaker>().As<ISquadMaker>();

            builder.RegisterType<AverageSkillCalculator>().As<IAverageSkillCalculator>();
            builder.RegisterType<DistanceCalculator>().As<IDistanceCalculator>();
            builder.RegisterType<SetupSourceFactory>().As<ISetupSourceFactory>();
            builder.RegisterType<SquadsSetupFactory>().As<ISquadsSetupFactory>();

            builder.RegisterType<PlayerMapper>().As<IPlayerMapper>();
            builder.RegisterType<PlayersFileReader>().As<IPlayerDataProvider>();
        }
    }
}
