using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.Application;

/// <summary>
/// Represents a timestamp
/// </summary>
internal class LogEntry
{
    public LogEntry(LogEntry? previous)
    {
        if (previous is not null && previous.TimeStamp is null)
        {
            throw new InvalidOperationException("Cannot add a new entry to an entry without a timestamp.");
        }

        Previous = previous;
    }


    public LogEntry? Previous { get; }

    public Label? Label { get; private set; }

    public TimeStamp? TimeStamp { get; set; }

    
    public void SetLabel(Label label)
    {
        if (Previous is null)
        {
            throw new InvalidOperationException("Cannot set label on first entry.");
        }

        Label = label;
    }
}
