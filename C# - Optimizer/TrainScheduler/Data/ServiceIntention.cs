namespace TrainScheduler.Data
{
    public class ServiceIntention
    {
        public int Id { get; private set; }
        public int Route { get; private set; }
        private SectionRequirements[] sectionRequirements;

    }
}