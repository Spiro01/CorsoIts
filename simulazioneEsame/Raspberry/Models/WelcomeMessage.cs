using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Worker.Models;
public class WelcomeMessage
{
    public int DeviceAddress { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
