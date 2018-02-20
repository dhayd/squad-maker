namespace Domain.Model
{
    internal class Skills : ISkills
    {
        public Skills(int shooting, int skating, int checking)
        {
            Shooting = shooting;
            Skating = skating;
            Checking = checking;
        }

        public int Shooting { get; }
        public int Skating { get; }
        public int Checking { get; }
    }
}