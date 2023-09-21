using System.Globalization;

namespace Server.ActionFilters;

internal class DefaultMessage
{
    public DefaultMessage(int statusCode, dynamic value)
    {
        StatusCode = statusCode;
        ExecutedOn = DateTime.Now.ToString("R",CultureInfo.CurrentCulture);
        Value = value;
    }

    public int StatusCode { get; private set; }
    public string ExecutedOn { get; private set; }
    public dynamic Value { get; private set; }
    
}