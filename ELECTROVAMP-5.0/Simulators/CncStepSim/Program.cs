
using CncStepSim;

namespace CncStepSim
{
    public class Program
    {
        public async static Task Main()
        {
            Random ran = new Random();
            MariadbMachine mdm = new MariadbMachine("Server=localhost;Port=3306;Database=statistics;Uid=root;Pwd=admin;");
            var mdm2 = mdm;
            while (true)
            {
                await mdm.ResetDb("Program1");
                await mdm.ResetDb("Program2");
                var programid = await mdm.AddProgram("Program1");
                var programid2 = await mdm.AddProgram("Program2");
                await mdm.AddWork("9", programid);
                await mdm.AddWork("8", programid2);
                int counter;
                int counter2;
                while (true)
                {
                    counter = await mdm.PieceDone("9", ran.Next(200,3000));
                    counter = await mdm.PieceDone("8", ran.Next(200, 3000));
                    Console.WriteLine("Machine1 " + counter);
                    Console.WriteLine("machine2 "+counter);
                    if (await mdm.ProgramDone("8")) break;
                }
            }
        }
    }
}