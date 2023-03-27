public class LogSwap
{

    private static void Main(string[] args)
    {
        StartSwap();
    }

    public static void StartSwap()
    {

        string pathSource = Directory.GetCurrentDirectory() + "\\source";
        string pathDest1 = Directory.GetCurrentDirectory() + "\\dest\\Salvagnini1";
        string pathDest2 = Directory.GetCurrentDirectory() + "\\dest\\Salvagnini2";

        var sourceFiles = Directory.EnumerateFiles(pathSource).OrderBy(x => x);

        Directory.CreateDirectory(pathDest1);
        Directory.CreateDirectory(pathDest2);


        //pulizia cartella destinazione
        foreach (var filename in Directory.GetFiles(pathDest1))
        {
            File.Delete(filename);
        }

        foreach (var filename in Directory.GetFiles(pathDest2))
        {
            File.Delete(filename);
        }

        Random r = new Random();

        Console.WriteLine("Percorso destinazione: " + pathDest1);
        Console.WriteLine("Percorso destinazione: " + pathDest2 + "\n");

        var alltxt = Directory.EnumerateFiles(pathSource, "*.txt").OrderBy(x => x).ToList();
        var allsyn = Directory.EnumerateFiles(pathSource, "*.syn").OrderBy(x => x).ToList();
        do
        {
            //NextInt64(minimo file da muovere, massimo file da muovere) per due per muovere sempre a coppie
            System.Console.WriteLine("\nMoving files");


            if (alltxt.Count == 0)
            {
                break;
            }

            try
            {
                File.Copy(alltxt.First(), pathDest1 + "\\" + Path.GetFileName(alltxt.First()));
                File.Copy(alltxt.First(), pathDest2 + "\\" + Path.GetFileName(alltxt.First()));
                Task.Delay(10).Wait();
                File.Copy(allsyn.First(), pathDest1 + "\\" + Path.GetFileName(allsyn.First()));
                File.Copy(allsyn.First(), pathDest2 + "\\" + Path.GetFileName(allsyn.First()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException());
            }

            alltxt.Remove(alltxt.First());
            allsyn.Remove(allsyn.First());


            //min e max millisecondi da aspettare prima di muovere altri file
            int t = (int)r.NextInt64(1000, 2000);
            System.Console.WriteLine("Sleeping " + t + " ms");

            Thread.Sleep(t);

        } while (alltxt.Count != 0);

    }

    public static string GetPathSource()
    {
        return Directory.GetCurrentDirectory() + "\\source";
    }

    public static string GetPathDest()
    {
        return Directory.GetCurrentDirectory() + "\\dest";
    }

}