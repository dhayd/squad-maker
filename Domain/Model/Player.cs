namespace Domain.Model
{

    internal class Player : IPlayer
    {
        public Player(string id, string firstName, string lastName, ISkills skills)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Skills = skills;
        }

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName => $"{FirstName} {LastName}";
        public ISkills Skills { get; }
    }
}