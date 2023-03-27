using System.Reflection;
namespace WebViewer.Data
{
    public class BrowseResult
    {
        public string id { get; set; }
    }

    public class KepServerList
    {
        public List<BrowseResult> browseResults { get; set; }
        public bool succeeded { get; set; }
        public string reason { get; set; }
    }


}

public class ReadResult
{
    public string id { get; set; }
    public bool s { get; set; }
    public string r { get; set; }
    public object v { get; set; }
    public long t { get; set; }

   
}

public class KepServerResults
{
    public List<ReadResult> readResults { get; set; }
}

