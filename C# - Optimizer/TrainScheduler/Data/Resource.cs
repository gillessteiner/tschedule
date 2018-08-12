namespace TrainScheduler.Data
{
    public class Resource
    {
        public string Id { get; private set; }
        public Duration ReleaseTime { get; private set; }
        public bool FollowingAllowed { get; private set; }
    }
}