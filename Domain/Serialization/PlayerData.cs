namespace Domain.Serialization
{
    public class PlayerData
    {
        public string _id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public SkillData[] skills { get; set; }
    }
}