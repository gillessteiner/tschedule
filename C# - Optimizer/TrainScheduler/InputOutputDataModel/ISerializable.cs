namespace TrainScheduler.InputOutputDataModel
{
    interface ISerializable
    {
        // Implemented in base class Serializable
        string ToJson();
        void FromJson(string json);
    }
}
