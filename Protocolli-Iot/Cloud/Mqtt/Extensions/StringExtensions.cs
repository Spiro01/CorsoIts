using System.Reflection.Metadata.Ecma335;

namespace Mqtt.Extensions;

public static class StringExtensions
{
    public static bool IsSameTopic(this string currentTopic, string compareTopic)
    {
        if (currentTopic == compareTopic) return true;

        var currentTopicSplit = currentTopic.Split("/");
        var compareTopicSplit = compareTopic.Split("/");

        if (!currentTopic.Contains("+")) return false;

        for (int i = 0; i < currentTopicSplit.Length; i++)
        {
            if (i >= compareTopicSplit.Length) return false;
            if (currentTopicSplit[i] == compareTopicSplit[i]) continue;
            if (currentTopicSplit.Contains("#")) return true;
            if (currentTopicSplit[i] == "+") continue;

            return false;
        }



        return true;
    }
}