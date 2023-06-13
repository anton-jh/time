using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time.Models;
internal class SubSegment
{
    public SubSegment(int hours, int minutes, Label label)
    {
        if (hours is < 0)
        {
            throw new ArgumentException($"{nameof(hours)} cannot be negative.", nameof(hours));
        }
        if (minutes is < 0 or > 59)
        {
            throw new ArgumentException($"{nameof(minutes)} must be between 0 and 59.", nameof(minutes));
        }

        Hours = hours;
        Minutes = minutes;
        Label = label;
    }


    public int Hours { get; }
    public int Minutes { get; }
    public Label Label { get; }

    public int TotalMinutes => Hours * 60 + Minutes;


    public static bool operator <(SubSegment a, SubSegment b)
    {
        return a.TotalMinutes < b.TotalMinutes;
    }
    public static bool operator >(SubSegment a, SubSegment b)
    {
        return a.TotalMinutes > b.TotalMinutes;
    }

    public static bool operator ==(SubSegment a, SubSegment b)
    {
        return a.Hours == b.Hours && a.Minutes == b.Minutes;
    }
    public static bool operator !=(SubSegment a, SubSegment b)
    {
        return !(a == b);
    }

    public static bool operator <=(SubSegment a, SubSegment b)
    {
        return !(a > b);
    }
    public static bool operator >=(SubSegment a, SubSegment b)
    {
        return !(a < b);
    }

    public override bool Equals(object? obj)
    {
        return obj is SubSegment other && this == other;
    }

    public override int GetHashCode()
    {
        return TotalMinutes;
    }

    public override string ToString()
    {
        return string.Format("{0:00}:{1:00}", Hours, Minutes);
    }
}
