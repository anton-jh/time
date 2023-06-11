using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Models;

namespace Time.PreProcessing.Time;
internal class ColonTimePreProcessor : IPreProcessor
{
    public string Process(string word)
    {
        if (word.Length == 4)
        {
            word = $"{word[..2]}:{word[2..4]}";
            Console.WriteLine(word);
        }
        Console.WriteLine(word);

        return word;
    }
}
