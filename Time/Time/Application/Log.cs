using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.Application;
internal class Log
{
    private LogEntry? _tail;


    public void AddTimeStamp(TimeStamp timeStamp)
    {
        LogEntry entry = new(_tail)
        {
            TimeStamp = timeStamp
        };
        _tail = entry;
    }

    public void AddLabel(Label label)
    {
        if (_tail.Label is null)
        {

        }
    }
}
