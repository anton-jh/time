using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Commands;
using Time.Exceptions;

namespace Time.Parsing;
internal class CommandParser : IParser<ICommand>
{
    private readonly List<ICommand> _commands;


    public CommandParser(IEnumerable<ICommand> commands)
    {
        _commands = commands.ToList();
    }


    public ICommand Parse(string word)
    {
        ICommand? command = _commands.FirstOrDefault(command => command.Match(word));

        return command is not null
            ? command
            : throw new UserErrorException("Unknown command.");
    }
}
