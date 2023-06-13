using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Exceptions;
using Time.Models;

namespace Time.Application;
internal class DraftLogEntry
{
    private readonly List<SubSegment> _subSegments = new();


    public DraftLogEntry(TimeStamp start)
    {
        Start = start;
    }


    public TimeStamp Start { get; }
    public TimeStamp? End { get; private set; }
    public Label? Label { get; private set; }

    public IReadOnlyList<SubSegment> SubSegments => SubSegments;


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

        int totalEntryTime = End.MinutesSinceMidnight - Start.MinutesSinceMidnight;
        int subSegmentSum = _subSegments.Sum(sub => sub.TotalMinutes) + subSegment.TotalMinutes;

        if (subSegmentSum > totalEntryTime)
        {
            throw new UserErrorException($"Total time of entry ({totalEntryTime} minutes) cannot be less than that of all sub-segments ({subSegmentSum}).");
        }

        _subSegments.Add(subSegment);
    }

    public CompleteLogEntry Close(TimeStamp end)
    {
        if (end <= Start)
        {
            throw new UserErrorException("End time must be after start time.");
        }

        if (Label is null)
        {
            throw new UserErrorException("Cannot close unlabeled entry.");
        }

        int subSegmentSum = _subSegments.Sum(sub => sub.TotalMinutes);
        int totalEntryTime = end.MinutesSinceMidnight - Start.MinutesSinceMidnight;

        if (totalEntryTime < subSegmentSum)
        {
            throw new UserErrorException($"Total time of entry ({totalEntryTime} minutes) cannot be less than that of all sub-segments ({subSegmentSum}).");
        }

        End = end;

        return new CompleteLogEntry(Start, End, Label, SubSegments);
    }
}
