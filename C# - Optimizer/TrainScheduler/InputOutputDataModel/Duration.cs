using System;

namespace TrainScheduler.InputOutputDataModel
{
    public static class IsoDuration
    {
        public static TimeSpan FromString(string str)
        {
            return string.IsNullOrEmpty(str) ? TimeSpan.Zero : System.Xml.XmlConvert.ToTimeSpan(str);
        }

        public static string ToString(TimeSpan time)
        {
            return System.Xml.XmlConvert.ToString(time);
        }
    }
}