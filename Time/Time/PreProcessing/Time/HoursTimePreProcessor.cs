using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time.PreProcessing.Time;
internal class HoursTimePreProcessor : IPreProcessor
{
    public string Process(string word)
    {
        if (word.Length == 2)
        {
            return word + "00";
        }
        else if (word.Length == 1)
        {
            return $"0{word}00";
        }

        return word;
    }
}
