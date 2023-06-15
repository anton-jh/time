using System.Text;
using Time.Exceptions;
using Time.Models;

namespace Time.Application;
internal class LogEntryDraft
{
    private readonly List<SubSegment> _subSegments = new();


    public LogEntryDraft(TimeOnly start)
    {
        Start = start;
    }


    public TimeOnly Start { get; }
    public TimeOnly? End { get; private set; }
    public Label? Label { get; private set; }

    public IReadOnlyList<SubSegment> SubSegments => _subSegments;


    public void SetLabel(Label label)
    {
        Label = label;
    }

    public void AddSubSegment(SubSegment subSegment)
    {
        if (End is null)
        {
            _subSegments.Add(subSegment);
            return;
        }

        TimeSpan totalEntryTime = End.Value - Start;
        TimeSpan subSegmentSum = new(SubSegments.Sum(seg => seg.TimeSpan.Ticks));

        if (subSegmentSum > totalEntryTime)
        {
            throw new UserErrorException($@"Total time of entry ({totalEntryTime:h\h m\m}) cannot be less than that of all sub-segments ({subSegmentSum:h\h m\m}).");
        }

        _subSegments.Add(subSegment);
    }

    public LogEntry Close(TimeOnly end)
    {
        if (end <= Start)
        {
            throw new UserErrorException("End time must be after start time.");
        }

        if (Label is null)
        {
            throw new UserErrorException("Cannot close unlabeled entry.");
        }

        TimeSpan totalEntryTime = end - Start;
        TimeSpan subSegmentSum = new(SubSegments.Sum(seg => seg.TimeSpan.Ticks));

        if (totalEntryTime < subSegmentSum)
        {
            throw new UserErrorException($@"Total time of entry ({totalEntryTime:h\hm}min) cannot be less than that of all sub-segments ({subSegmentSum:h\hm}min).");
        }

        End = end;

        return new LogEntry(Start, End.Value, Label, SubSegments);
    }

    public void RemoveLastSubSegment()
    {
        if (_subSegments.Any())
        {
            _subSegments.RemoveAt(_subSegments.Count - 1);
        }
    }


    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        string end = End is null ? "     " : End.Value.ToString();

        stringBuilder.AppendLine($"{Start} -> {end} ({Label})");

        foreach (SubSegment subSegment in SubSegments)
        {
            stringBuilder.AppendLine("  " + subSegment.ToString());
        }

        return stringBuilder.ToString();
    }
}
