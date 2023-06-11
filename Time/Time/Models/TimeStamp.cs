namespace Time.Models;
internal class TimeStamp
{
    public TimeStamp(int hours, int minutes)
    {
        if (hours is < 0 or > 23)
        {
            throw new ArgumentException($"{nameof(hours)} must be between 0 and 23.", nameof(hours));
        }
        if (minutes is < 0 or > 59)
        {
            throw new ArgumentException($"{nameof(minutes)} must be between 0 and 59.", nameof(minutes));
        }

        Hours = hours;
        Minutes = minutes;
    }


    public int Hours { get; }
    public int Minutes { get; }
    public int MinutesSinceMidnight => Hours * 60 + Minutes;


    public static bool operator <(TimeStamp a, TimeStamp b)
    {
        return a.MinutesSinceMidnight < b.MinutesSinceMidnight;
    }
    public static bool operator >(TimeStamp a, TimeStamp b)
    {
        return a.MinutesSinceMidnight > b.MinutesSinceMidnight;
    }

    public static bool operator ==(TimeStamp a, TimeStamp b)
    {
        return a.Hours == b.Hours && a.Minutes == b.Minutes;
    }
    public static bool operator !=(TimeStamp a, TimeStamp b)
    {
        return !(a == b);
    }

    public static bool operator <=(TimeStamp a, TimeStamp b)
    {
        return !(a > b);
    }
    public static bool operator >=(TimeStamp a, TimeStamp b)
    {
        return !(a < b);
    }

    public override bool Equals(object? obj)
    {
        return obj is TimeStamp other && this == other;
    }

    public override int GetHashCode()
    {
        return MinutesSinceMidnight;
    }

    public override string ToString()
    {
        return string.Format("{0:00}:{1:00}", Hours, Minutes);
    }
}
