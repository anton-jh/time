using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time.Exceptions;
internal abstract class UserErrorException : Exception
{
    public UserErrorException(string userMessage)
        : base(userMessage)
    {
    }


    public override string ToString()
    {
        return Message;
    }
}
