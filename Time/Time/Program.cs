using Time.Application;
using Time.Commands;
using Time.Exceptions;
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
    new HoursTimePreProcessor(),
    new ColonTimePreProcessor()
};

IParser<TimeStamp> timeParser = new TimeStampParser();
IParser<SubSegment> subSegmentParser = new SubSegmentParser();
IParser<ICommand> commandParser = new CommandParser(new List<ICommand>()
{
    new RemoveLastEntryCommand(),
    new RemoveLastSubSectionCommand(),
    new RemoveLastExtraSectionCommand(),
    new ShowReportCommand(),
    new ClearEntryDraftCommand()
});

Log log = new();


while (true)
{
    ShowState();

    string? rawInput = Console.ReadLine();
    if (rawInput is null)
    {
        continue;
    }
    string[] inputWords = rawInput.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    foreach (string word in inputWords)
    {
        try
        {
            Parse(word);
        }
        catch (UserErrorException ex)
        {
            Console.WriteLine(ex.Message);
            Console.Read();
        }
#if !DEBUG
        catch (Exception)
        {
            Console.WriteLine("Unknown error.");
            Console.Read();
        }
#endif
    }
}


void Parse(string word)
{
    word = PreProcess(word, globalPreProcessors);

    if (char.IsNumber(word, 0))
    {
        word = PreProcess(word, timePreProcessors);

        TimeStamp? timeStamp = timeParser.Parse(word);

        if (timeStamp is null)
        {
            Console.WriteLine("Invalid timestamp.");
            return;
        }

        log.LogTime(timeStamp.Value);
    }
    else if (word[0] is '-' or '+')
    {
        SubSegment? subSegment = subSegmentParser.Parse(word);

        if (subSegment is null)
        {
            Console.WriteLine("Invalid SubSegment.");
            return;
        }

        log.AddSubSegment(subSegment);
    }
    else if (word[0] == '.')
    {
        commandParser.Parse(word[1..])?.Apply(log);
    }
    else
    {
        log.SetLabel(new Label(word));
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

void ShowState()
{
    Console.Clear();

    if (log.ExtraSegments.Any())
    {
        foreach (SubSegment subSegment in log.ExtraSegments)
        {
            Console.WriteLine(subSegment);
        }

        Console.WriteLine();
    }

    foreach (LogEntry entry in log.Entries)
    {
        Console.WriteLine(entry);
    }

    if (log.EntryDraft is not null)
    {
        Console.WriteLine(log.EntryDraft);
    }

    Console.WriteLine();
}
