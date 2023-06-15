using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Application;

namespace Time.Commands;
internal abstract class CommandBase : ICommand
{
    private readonly string _commandName;


    public CommandBase(string commandName)
    {
        _commandName = commandName;
    }


    public bool Match(string commandName)
    {
        return commandName == _commandName;
    }

    public abstract void Apply(Log log);
}
