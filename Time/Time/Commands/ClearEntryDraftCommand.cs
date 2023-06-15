using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Application;

namespace Time.Commands;
internal class ClearEntryDraftCommand : CommandBase
{
    public ClearEntryDraftCommand()
        : base("c")
    {
    }


    public override void Apply(Log log)
    {
        log.ClearEntryDraft();
    }
}
