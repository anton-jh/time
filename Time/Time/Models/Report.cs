using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Application;

namespace Time.Models;
internal class Report
{
    private readonly Dictionary<Label, TimeSpan> _accounts = new();


    public Report(IEnumerable<LogEntry> entries, IEnumerable<SubSegment> extraSegments)
    {
        AddEntries(entries);
        AddSegments(extraSegments);
    }


    private void AddEntries(IEnumerable<LogEntry> entries)
    {
        foreach (LogEntry entry in entries)
        {
            AddTime(entry.Label, entry.TotalTimeExcludingSubSegments);
            AddSegments(entry.SubSegments);
        }
    }

    private void AddSegments(IEnumerable<SubSegment> subSegments)
    {
        foreach (SubSegment subSegment in subSegments)
        {
            AddTime(subSegment.Label, subSegment.TimeSpan);
        }
    }

    private void AddTime(Label label, TimeSpan time)
    {
        if (_accounts.ContainsKey(label))
        {
            _accounts[label] += time;
        }
        else
        {
            _accounts.Add(label, time);
        }
    }


    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        int longestKey = _accounts.Keys.Max(key => key.Value.Length);

        foreach (KeyValuePair<Label, TimeSpan> account in _accounts)
        {
            int padding = longestKey - account.Key.Value.Length;
            string paddingString = new(' ', padding);

            stringBuilder.AppendLine($@"{account.Key}:{paddingString} {account.Value:h\hm\m}");
        }

        return stringBuilder.ToString();
    }
}
