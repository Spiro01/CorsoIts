using System.Net;

public static class Extensions
{
    public static int[] SplitIpAddress(this IPAddress Ip)
    {
        var splittedIpInt = new List<int>();

        foreach (string splitIp in Ip.ToString().Split(".", 4))
        {
            splittedIpInt.Add(int.Parse(splitIp));
        }
        return splittedIpInt.ToArray();
    }

    public static IPAddress MergeIpAddress(this int[] SplittedIpInt)
    {
        string ris = "";

        foreach (var splitIp in SplittedIpInt) ris = ris + splitIp + ".";
        ris = ris.Remove(ris.Length - 1);

        return IPAddress.Parse(ris);

    }



}