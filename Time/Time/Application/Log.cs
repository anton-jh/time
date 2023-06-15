using Time.Exceptions;
using Time.Models;

namespace Time.Application;
internal class Log
{
    private readonly List<LogEntry> _entries = new();
    private readonly List<SubSegment> _segments = new();


    public LogEntryDraft? EntryDraft { get; private set; }

    public IEnumerable<LogEntry> Entries => _entries;
    public IEnumerable<SubSegment> ExtraSegments => _segments;


    public void LogTime(TimeOnly timeStamp)
    {
        if (EntryDraft is null)
        {
            if (_entries.Any() && _entries.Last().End > timeStamp)
            {
                throw new UserErrorException("Start time must be after previous entry's end time.");
            }

            EntryDraft = new(timeStamp);
            return;
        }

        _entries.Add(EntryDraft.Close(timeStamp));
        EntryDraft = null;
    }

    public void SetLabel(Label label)
    {
        if (EntryDraft is null && _entries.Any())
        {
            EntryDraft = new(_entries.Last().End);
        }
        else if (EntryDraft is null)
        {
            throw new UserErrorException("No open entry and no entry to continue from.");
        }

        EntryDraft.SetLabel(label);
    }

    public void AddSubSegment(SubSegment subSegment)
    {
        if (!subSegment.Subtractive)
        {
            _segments.Add(subSegment);
            return;
        }

        if (EntryDraft is null)
        {
            throw new UserErrorException("No open entry.");
        }

        EntryDraft.AddSubSegment(subSegment);
    }

    public void RemoveLastEntry()
    {
        if (_entries.Any())
        {
            _entries.RemoveAt(_entries.Count - 1);
        }
    }

    public void RemoveLastSubSection()
    {
        EntryDraft?.RemoveLastSubSegment();
    }

    public void RemoveLastExtraSegment()
    {
        if (_segments.Any())
        {
            _segments.RemoveAt(_segments.Count - 1);
        }
    }

    public void ClearEntryDraft()
    {
        EntryDraft = null;
    }

    public Report GenerateReport()
    {
        return new(_entries, _segments);
    }
}
