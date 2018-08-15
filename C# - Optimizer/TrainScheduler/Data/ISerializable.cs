namespace TrainScheduler.Data
{
    interface IDeSerializable
    {
        void FromJson(string json);
    }

    interface ISerializable
    {
        string ToJson();
    }
}
