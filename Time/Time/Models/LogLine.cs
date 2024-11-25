namespace Time.Models;

internal abstract record LogLine
{
    public abstract void Apply(Log log);
}

internal record TimeLogLine(TimeStamp Timestamp) : LogLine
{
    public override void Apply(Log log)
    {
        log.LogTime(Timestamp);
    }
}


internal record LabelLogLine(Label Label) : LogLine
{
    public override void Apply(Log log)
    {
        log.SetLabel(Label);
    }
}

internal record SubSegmentLogLine(SubSegment SubSegment) : LogLine
{
    public override void Apply(Log log)
    {
        log.AddSubSegment()
    }
}

internal record ExternalSegmentLogLine(TimeSpan Timespan) : LogLine;