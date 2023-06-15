using Time.Exceptions;
using Time.Models;

namespace Time.Parsing;
internal class TimeStampParser : IParser<TimeStamp>
{
    public TimeStamp Parse(string word)
    {
        if (word.Length != 5)
        {
            ThrowHelper();
        }

        string[] parts = word.Split(':');

        if (parts.Length != 2)
        {
            ThrowHelper();
        }

        if (!int.TryParse(parts[0], out int hours))
        {
            ThrowHelper();
        }
        if (!int.TryParse(parts[1], out int minutes))
        {
            ThrowHelper();
        }

        if (hours is < 0 or > 23)
        {
            ThrowHelper();
        }
        if (minutes is < 0 or > 59)
        {
            ThrowHelper();
        }

        return new TimeStamp(new TimeOnly(hours, minutes));
    }


    private static void ThrowHelper()
    {
        throw new UserErrorException("Invalid timestamp");
    }
}
