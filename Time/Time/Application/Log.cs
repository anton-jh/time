using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Exceptions;
using Time.Models;

namespace Time.Application;
internal class Log
{
    private readonly List<CompleteLogEntry> _entries = new();

    private DraftLogEntry? _draft;


    public void LogTime(TimeStamp timeStamp)
    {
        if (_draft is null)
        {
            _draft = new(timeStamp);
            return;
        }

        _entries.Add(_draft.Close(timeStamp));
    }

    public void SetLabel(Label label)
    {
        if (_draft is null)
        {
            throw new UserErrorException("No open entry.");
        }

        _draft.SetLabel(label);
    }

    public void AddSubSegment(SubSegment subSegment)
    {
        if (_draft is null)
        {
            throw new UserErrorException("No open entry.");
        }

        _draft.AddSubSegment(subSegment);
    }
}
