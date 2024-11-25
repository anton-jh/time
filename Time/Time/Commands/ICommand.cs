using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.Commands;
internal interface ICommand
{
    bool Match(string commandName);
    void Apply(Log log);
}
