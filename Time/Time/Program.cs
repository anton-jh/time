using Time.Models;
using Time.Parsing;
using Time.PreProcessing;
using Time.PreProcessing.Global;
using Time.PreProcessing.Time;


List<IPreProcessor> globalPreProcessors = new()
{
    new NowPreProcessor()
};

List<IPreProcessor> timePreProcessors = new()
{
    new ColonTimePreProcessor()
};

IParser<TimeStamp> timeParser = new TimeParser();


while (true)
{
    string[] inputWords = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    foreach (string word in inputWords)
    {
        Parse(word);
    }
}


void Parse(string word)
{
    word = PreProcess(word, globalPreProcessors);

    if (char.IsNumber(word, 0))
    {
        word = PreProcess(word, timePreProcessors);

        TimeStamp? timeStamp = timeParser.Parse(word);

        Console.WriteLine($"Timestamp: ({timeStamp})");
    }
    else if (word[0] == '.')
    {

    }
}

string PreProcess(string word, IEnumerable<IPreProcessor> preProcessors)
{
    foreach (IPreProcessor preProcessor in preProcessors)
    {
        word = preProcessor.Process(word);
    }

    return word;
}
