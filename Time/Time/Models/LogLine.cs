using Time.Exceptions;

namespace Time.Models;

internal abstract record LogLine;

internal record TimeLogLine(TimeOnly Timestamp) : LogLine;
internal record LabelLogLine(string Label) : LogLine;
internal record SubSegmentLogLine(TimeSpan Timespan) : LogLine
{
    public override bool CanAppend(LogState state)
    {
        return state.IsOpen();
    }
}


internal record ExternalSegmentLogLine(TimeSpan Timespan) : LogLine
{
    public override bool CanAppend(LogState state)
    {
        return true;
    }
}


internal class LogState
{
    private Dictionary<string, TimeSpan> _accounts = [];
    private TimeOnly? _startTime;


    public Stack<LogLine> MainStack { get; set; } = [];


    public void Append(LogLine line)
    {
        switch (line)
        {
            case TimeLogLine timeLogLine:
                if (GetLatestTime() is TimeLogLine time
                    && time.Timestamp < timeLogLine.Timestamp)
                {
                    throw new InvalidLogOperationException("New timestamp must be after the previous");
                }
                MainStack.Push(line);
                break;
            case LabelLogLine labelLogLine:
                if (IsEmpty())
                {
                    throw new InvalidLogOperationException("Cannot add label without a start time");
                }
                if (GetLabelOfDraftSegment() is not null)
                {
                    throw new InvalidLogOperationException("Cannot add multiple labels to same segment");
                }
                break;
            default:
                break;
        }
    }


    private bool IsEmpty()
    {
        return MainStack.Count == 0;
    }

    private TimeLogLine? GetLatestTime()
    {
        return MainStack
            .OfType<TimeLogLine>()
            .FirstOrDefault();
    }

    private LabelLogLine? GetLabelOfDraftSegment()
    {
        return MainStack
            .TakeWhile(x => x is not TimeLogLine)
            .OfType<LabelLogLine>()
            .FirstOrDefault();
    }


    internal record Segment(TimeLogLine StartTime, LabelLogLine? Label);
}
