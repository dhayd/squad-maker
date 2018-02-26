namespace Domain.Model
{
    public interface IPlayer
    {
        string Id { get; }

        string FirstName { get; }

        string LastName { get; }

        string FullName { get; }

        ISkills Skills { get; } 
    }
}