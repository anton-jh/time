using Time.Models;

namespace Time.Parsing;
internal class TimeStampParser : IParser<TimeStamp>
{
    public TimeStamp? Parse(string word)
    {
        if (word.Length != 5)
        {
            return null;
        }

        string[] parts = word.Split(':');

        if (parts.Length != 2)
        {
            return null;
        }

        if (!int.TryParse(parts[0], out int hours))
        {
            return null;
        }
        if (!int.TryParse(parts[1], out int minutes))
        {
            return null;
        }

        if (hours is < 0 or > 23)
        {
            return null;
        }
        if (minutes is < 0 or > 59)
        {
            return null;
        }

        return new TimeStamp(new TimeOnly(hours, minutes));
    }
}
