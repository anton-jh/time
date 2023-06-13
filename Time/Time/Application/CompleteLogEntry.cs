using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.Application;
internal class CompleteLogEntry
{   
    public CompleteLogEntry(TimeStamp start, TimeStamp end, Label label, IEnumerable<SubSegment> subSegments)
    {
        Start = start;
        End = end;
        Label = label;
        SubSegments = subSegments;
    }

    public TimeStamp Start { get; }
    public TimeStamp End { get; }
    public Label Label { get; }
    public IEnumerable<SubSegment> SubSegments { get; }

    public int TotalMinutesExcludingSubSegments =>
        End.MinutesSinceMidnight - Start.MinutesSinceMidnight - SubSegments.Sum(seg => seg.TotalMinutes);
}
