using Opc.Ua;
using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ElectroVamp.Domain.Entities
{
    public class OpcData
    {
        public OpcData(string id)
        {
            this.id = id;
        }
        public OpcData()
        {

        }
        public string id { get; set; }
        public string machineid { get; set; }
        public DataValue data { get; set; }

        public static OpcData Parse<T>(string machineid,string id, T data)
        {
            var value = new DataValue() { Value = (T)Convert.ChangeType(data, typeof(T)) };
            return new OpcData() {machineid = machineid, id = id, data = value };
        }
    }
    [Flags]
    public enum OpcEndpoint
    {
        KepServer,
        OpcMachines
    };
    public class Node
    {
        public string id { get; set; }
        public string node { get; set; }
        public int name_space { get; set; }
    }

}
