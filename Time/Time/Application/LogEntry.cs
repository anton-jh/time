using System.Text;
using Time.Models;

namespace Time.Application;
internal class LogEntry
{   
    public LogEntry(TimeOnly start, TimeOnly end, Label label, IEnumerable<SubSegment> subSegments)
    {
        Start = start;
        End = end;
        Label = label;
        SubSegments = subSegments;
    }

    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public Label Label { get; }
    public IEnumerable<SubSegment> SubSegments { get; }

    public TimeSpan TotalTimeExcludingSubSegments =>
        End - Start - new TimeSpan(SubSegments.Sum(seg => seg.TimeSpan.Ticks));


    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendLine($"{Start} -> {End} ({Label})");

        foreach (SubSegment subSegment in SubSegments)
        {
            stringBuilder.AppendLine("  " + subSegment.ToString());
        }

        return stringBuilder.ToString();
    }
}
