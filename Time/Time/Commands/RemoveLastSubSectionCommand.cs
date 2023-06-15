using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Application;

namespace Time.Commands;
internal class RemoveLastSubSectionCommand : CommandBase
{
    public RemoveLastSubSectionCommand()
        : base("r-")
    {
    }


    public override void Apply(Log log)
    {
        log.RemoveLastSubSection();
    }
}
