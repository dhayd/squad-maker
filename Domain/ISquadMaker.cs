using Domain.Model;

namespace Domain
{
    public interface ISquadMaker
    {
        ISquadsSetup Make(int numberOfSquads);
    }
}
