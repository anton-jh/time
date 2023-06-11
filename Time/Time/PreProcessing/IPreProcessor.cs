using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.PreProcessing;
internal interface IPreProcessor
{
    string Process(string word);
}
